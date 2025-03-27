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
    public bool jumping;


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
        characterPhysic = new CharacterPhysic(transform, this);
        jumping = false; 
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
        if (characterPhysic.CollideWithGround())
        {
            jumping = false;
        }

        if (!jumping && InputManager.Instance.GetInput(InputAction.Jump, true))
        {
          
            characterPhysic.Jump();
            jumping = true;
        }

        
    }

    private void HandleSlide()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            GameplayManager.Instance.SpawnSegment();
        }
    }
}




