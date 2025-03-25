using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
using Utils;


/// <summary>
/// Use to control the map:<br/>
/// - Spawn Segment <br/>
/// - Move Segments (update)
/// </summary>
public class MapController 
{
    private Transform mapRoot;
    private MapGenerator mapGenerator;

    private List<MapSegment> mapSegments;
    private SpawnConfigData spawnConfigData;

    private Dictionary<MapBiome, MapBiomeData> biomeDataDict;

    public MapController(Transform mapRoot)
    {
        this.mapRoot = mapRoot;
        
        
        Initialize();
      
    }


    public void Update()
    {
        for(int i = 0; i < mapSegments.Count; i++)
        {
            mapSegments[i].MoveSegment(GameplayManager.Instance.moving_speed, -Vector3.right);
        }
    }


    

    private void Initialize()
    {
        LoadSpawnConfigData();
        mapSegments = new List<MapSegment>();
        mapGenerator = new MapGenerator(spawnConfigData);

        biomeDataDict = spawnConfigData.GetBiomeDataDict();

    }

    public void InitEnviroment()
    {
        // Spawn Water,...

        // Spawn Start Segment (Start_Gate)
        SpawnStartSegment();

    }

    public void SpawnEnviroment()
    {

    }

    private void SpawnStartSegment()
    {
        float height = biomeDataDict[MapBiome.Temple].height;

        GameObject startSegmentPref = spawnConfigData.startSegment;
        GameObject startSegmentInstance = GameObject.Instantiate(startSegmentPref, Vector3.zero + Vector3.up * height, Quaternion.identity,  mapRoot);
        MapSegment startSegment = new MapSegment(SegmentType.START, startSegmentInstance.transform);
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
        SegmentType genratedType = mapGenerator.GenerateNewSegmentType();
        SpawnNewSegment(genratedType, mapGenerator.GetCurrentBiome());
    }


    public void SpawnNewSegment(SegmentType segmentType, MapBiome biome)
    {

        
        GameObject segmentPref = GetSegmentPrefab(segmentType, biome);
        if (segmentPref == null)
        {
            Debug.Log("Cannot spawn segment: segmentPref is null");
            return;
        }

        float height = biomeDataDict[biome].height;
        Vector3 position = GetNewPosition(segmentPref) + Vector3.up * height;
        Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];


        GameObject segmentInstance = GameObject.Instantiate(segmentPref, Vector3.down * 10, Quaternion.identity, mapRoot);
        segmentInstance.transform.position = position;
        segmentInstance.transform.rotation = rotation;

        MapSegment segment = new MapSegment(segmentType, segmentInstance.transform);
        mapSegments.Add(segment);

        //Debug.Log(segmentInstance.GetComponent<MeshRenderer>().bounds.size);
    }


    private Vector3 GetNewPosition(GameObject newPref)
    {
        Vector3 directionVector = Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];

        Vector3 newSize = newPref.GetComponent<MeshRenderer>().bounds.size;
        Vector3 lastPosition = mapSegments[mapSegments.Count - 1].transform.position;
        Vector3 lastSize = mapSegments[mapSegments.Count - 1].transform.GetComponent<MeshRenderer>().bounds.size;

        Debug.Log(lastSize);
        Debug.Log(newSize);


        Vector3 newPosition = lastPosition + directionVector * (newSize.x + lastSize.x) / 2;
        newPosition.y = 0;

        return newPosition;
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
