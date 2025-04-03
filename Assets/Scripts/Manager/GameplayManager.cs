using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.Enums;



public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    // Inspector
    public GameObject mapRoot;
    public GameObject playerPrefabs;
    public GameObject playerRoot;
    public GameObject coinPrefab;
    public GameObject CameraRoot;

   
    

    // Managers
    public InputManager inputManager { get; private set; }
    public MapController mapController { get; private set; }
    public PowerUpManager powerUpManager { get; private set; }
    public ProgressionManager progressionManager { get; private set; }
    public CameraManager cameraManager { get; private set; }

    // Global attributes
    public float moving_speed { get; private set; } // Moving speed of character (moving speed of map segments)
    public Direction currentDirecion { get; private set; }

    public Vector3 plaerSpawnPoint = Vector3.up * 10;
    public int currentDifficulty { get; private set; }
    public Player player { get; private set; }

    public bool inInvisibleState { get; private set; }
    public GameState gameState { get; private set; }
    //

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        SpawnPlayer();
        Inintialize();
        InitSpawnObject();
    }

    private void Inintialize()
    {

        //Cursor.visible = false;
        gameState = GameState.MainMenu;

        if (mapRoot == null)
        {
            Debug.LogError("MapRoot is null");

        }

        inputManager = new InputManager();
        mapController = new MapController(mapRoot.transform);
        powerUpManager = new PowerUpManager();
        progressionManager = new ProgressionManager();
        cameraManager = new CameraManager(CameraRoot, playerRoot);
        // Gameplay Attribute setting
        currentDirecion = Direction.FORWARD;
        moving_speed = 70.0f;
        currentDifficulty = 1;


    }

    private void ResetToDefault()
    {
        currentDirecion = Direction.FORWARD;
        moving_speed = 70.0f;
        currentDifficulty = 1;
        cameraManager.DefaultCamera();
        inInvisibleState = false;
    }

    public void ToggleInvisibleState()
    {
        inInvisibleState = !inInvisibleState;
    }

    public void ToggleInvisibleState(bool isActive)
    {
        inInvisibleState = isActive;
    }


    private void InitSpawnObject()
    {
        
        mapController.InitEnviroment();

        
    }
    private void SpawnPlayer()
    {
        //Spawn Player
        GameObject playerObj = Instantiate(playerPrefabs, plaerSpawnPoint, Quaternion.identity);
        //playerObj.transform.position = Vector3.zero;
        playerObj.transform.SetParent(playerRoot.transform, true);

        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameState == GameState.Playing)
        {
            mapController.Update();
            player.MyUpdate();
            cameraManager.Update();
        }
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameState = GameState.GameOver;
    }


    // Setter
    public void SetMovingSpeed(float speed)
    {
        moving_speed = speed;
    }

    public void SetMoveDirection(Direction direction)
    {
        currentDirecion = direction;
    }


    [Button]
    public void Test()
    {
         powerUpManager.ActivatePowerUp();

        //ToggleInvisibleState();
    }

    [Button]
    public void SpawnSegment()
    {
        mapController.SpawnNewSegment();
    }

    [Button]
    public void StartGame()
    {
        if (gameState == GameState.MainMenu || gameState == GameState.GameOver )
        {
            gameState = GameState.Playing;

            cameraManager.GameplayCamera();
        }
    }

    [Button]
    public void PauseGame()
    {
        if (gameState == GameState.Playing)
        {
            gameState = GameState.Paused;
        }


    }

    [Button]
    public void ContinueGame()
    {
        if (gameState == GameState.Paused)
        {

            // Call UI Countdown  (Coroutine)-> Play
            gameState = GameState.Playing;
        }
    }


    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void Stop_Coroutine(Coroutine coroutine)
    {
        if (coroutine == null) return;
        StopCoroutine(coroutine);
    }

    public void ChangeDirection(bool isTurnLeft)
    {
        
        currentDirecion = UtilMethods.TurnDirection(currentDirecion, isTurnLeft);
    }
    [Button]

    public void Reset()
    {
        gameState = GameState.GameOver;
        StopAllCoroutines();

        //
        currentDirecion = Direction.FORWARD;

        
        cameraManager.Reset();
        mapController.Reset();
        progressionManager.Reset();

        //
        InitSpawnObject();

        StartCoroutine(WaitForStart());
    
    }
     IEnumerator WaitForStart()
    {
        player.Reset();
        //InitSpawnObject();
       
        yield return new WaitForSeconds(0.5f);
        StartGame();
    }

}
