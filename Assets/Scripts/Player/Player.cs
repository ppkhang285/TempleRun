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
    private float slidingTime = 1.0f;
    private bool isSliding = false;
    private bool isStumple = false;
    private float stumpleTime = 5.0f;
    private void Start()
    {
       
        Initialize();
    }

    public void MyUpdate()
    {
        HandleMoving();
        HandleJump();
        HandleTurn();
        HandleSlide();
        
        characterPhysic.Update();
    }

    private void Initialize()
    {
        characterPhysic = new CharacterPhysic(transform, this);

        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);
        
    }

    private void HandleMoving()
    {
        Vector3 moveDirection = Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];

        if (InputManager.Instance.GetInput(InputAction.MoveLeft) && !characterPhysic.CollideLeft() )
        {
            
            Quaternion rotation = Constants.ROTATION_VECTOR[Direction.LEFT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
        else if (InputManager.Instance.GetInput(InputAction.MoveRight) && !characterPhysic.CollideRight())
        {
            Quaternion rotation = Constants.ROTATION_VECTOR[Direction.RIGHT];

            transform.position += rotation * moveDirection * Constants.CHARACTER_VERTICAL_VELOCITY * Time.deltaTime;
        }
    }

    private void HandleJump()
    {

        if (InputManager.Instance.GetInput(InputAction.Jump, true) && !isSliding)
        {
          
            characterPhysic.Jump();
   
        }

        
    }

    private void HandleSlide()
    {
        if (InputManager.Instance.GetInput(InputAction.Slide, true) && !characterPhysic.isJumping)
        {

            isSliding = true;
            runningCollider.gameObject.SetActive(false);
            slidingCollider.gameObject.SetActive(true);

            GameplayManager.Instance.RunCoroutine(SlideSequence());

        }
       
    }


    IEnumerator SlideSequence()
    {
        yield return new WaitForSeconds(slidingTime);

        isSliding = false;
        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);
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


    IEnumerator StumpleCooldown()
    {
        yield return new WaitForSeconds(stumpleTime);
        isStumple = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            
            GameplayManager.Instance.SpawnSegment();
            canTurn = true;
        }
        else if (other.CompareTag("DeathTrigger"))
        {
            Debug.Log("Game over");
        }
        else if (other.CompareTag("StumpleTrigger"))
        {
            if (isStumple)
            {
                Debug.Log("Game Over");
            }

            else
            {
                isStumple = true;
                GameplayManager.Instance.RunCoroutine(StumpleCooldown());
            }
        }
    }


}




