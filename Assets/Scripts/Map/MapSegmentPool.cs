using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;






public class MapSegmentPool
{

    private Dictionary<(MapBiome, SegmentType), Queue<GameObject>> objectPool;
    private GameObject poolRoot;
    private Vector3 rootPosition;

    public MapSegmentPool()
    {
        Initialize();
    }

    private void Initialize()
    {
        rootPosition = new Vector3(0, -1000, 0);
        poolRoot = new GameObject("PoolRoot");
        poolRoot.transform.position = rootPosition;

        objectPool = new Dictionary<(MapBiome, SegmentType), Queue<GameObject>>();

    }

    public GameObject GetObject(MapBiome biome, SegmentType type, GameObject prefab = null)
    {
        (MapBiome, SegmentType) key = (biome, type);
        if (!objectPool.ContainsKey(key) || objectPool[key].Count <=0)
        {
            if (prefab == null)
            {
                Debug.LogError("No prefab to Create Object in Pooling");
            }

            //Create new Instance
            GameObject segmentInstance = GameObject.Instantiate(prefab);
            

            // Return it
            return segmentInstance;
        }

        GameObject seqmentObject = objectPool[key].Dequeue();
        ResetObjectState(seqmentObject);

        return seqmentObject;
    }

    public void ReturnObject(GameObject returnObject, MapBiome biome, SegmentType type)
    {
        (MapBiome, SegmentType) key = (biome, type);

        returnObject.transform.SetParent(poolRoot.transform);
        returnObject.transform.localPosition = Vector3.zero;


        if (!objectPool.ContainsKey(key))
        {
            objectPool[key] = new Queue<GameObject>(); 
        }

        objectPool[key].Enqueue(returnObject);
    }

    private void ResetObjectState(GameObject segmentObject)
    {
   
        segmentObject.transform.SetParent(null);

        // Player turn of spawnTrigger when collided -> Turn on again
        
        segmentObject.transform.GetChild(0).Find("spawnTrigger").gameObject.SetActive(true);
        
    }

    
}
