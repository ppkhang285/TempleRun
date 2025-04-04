using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Utils.Enums;

public class ProgressionManager
{
    private static ProgressionManager _instance;

    public int currentCoin { get; private set; }
    public float currentDistance { get; private set; }
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

    private float BASE_SPEED = 70.0f;
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

        GameplayManager.Instance.RunCoroutine(IncreaseSpeed());
    }

    public void Init()
    {
        maxCoinSpawnInterval = 10.0f;
        minCoinSpawnInterval = 5.0f;

        maxItemSpawnInterval = 17.0f;
        minItemSpawnInterval = 10.0f;

        currentDistance = 0;
        moving_speed = BASE_SPEED;

        ResetCoinTimer();
        ResetItemTimer();
    }
    public void Reset()
    {
        spawnCoinTimer = 10;
        spawnItemTimer = 20;

        currentDistance = 0;
        moving_speed = BASE_SPEED;

        ResetCoinTimer();
        ResetItemTimer();
        StartGame();

        //GameplayManager.Instance.Stop_Coroutine(itemTimerCoroutine);
        //GameplayManager.Instance.Stop_Coroutine(coinTimerCoroutine);

    }

    public void Update()
    {
       
        currentDistance += Time.deltaTime * moving_speed * 0.1f;
        UIManager.Instance.UpdateHUDPanel(currentDistance, currentCoin);
    }
    IEnumerator IncreaseSpeed()
    {
        // Increase speed by 10% every of BASE 5 seconds
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            moving_speed += BASE_SPEED * 0.1f;
            
        }
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
        spawnItemTimer = Random.Range(minItemSpawnInterval, maxItemSpawnInterval);
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
        UIManager.Instance.UpdateHUDPanel(currentDistance, currentCoin);
        // Debug.Log($"Current coin: {currentCoin}");
    }
}
