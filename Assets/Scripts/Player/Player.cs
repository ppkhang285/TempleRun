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
 
    // Managers

    public CharacterPhysic characterPhysic { get; private set; }


    // Attributes

    [HideInInspector] public BoxCollider currentCollider { get; private set; }
    private bool canTurn = true;
    private float slidingTime = 1.0f;
    private bool isSliding = false;
    private bool isStumple = false;
    private float stumpleTime = 5.0f;
    private bool canControl = true;
   

    private void Start()
    {
       
        Initialize();
    }

    public void MyUpdate()
    {
        if (canControl) 
        {
            HandleMoving();
            HandleJump();
            HandleTurn();
            HandleSlide();
        }
        
        
        characterPhysic.Update();
    }

    private void Initialize()
    {
        characterPhysic = new CharacterPhysic(transform, this);

        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);

        currentCollider = runningCollider;
        
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

        if (InputManager.Instance.GetInput(InputAction.Jump, true))
        {

            isSliding = false;
            runningCollider.gameObject.SetActive(true);
            slidingCollider.gameObject.SetActive(false);
            currentCollider = runningCollider;

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
            currentCollider = slidingCollider;

            GameplayManager.Instance.RunCoroutine(SlideSequence());

        }
       
    }


    IEnumerator SlideSequence()
    {
        yield return new WaitForSeconds(slidingTime);

        if (characterPhysic.isJumping) yield return null ;
        isSliding = false;
        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);
        currentCollider = runningCollider;
    }

    private void HandleTurn()
    {
        if (!canTurn) return;

        if (InputManager.Instance.GetInput(InputAction.TurnLeft, false) && characterPhysic.CanTurnLeft())
        {
            Debug.Log("Turn");
            GameplayManager.Instance.ChangeDirection(true);
            
            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            transform.rotation = rotation;
            canTurn = false;
        }
        else if (InputManager.Instance.GetInput(InputAction.TurnRight, false) && characterPhysic.CanTurnRight())
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
            other.gameObject.SetActive(false);
            canTurn = true;
        }
        else if (other.CompareTag("DeathTrigger"))
        {
         //   canControl = false;
            Debug.Log("Game over");
        }
        else if (other.CompareTag("StumpleTrigger"))
        {
            other.gameObject.SetActive(false);
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
       else if (other.CompareTag("Coin"))
        {
            
            GameplayManager.Instance.coinManager.CollectCoin(other.gameObject);
        }
    }


}




