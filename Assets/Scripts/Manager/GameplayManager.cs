using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    // Gameplay attributes
    public float moving_speed { get; private set; } // Moving speed of character (moving speed of map segments)
    public MoveDirection currentDirecion { get; private set; }
    public int currentDifficulty { get; private set; }
    public Player player { get; private set; }


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

        inputManager = new InputManager();

        if (mapRoot == null)
        {
            Debug.LogError("MapRoot is null");
           
        }
        mapController = new MapController(mapRoot.transform);

        // Gameplay Attribute setting
        currentDirecion = MoveDirection.FORWARD;
        moving_speed = 5.0f;
        currentDifficulty = 1;


    }

    private void InitSpawnObject()
    {
        mapController.InitEnviroment();

        //Spawn Player
        GameObject playerObj = Instantiate(playerPrefabs, Vector3.up * 8, Quaternion.identity);
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        mapController.Update();
        player.MyUpdate();
    }

    // Setter
    public void SetMovingSpeed(float speed)
    {
        moving_speed = speed;
    }

    public void SetMoveDirection(MoveDirection direction)
    {
        currentDirecion = direction;
    }

    private void TestInput()
    {
        if (InputManager.Instance.GetInput(InputAction.TurnLeft))
        {
            Debug.Log("Turn Left");
        }
        if (InputManager.Instance.GetInput(InputAction.TurnRight))
        {
            Debug.Log("Turn Right");
        }
        if (InputManager.Instance.GetInput(InputAction.MoveLeft))
        {
            Debug.Log("Move Left");
        }
        if (InputManager.Instance.GetInput(InputAction.MoveRight))
        {
            Debug.Log("Move Right");
        }
    }

    [Button]
    public void SpawnSegment()
    {
        mapController.SpawnNewSegment();
    }
}
