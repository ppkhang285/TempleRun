
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static UnityEngine.Rendering.HableCurve;

public class CoinController
{
    private GameObject coinPrefab;

    private Transform mapRoot;

    private CoinPool coinPool;

    private List<GameObject> coinList;

    private const int MIN_COIN = 5;
    private const int MAX_COIN = 10;


    private const int padding = 1; // Padding between 2 coin
    private const float DESTROY_DISTANCE = 300.0f;

    public CoinController(Transform mapRoot)
    {

        LoadCoinPrefab();
        this.mapRoot = mapRoot; 
        coinPool = new CoinPool(coinPrefab);
        coinList = new List<GameObject>();
    }

    public void MoveCoins(float speed, Vector3 direction)
    {
        foreach(GameObject coinObj in coinList)
        {
            coinObj.transform.position  += direction * speed * Time.deltaTime;
        }
        
    
    }

    private void LoadCoinPrefab()
    {
        coinPrefab = GameplayManager.Instance.coinPrefab;


    }

    public void SpawnCoin(MapSegment mapSegment)
    {
        if (mapSegment == null)
        {
            Debug.Log("Map Segment is null when spawning coin");
            return;
        }

        if (mapSegment.segmentType != Enums.SegmentType.Jump) return;
        
        List<Vector3> coinPosList = Pattern_Jump(mapSegment);
        Quaternion rotation = Constants.ROTATION_VECTOR[mapSegment.direction];
        Vector3 segmentPos = mapSegment.segmentTransform.position;

        foreach (Vector3 coinPos in coinPosList)
        {
            GameObject coinObj = coinPool.GetObject();

            coinObj.transform.SetParent(mapRoot, true);
            coinObj.transform.position = segmentPos + rotation * coinPos;//coinPos + segmentPos;
    

            coinList.Add(coinObj);
            
        }
    }

    public void DespawnCoin(GameObject coinObj)
    {
        coinList.Remove(coinObj);
        coinPool.ReturnObject(coinObj);
        
    }

    public void DesSpawnCoinAt(int index)
    {
        GameObject coinObj = coinList[index];
        coinList.RemoveAt(index);
        coinPool.ReturnObject(coinObj);
    }
    public void HandleDeleteCoins()
    {
        for (int i = coinList.Count - 1; i >= 0; i--)
        {
            float distance = Vector3.Distance(coinList[i].transform.position, Vector3.zero);
            if (distance > DESTROY_DISTANCE)
            {
                DesSpawnCoinAt(i);
            }
        }
    }

    /// <summary>
    /// Return List of coin's position. For both Straight and Slide
    /// </summary>
    public List<Vector3> Pattern_Straight(MapSegment segment)
    {
        if (segment == null)
        {
            Debug.Log("GameObject is null");
        }
        // Init stat
        GameObject segmentObj = segment.segmentTransform.gameObject;

        List<Vector3> result = new List<Vector3>();

        Vector3 landSize = segmentObj.transform.GetChild(0).Find("groundTrigger").GetComponent<BoxCollider>().size;
        Vector3 coinSize = coinPrefab.GetComponent<BoxCollider>().size;

        int maxCoin = (int)Mathf.Floor((landSize.x + padding) / (coinSize.x + padding)); // n*s + (n-1)*p < x -> find n
        maxCoin = (int)Mathf.Min(maxCoin, MAX_COIN);

        //
        int[] lane = new int[3] { -1, 0, 1}; // Left - Middle - Right
        int randomIndex = UnityEngine.Random.Range(0, 3);

        float baseX = -landSize.x / 2 + coinSize.x / 2;
        float z = lane[randomIndex] * (landSize.z /3 - coinSize.z/2);

        result.Add(new Vector3(baseX, 0, z));

        for(int i = 1; i < maxCoin; i++)
        {
            Vector3 currPos = result[result.Count - 1] + Vector3.right * (coinSize.x + padding);
            result.Add(currPos);
        }

        return result;
    }

    /// <summary>
    /// Return List of coin's position. For Jump
    /// </summary>
    public List<Vector3> Pattern_Jump(MapSegment segment)
    {
        if (segment == null)
        {
            Debug.Log("GameObject is null");
        }
        // Init stat
        GameObject segmentObj = segment.segmentTransform.gameObject;

        List<Vector3> result = new List<Vector3>();

        Vector3 landSize = segmentObj.transform.GetChild(0).Find("groundTrigger").GetComponent<BoxCollider>().size;

        Vector3 collliderPos = Vector3.zero;
        if (segmentObj.transform.GetChild(0).Find("deathTrigger"))
        {
            collliderPos  = segmentObj.transform.GetChild(0).Find("deathTrigger").GetComponent<BoxCollider>().center;
        }
        
        else
        {
            collliderPos = segmentObj.transform.GetChild(0).Find("stumpleTrigger").GetComponent<BoxCollider>().center;
        }
        Vector3 coinSize = coinPrefab.GetComponent<BoxCollider>().size;

        int maxCoin = (int)Mathf.Floor((landSize.x + padding) / (coinSize.x + padding)); // n*s + (n-1)*p < x -> find n
        maxCoin = (int)Mathf.Min(maxCoin, MAX_COIN);

        //
        int[] lane = new int[3] { -1, 0, 1 }; // Left - Middle - Right
        int randomIndex = UnityEngine.Random.Range(0, 3);
        float baseX = -landSize.x / 2 + coinSize.x / 2;
        float z = lane[randomIndex] * (landSize.z / 3 - coinSize.z / 2);

        result.Add(new Vector3(baseX, 0, z));

        for (int i = 1; i < maxCoin; i++)
        {
            Vector3 currPos = result[result.Count - 1] + Vector3.right * (coinSize.x + padding);
            currPos.y = -0.01f * (currPos.x - collliderPos.x) * (currPos.x - collliderPos.x) + 10;
            result.Add(currPos);
        }

        return result;

      
    }
}
