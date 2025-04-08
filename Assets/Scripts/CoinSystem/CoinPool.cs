using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool
{
    private GameObject coinPrefab;

    private GameObject poolRoot;

    private Queue<GameObject> coinList;

    public CoinPool(GameObject coinPrefab)
    {
        this.coinPrefab = coinPrefab;

        Initialize();
    }

    public void Reset()
    {
        while (coinList.Count > 0)
        {
            GameObject obj = coinList.Dequeue();
            GameObject.Destroy(obj);
        }

    }
    public void Initialize()
    {
        poolRoot = new GameObject("CoinPoolRoot");
        poolRoot.transform.position = new Vector3(100, -1000, 0);

        coinList = new Queue<GameObject>();
    }

    public GameObject GetObject()
    {
        if (coinList.Count <= 0)
        {
            GameObject coinObj = GameObject.Instantiate(coinPrefab);
            return coinObj;
        }
        else
        {
            GameObject coinObj = coinList.Dequeue();

            coinObj.SetActive(true);
            coinObj.transform.SetParent(null);

            return coinObj;
        }
        
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolRoot.transform);
        obj.transform.localPosition = Vector3.zero;

        coinList.Enqueue(obj);
    }

}
