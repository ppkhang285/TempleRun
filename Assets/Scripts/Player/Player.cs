using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.Enums;

public class Player : MonoBehaviour
{

    // Inspector
    [SerializeField] private BoxCollider runningCollider;
    [SerializeField] private BoxCollider slidingCollider;

    // Event
    public static event Action onTurnLeft;
    public static event Action onTurnRight;


    // Managers

    public CharacterPhysic characterPhysic { get; private set; }


    // Attributes

    private BoxCollider currentCollider;
    private bool canTurn = true;


    private void Start()
    {
       
        Initialize();
    }

    public void MyUpdate()
    {
        HandleMoving();
        HandleJump();
        HandleTurn();
        
        characterPhysic.Update();
    }

    private void Initialize()
    {
        characterPhysic = new CharacterPhysic(transform, this);

        runningCollider.enabled = true;
        slidingCollider.gameObject.SetActive(false);
    }

    private void HandleMoving()
    {
        Vector3 moveDirection = Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];

        if (InputManager.Instance.GetInput(InputAction.MoveLeft) && !characterPhysic.CollideLeft() )
        {
            
            Quaternion rotation = Constants.ROTATION_VECTOR[MoveDirection.LEFT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
        else if (InputManager.Instance.GetInput(InputAction.MoveRight) && !characterPhysic.CollideRight())
        {
            Quaternion rotation = Constants.ROTATION_VECTOR[MoveDirection.RIGHT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
    }

    private void HandleJump()
    {

        if (InputManager.Instance.GetInput(InputAction.Jump, true))
        {
          
            characterPhysic.Jump();
   
        }

        
    }

    private void HandleSlide()
    {

    }

    private void HandleTurn()
    {
        if (!canTurn) return;

        if (InputManager.Instance.GetInput(InputAction.TurnLeft, true) && characterPhysic.CanTurnLeft())
        {
            GameplayManager.Instance.ChangeDirection(true);
            
            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            transform.rotation = rotation;
            canTurn = false;
        }
        else if (InputManager.Instance.GetInput(InputAction.TurnRight, true) && characterPhysic.CanTurnRight())
        {
            GameplayManager.Instance.ChangeDirection(false);

            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            transform.rotation = rotation;
            canTurn = false;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            GameplayManager.Instance.SpawnSegment();
            canTurn = true;
        }
    }
}




