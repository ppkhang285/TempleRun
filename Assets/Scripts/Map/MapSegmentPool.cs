using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;




public class PoolData
{
    public MapBiome biome;
    public SegmentType type;
}

public class MapSegmentPool 
{

    private Dictionary<PoolData, Queue<GameObject>> objectPool;

    public MapSegmentPool()
    {
        Initialize();   
    }

    private void Initialize()
    {
        objectPool = new Dictionary<PoolData, Queue<GameObject>>();
    }

    public GameObject GetObject()
    {
        return null;
    }

    public void ReturnObject()
    {

    }

    
}
