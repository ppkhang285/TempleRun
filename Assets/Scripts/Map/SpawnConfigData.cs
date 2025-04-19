using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtils.Enums;

[CreateAssetMenu(fileName = "SpawnConfigData", menuName = "MapData/SpawnConfigData")]
public class SpawnConfigData : ScriptableObject
{
    public List<MapBiomeData> biomeList;
    public GameObject startSegment;
    //public GameObject changeBiomeSegment;
    public MapBiome startBiome;


    public Dictionary<MapBiome, MapBiomeData> GetBiomeDataDict()
    {
        Dictionary<MapBiome, MapBiomeData> biomeDataDict = new Dictionary<MapBiome, MapBiomeData>();
        for (int i = 0; i < biomeList.Count; i++)
        {
            if (!biomeDataDict.ContainsKey(biomeList[i].biome))
            {
                biomeDataDict.Add(biomeList[i].biome, biomeList[i]);
            }
        }
        return biomeDataDict;
    }

}
