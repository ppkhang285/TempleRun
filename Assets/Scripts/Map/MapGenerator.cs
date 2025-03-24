using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
using Utils;


/// <summary>
/// Decide which segment to spawn next, then spawn it in MapController
/// </summary>
public class MapGenerator
{
    private SpawnConfigData spawnConfigData;
    private DifficultData difficultData;

    private SegmentRule segmentRule;

    private MapBiome currentBiome;
    private List<SegmentType> segmentList;



    public MapGenerator()
    {


        LoadSpawnConfigData();
        LoadDifficultData();

        Initialize();


    }

    private void Initialize()
    {
        segmentRule = new SegmentRule();
        segmentList = new List<SegmentType>();


        currentBiome = spawnConfigData.startBiome;
        segmentList.Add(SegmentType.START);
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

    private void LoadDifficultData()
    {
        difficultData = Resources.Load<DifficultData>(Paths.DIFFICULTY_DATA);
        if (difficultData == null)
        {
            Debug.Log("Cannot load DifficultData:  DifficultData is null");
        }
        Debug.Log("Load DifficultData successfully");
    }


    private SegmentType GetNextSegmentType()
    {
        return SegmentType.NONE;
    }

    private SegmentType ProceduralGenerate()
    {
        return SegmentType.NONE;
    }

   
}
