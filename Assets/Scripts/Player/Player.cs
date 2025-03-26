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


    // Managers

    public CharacterPhysic characterPhysic { get; private set; }


    // Attributes

    private BoxCollider currentCollider;
    private bool jumped;


    private void Start()
    {
       
        Initialize();
    }

    public void MyUpdate()
    {
        HandleMoving();
        HandleJump();
        
        characterPhysic.Update();
    }

    private void Initialize()
    {
        characterPhysic = new CharacterPhysic(transform);
        jumped = false; 
    }

    private void HandleMoving()
    {
        Vector3 moveDirection = Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];

        if (InputManager.Instance.GetInput(InputAction.MoveLeft))
        {
            
            Quaternion rotation = Constants.ROTATION_VECTOR[MoveDirection.LEFT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
        else if (InputManager.Instance.GetInput(InputAction.MoveRight))
        {
            Quaternion rotation = Constants.ROTATION_VECTOR[MoveDirection.RIGHT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (characterPhysic.CollideWithGround())
        {
            jumped = false;
        }

        if (!jumped && InputManager.Instance.GetInput(InputAction.Jump))
        {
            characterPhysic.AddForce(Vector3.up * Constants.CHARACTER_JUMP_FORCE);
            jumped = true;
        }

        
    }



    
}
