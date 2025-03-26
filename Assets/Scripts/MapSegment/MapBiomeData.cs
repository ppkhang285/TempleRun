using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

[CreateAssetMenu(fileName = "MapBiomeData", menuName = "MapData/MapBiomeData")]
public class MapBiomeData : ScriptableObject
{
    public MapBiome biome;
    public float height;

    public List<MapSegmentData> segmentTypeList;

    public MapSegmentData GetSegmentData(SegmentType segmentType)
    {
        for (int i = 0; i < segmentTypeList.Count; i++)
        {
            if (segmentTypeList[i].segmentType == segmentType)
            {
                return segmentTypeList[i];
            }
        }
        return null;
    }

    public Dictionary<SegmentType, MapSegmentData> GetSegmentDataDict()
    {
        Dictionary<SegmentType, MapSegmentData> segmentDataDict = new Dictionary<SegmentType, MapSegmentData>();
        for (int i = 0; i < segmentTypeList.Count; i++)
        {
            if (!segmentDataDict.ContainsKey(segmentTypeList[i].segmentType))
            {
                segmentDataDict.Add(segmentTypeList[i].segmentType, segmentTypeList[i]);
            }
        }
        return segmentDataDict;
    }
}
