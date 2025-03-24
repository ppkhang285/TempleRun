using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

public class GameplayManager : MonoBehaviour
{


    public GameObject mapRoot;

    public InputManager inputManager { get; private set; }
    public MapController mapController { get; private set; }


    void Start()
    {


        Inintialize();
    }

  

    private void Inintialize()
    {

        inputManager = new InputManager();

        if (mapRoot == null)
        {
            Debug.LogError("MapRoot is null");
           
        }
        mapController = new MapController(mapRoot.transform);
    }
    
    void Update()
    {
       
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

    public void TestEditr()
    {
        Debug.Log("Test Editor");
    }
}
