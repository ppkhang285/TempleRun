using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

[CreateAssetMenu(fileName = "SpawnConfigData", menuName = "MapData/SpawnConfigData")]
public class SpawnConfigData : ScriptableObject
{
    public List<MapBiomeData> biomeList;
    public GameObject startSegment;
    //public GameObject changeBiomeSegment;
    public MapBiome startBiome;


}
