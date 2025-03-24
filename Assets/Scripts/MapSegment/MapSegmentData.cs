using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

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
}
