using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Utils.Enums;

public class ProgressionManager
{
    private static ProgressionManager _instance;

    public int currentCoin { get; private set; }
    public float currentRunningDistance { get; private set; }
    public int currentDifficulty { get; private set; }
    public float moving_speed { get; private set; }

    public float spawnCoinTimer { get; private set; }
    public float spawnItemTimer { get; private set; }

    private float maxCoinSpawnInterval;
    private float minCoinSpawnInterval;

    private float maxItemSpawnInterval;
    private float minItemSpawnInterval;

    private bool coinSpawned;
    private bool itemSpawned;

    private Coroutine itemTimerCoroutine;
    private Coroutine coinTimerCoroutine;


    public static ProgressionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ProgressionManager();
            }
            return _instance;
        }
    }

    public ProgressionManager()
    {
        currentCoin = 0;

        Init();
        
        
    }

    public void StartGame()
    {
        itemTimerCoroutine = GameplayManager.Instance.RunCoroutine(RunItemTimer());
        coinTimerCoroutine =  GameplayManager.Instance.RunCoroutine(RunCointimer());
    }

    public void Init()
    {
        maxCoinSpawnInterval = 10.0f;
        minCoinSpawnInterval = 5.0f;

        maxItemSpawnInterval = 30.0f;
        minItemSpawnInterval = 20.0f;
    }
    public void Reset()
    {
        spawnCoinTimer = 10;
        spawnItemTimer = 20;

        currentRunningDistance = 0;
        moving_speed = 70.0f;

        StartGame();

        //GameplayManager.Instance.Stop_Coroutine(itemTimerCoroutine);
        //GameplayManager.Instance.Stop_Coroutine(coinTimerCoroutine);

    }

    public void Update()
    {
        currentRunningDistance += Time.deltaTime * moving_speed;

    }


    IEnumerator RunCointimer()
    {
        coinSpawned = false;
        while (spawnCoinTimer > 0)
        {
            spawnCoinTimer -= Time.deltaTime;
            yield return null;
        }


    }
    IEnumerator RunItemTimer()
    {
        itemSpawned = false;
        while (spawnItemTimer > 0)
        {
            spawnItemTimer -= Time.deltaTime;
            yield return null;
        }


    }

    public void ResetCoinTimer()
    {
        spawnCoinTimer = Random.Range(minCoinSpawnInterval, maxCoinSpawnInterval);
        //GameplayManager.Instance.Stop_Coroutine(coinTimerCoroutine);
        coinTimerCoroutine =  GameplayManager.Instance.RunCoroutine(RunCointimer());
    }

    public void ResetItemTimer()
    {
        spawnCoinTimer = Random.Range(minItemSpawnInterval, maxItemSpawnInterval);
        //GameplayManager.Instance.Stop_Coroutine(itemTimerCoroutine);
        itemTimerCoroutine = GameplayManager.Instance.RunCoroutine(RunItemTimer());
    }

    public bool CanSpawnCoin(MapSegment segment)
    {
        List<SegmentType> validSpawn = new List<SegmentType>()
            { SegmentType.Straight,
              SegmentType.Slide,
              SegmentType.Jump
            };
        if (!validSpawn.Contains(segment.segmentType)) return false;
        if (itemSpawned) return false;
        return spawnCoinTimer <= 0;
    }

    public bool CanSpawnItem(MapSegment segment)
    {
        List<SegmentType> validSpawn = new List<SegmentType>()
            { SegmentType.Straight,

            };
        if (!validSpawn.Contains(segment.segmentType)) return false;
        if (coinSpawned) return false;
        return spawnItemTimer <= 0;
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
