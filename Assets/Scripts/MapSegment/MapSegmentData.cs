using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtils.Enums;

[CreateAssetMenu(fileName = "MapSegmentData", menuName = "MapData/MapSegmentData")]
public class MapSegmentData: ScriptableObject
{
    public SegmentType segmentType;
    public MapBiome biome;

    [Serializable]
    public struct PrefabWithWeight
    {
        public GameObject prefab;
        public float weight;
    }
    public List<PrefabWithWeight> prefabList;

    public GameObject GetRandomPrefab()
    {
        float weightSum = 0;
        for (int i = 0; i < prefabList.Count; i++)
        {
            weightSum += prefabList[i].weight;
        }
        float randomIndex = UnityEngine.Random.Range(0, weightSum);
        int currentSum = 0;

        for(int i = 0; i < prefabList.Count; i++)
        {
            currentSum += (int)prefabList[i].weight;
            if (randomIndex <= currentSum)
            {
                return prefabList[i].prefab;
            }
        }

        return prefabList[0].prefab;
    }

   
}
