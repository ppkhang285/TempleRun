using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{

    public int currentCoin { get; private set; }
    public float spawnTimer { get; private set; }

    private float maxSpawnInterval;
    private float minSpawnInterval;


    public CoinManager()
    {
        currentCoin = 0;
        maxSpawnInterval = 10.0f;
        minSpawnInterval = 5.0f;
        spawnTimer = 10;

        GameplayManager.Instance.RunCoroutine(Runtimer());
    }

    


    IEnumerator Runtimer()
    {
        while (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            yield return null;
        }


    }

    public void ResetTimer()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        GameplayManager.Instance.RunCoroutine(Runtimer());
    }
    public bool CanSpawnCoin()
    {
        return spawnTimer <= 0;
    }

    public void CollectCoin(GameObject coinObj)
    {
        GameplayManager.Instance.mapController.coinSpawner.DespawnCoin(coinObj);

        GainCoin(1);
    }

    public void GainCoin(int coin)
    {
        
        currentCoin += coin;
       // Debug.Log($"Current coin: {currentCoin}");
    }
}
