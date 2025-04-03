using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
using Utils;
using System.Linq;


/// <summary>
/// Decide which segment to spawn next, then spawn it in MapController
/// </summary>
public class MapGenerator
{
    private SpawnConfigData spawnConfigData;
    private DifficultData difficultData;

    private SegmentRule segmentRule;

    private MapBiome currentBiome;
    private Dictionary<MapBiome, MapBiomeData> biomeDataDict;   


    private Queue<SegmentType> currentSegmentQ;

    private const int MAX_SEGMENT = 11;

    public MapGenerator(SpawnConfigData spawnConfigData)
    {
        this.spawnConfigData = spawnConfigData;


        LoadDifficultData();

        Initialize();


    }

    private void Initialize()
    {
        segmentRule = new SegmentRule();
        currentSegmentQ = new Queue<SegmentType>();

       
        currentBiome = spawnConfigData.startBiome;
        biomeDataDict = spawnConfigData.GetBiomeDataDict();
        AddSegment(SegmentType.START);
    }

    

    // Getter method
 
    public MapBiome GetCurrentBiome()
    {
        return currentBiome;
    }



    private void LoadDifficultData()
    {
        difficultData = Resources.Load<DifficultData>(Paths.DIFFICULTY_DATA);
        if (difficultData == null)
        {
            Debug.Log("Cannot load DifficultData:  DifficultData is null");
        }
        Debug.Log("Load DifficultData successfully");
    }






    public SegmentType GenerateNewSegmentType()
    {
        SegmentType nextSegmentType = ProceduralGenerate();

        AddSegment(nextSegmentType);

        //Debug.Log(nextSegmentType);

        return nextSegmentType;
    }

    private SegmentType ProceduralGenerate()
    {

        // Get list of segment type belong to current biome
        List<SegmentType> segmentList = new List<SegmentType>();
        MapBiomeData biomeData = biomeDataDict[currentBiome];

        for(int i = 0; i < biomeData.segmentTypeList.Count; i++)
        {
            segmentList.Add(biomeData.segmentTypeList[i].segmentType);
        }

        // Put list to SegmentRule to filter out list of next valid segment type 
        segmentList = segmentRule.Filter(segmentList, currentSegmentQ.ToList());

        // Get random segment type from list

        SegmentType nextSegmentType = GetRandomType(segmentList);

        return nextSegmentType;
    }


    private SegmentType GetRandomType(List<SegmentType> segmentList)
    {
        int weightSum = 0;


        DifficultProfile biomeProfile = difficultData.profiles.Find(x => x.profile.difficulty == GameplayManager.Instance.currentDifficulty).profile;

        Dictionary<SegmentType, int> segmentWeightDict = biomeProfile.GetSegmentWeightDict();

        for (int i = 0; i < segmentList.Count; i++)
        {

            weightSum += segmentWeightDict[segmentList[i]];
        }

        int randomIndex = Random.Range(0, weightSum);
        for (int i = 0; i < segmentList.Count; i++)
        {
            randomIndex -= segmentWeightDict[segmentList[i]];
            if (randomIndex <= 0)
            {
                return segmentList[i];
            }
        }

        return SegmentType.NONE;
    }

    public void AddSegment(SegmentType segmentType)
    {
        if (currentSegmentQ.Count > MAX_SEGMENT)
        {
            DeleteOldSegment();
        }
        currentSegmentQ.Enqueue(segmentType);
    }

    public void DeleteOldSegment()
    {
        currentSegmentQ.Dequeue();
    }

    public void Reset()
    {
        currentSegmentQ.Clear();

        currentBiome = spawnConfigData.startBiome;
        AddSegment(SegmentType.START);


    }

}
