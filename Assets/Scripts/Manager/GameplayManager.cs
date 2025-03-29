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

    // Managers
    public InputManager inputManager { get; private set; }
    public MapController mapController { get; private set; }


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
        inputManager = new InputManager();

        if (mapRoot == null)
        {
            Debug.LogError("MapRoot is null");
           
        }
        mapController = new MapController(mapRoot.transform);

        // Gameplay Attribute setting
        currentDirecion = Direction.FORWARD;
        moving_speed = 40.0f;
        currentDifficulty = 1;


    }

    private void InitSpawnObject()
    {
        mapController.InitEnviroment();

        //Spawn Player
        GameObject playerObj = Instantiate(playerPrefabs, Vector3.up * 10, Quaternion.identity);
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {

        if (gameState == GameState.Playing)
        {
            mapController.Update();
            player.MyUpdate();
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

    // Chuyen vo Utils
    public Direction TurnDirection(Direction currDirect, bool isTurnLeft)
    {
        switch (currDirect)
        {
            case Direction.FORWARD:
                currDirect = isTurnLeft ? Direction.LEFT : Direction.RIGHT;
                break;

            case Direction.BACKWARD:
                currDirect = isTurnLeft ? Direction.RIGHT : Direction.LEFT;
                break;
            case Direction.LEFT:
                currDirect = isTurnLeft ? Direction.BACKWARD : Direction.FORWARD;
                break;
            case Direction.RIGHT:
                currDirect = isTurnLeft ? Direction.FORWARD : Direction.BACKWARD;
                break;
        }

        return currDirect;
    }
    public void ChangeDirection(bool isTurnLeft)
    {
        
        currentDirecion = UtilMethods.TurnDirection(currentDirecion, isTurnLeft);
    }

   
}
