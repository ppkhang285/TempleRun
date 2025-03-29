using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
using Utils;
using UnityEngine.UIElements;


/// <summary>
/// Use to control the map:<br/>
/// - Spawn Segment <br/>
/// - Move Segments (update)
/// </summary>
public class MapController 
{
    private Transform mapRoot;
    private MapGenerator mapGenerator;
    private MapSegmentPool mapSegmentPool;

    private List<MapSegment> mapSegments;
    private SpawnConfigData spawnConfigData;

    private Dictionary<MapBiome, MapBiomeData> biomeDataDict;

    private const int MAX_SEGMENT = 4;
    private const float DESTROY_DISTANCE = 300.0f;

    public MapController(Transform mapRoot)
    {
        this.mapRoot = mapRoot;
        
        
        Initialize();
      
    }


    public void Update()
    {

        //if (mapSegments.Count > MAX_SEGMENT)
        //{
        //    mapSegments[0].OnDestroy();
        //    mapSegments.RemoveAt(0);
        //}

        HandleDeleteSegments();


        for (int i = 0; i < mapSegments.Count; i++)
        {
            Vector3 moveVector = -Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];
            mapSegments[i].MoveSegment(GameplayManager.Instance.moving_speed, moveVector);
        }
    }

    private void HandleDeleteSegments()
    {
        for (int i = mapSegments.Count - 1; i >= 0; i--)
        {
            float distance = Vector3.Distance(mapSegments[i].segmentTransform.position, Vector3.zero);
            if (distance > DESTROY_DISTANCE)
            {
                //mapSegments[i].OnDestroy();
                GameObject segmentObject = mapSegments[i].segmentTransform.gameObject;

                mapSegmentPool.ReturnObject(segmentObject, mapSegments[i].biome, mapSegments[i].segmentType);

                mapSegments.RemoveAt(i);
            }
        }
    }

    

    private void Initialize()
    {
        LoadSpawnConfigData();
        mapSegments = new List<MapSegment>();
        mapGenerator = new MapGenerator(spawnConfigData);
        mapSegmentPool = new MapSegmentPool();

        biomeDataDict = spawnConfigData.GetBiomeDataDict();

    }

    public void InitEnviroment()
    {
        // Spawn Water,...

        // Spawn Start Segment (Start_Gate)
        SpawnStartSegment();
        SpawnNewSegment();
        SpawnNewSegment();
        SpawnNewSegment();
    }

    public void SpawnEnviroment()
    {

    }

    private void SpawnStartSegment()
    {
        float height = biomeDataDict[MapBiome.Temple].height;

        GameObject startSegmentPref = spawnConfigData.startSegment;
        GameObject startSegmentInstance = GameObject.Instantiate(startSegmentPref, Vector3.zero + Vector3.up * height, Quaternion.identity,  mapRoot);
        MapSegment startSegment = new MapSegment(SegmentType.START, MapBiome.Temple,startSegmentInstance.transform, 
                                                    GameplayManager.Instance.currentDirecion );


        mapSegments.Add(startSegment);


        //SpawnNewSegment(SegmentType.Straight, MapBiome.Temple);
    }

    private void LoadSpawnConfigData()
    {
        spawnConfigData = Resources.Load<SpawnConfigData>(Paths.SPAWN_CONFIG_DATA);
        if (spawnConfigData == null)
        {
            Debug.Log("Cannot load SpawnConfigData:  SpawnConfigData is null");
        }
        Debug.Log("Load SpawnConfigData successfully");
    }



    public void SpawnNewSegment()
    {
        
        SegmentType generatedType = mapGenerator.GenerateNewSegmentType();

        SpawnNewSegment(generatedType, mapGenerator.GetCurrentBiome());
    }


    public void SpawnNewSegment(SegmentType segmentType, MapBiome biome)
    {
        
        
        GameObject segmentPref = GetSegmentPrefab(segmentType, biome);
        List<MapSegment> newSegments = new List<MapSegment>();
        
        if (segmentPref == null)
        {
            Debug.Log("Cannot spawn segment: segmentPref is null");
            return;
        }

        float height = biomeDataDict[biome].height;


        for(int i = mapSegments.Count -1; i >= 0; i--)
        {
            if (!mapSegments[i].CanSpawnNeighbor()) continue;
            MapSegment lastSegment = mapSegments[i];
            lastSegment.FlagNextSpawn();

            Direction newSegmentDirection = lastSegment.GetNeighborDirection();
            Vector3 position = lastSegment.GetNeighborPos(segmentPref) + Vector3.up * height;         
            Quaternion rotation = Constants.ROTATION_VECTOR[newSegmentDirection];

            //GameObject segmentInstance = GameObject.Instantiate(segmentPref, Vector3.down * 10, Quaternion.identity, mapRoot);
            GameObject segmentInstance = mapSegmentPool.GetObject(biome, segmentType, segmentPref);
            segmentInstance.transform.SetParent(mapRoot);

            segmentInstance.transform.position = position;
            segmentInstance.transform.rotation = rotation;

            MapSegment segment = new MapSegment(segmentType, biome, segmentInstance.transform, newSegmentDirection);
            mapSegments.Add(segment);


            if (lastSegment.segmentType == SegmentType.Turn_Both)
            {
                newSegmentDirection = lastSegment.GetNeighborDirection(true);
                position = lastSegment.GetNeighborPos(segmentPref, true) + Vector3.up * height;
                rotation = Constants.ROTATION_VECTOR[newSegmentDirection];

                //segmentInstance = GameObject.Instantiate(segmentPref, Vector3.down * 10, Quaternion.identity, mapRoot);
                segmentInstance = mapSegmentPool.GetObject(biome, segmentType, segmentPref);

                segmentInstance.transform.SetParent(mapRoot);
                segmentInstance.transform.position = position;
                segmentInstance.transform.rotation = rotation;

                segment = new MapSegment(segmentType, biome, segmentInstance.transform, newSegmentDirection);
                mapSegments.Add(segment);
            }
            

        }
       
    }


    private GameObject GetSegmentPrefab(SegmentType segmentType, MapBiome biome)
    {
        MapBiomeData biomeData = biomeDataDict[biome];
        
        MapSegmentData segmentData = biomeData.GetSegmentData(segmentType);
 
        
        if (segmentData == null)
        {
            Debug.Log("Cannot get segment prefab: segmentData is null");
            return null;
        }

        GameObject prefab = segmentData.GetRandomPrefab();
        return prefab;
    }




}
