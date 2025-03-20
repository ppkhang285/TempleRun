using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

public class GameplayManager : MonoBehaviour
{


    public InputManager inputManager;



    void Start()
    {


        Inintialize();
    }

  

    private void Inintialize()
    {


        inputManager = new InputManager();
    }
    
    void Update()
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
}
