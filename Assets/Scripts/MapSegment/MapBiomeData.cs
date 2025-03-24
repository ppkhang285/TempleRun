using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

[CreateAssetMenu(fileName = "MapBiomeData", menuName = "MapData/MapBiomeData")]
public class MapBiomeData : ScriptableObject
{
    public MapBiome biome;

    public List<MapSegmentData> segmentTypeList;


}
