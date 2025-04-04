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
    public GameObject canvas;
   
    

    // Managers
    public InputManager inputManager { get; private set; }
    public MapController mapController { get; private set; }
    public PowerUpManager powerUpManager { get; private set; }
    public ProgressionManager progressionManager { get; private set; }
    public CameraManager cameraManager { get; private set; }
    public UIManager uiManager { get; private set; }
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

        inputManager = InputManager.Instance;
        mapController = new MapController(mapRoot.transform);
        powerUpManager = new PowerUpManager();
        progressionManager = ProgressionManager.Instance;
        cameraManager = new CameraManager(CameraRoot, playerRoot);
        uiManager =  UIManager.Instance;


        // Gameplay Attribute setting
        currentDirecion = Direction.FORWARD;
        moving_speed = 70.0f;
        currentDifficulty = 1;


    }


    void Update()
    {
        if (InputManager.Instance.GetInput(InputAction.Pause, true))
        {
            PauseGame();
            
        }
        if (gameState == GameState.Playing)
        {
            mapController.Update();
            player.MyUpdate();
            cameraManager.Update();
            progressionManager.Update();
        }



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



    // Setter
    public void SetMovingSpeed(float speed)
    {
        moving_speed = speed;
    }

    public void SetMoveDirection(Direction direction)
    {
        currentDirecion = direction;
    }


 
    public void Test()
    {
         powerUpManager.ActivatePowerUp();

        //ToggleInvisibleState();
    }

    public void SpawnSegment()
    {
        mapController.SpawnNewSegment();
    }


    public void StartGame()
    {
        if (gameState == GameState.MainMenu || gameState == GameState.GameOver )
        {
            gameState = GameState.Playing;

            cameraManager.GameplayCamera();
        }
    }

 
    public void PauseGame()
    {
        if (gameState == GameState.Playing)
        {
            uiManager.OnPauseGame();
            gameState = GameState.Paused;
        }


    }


    public void ContinueGame()
    {
        if (gameState == GameState.Paused)
        {
            // Call UI Countdown  (Coroutine)-> Play
            
            StartCoroutine(ContinueCountDown());
            
        }
    }

    IEnumerator ContinueCountDown()
    {
        int time = 3;

        uiManager.ShowCountdownPanel(true);
        while (time > 0)
        {
            uiManager.UpdateCountDownPanel(time);
            yield return new WaitForSeconds(1);
            time--;
        }
        gameState = GameState.Playing;
        uiManager.OnContinueGame();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
      
        gameState = GameState.GameOver;
        uiManager.OnGameOver();
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
        uiManager.OnMainMenu();

    }
     IEnumerator WaitForStart()
    {
        player.Reset();
        //InitSpawnObject();
       
        yield return new WaitForSeconds(0.5f);
        StartGame();
    }



    public void RestartGame()
    {
        Reset();
        uiManager.OnRestartGame();
        StartCoroutine(WaitForStart());
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
