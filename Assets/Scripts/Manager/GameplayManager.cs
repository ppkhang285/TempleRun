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
    public CoinManager coinManager { get; private set; }
    public CameraManager cameraManager { get; private set; }

    // Global attributes
    public float moving_speed { get; private set; } // Moving speed of character (moving speed of map segments)
    public Direction currentDirecion { get; private set; }
    public int currentDifficulty { get; private set; }
    public Player player { get; private set; }

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
        coinManager = new CoinManager();
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

    }
   

    private void InitSpawnObject()
    {
        mapController.InitEnviroment();

        //Spawn Player
        GameObject playerObj = Instantiate(playerPrefabs, Vector3.up * 10, Quaternion.identity);
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
    }

    [Button]
    public void SpawnSegment()
    {
        mapController.SpawnNewSegment();
    }

    [Button]
    public void StartGame()
    {
        if (gameState == GameState.MainMenu)
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


    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }


    
    public void ChangeDirection(bool isTurnLeft)
    {
        
        currentDirecion = UtilMethods.TurnDirection(currentDirecion, isTurnLeft);
    }

   
}
