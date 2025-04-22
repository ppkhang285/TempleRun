using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;
using XLua;
using static GameUtils.Enums;



public class Player : MonoBehaviour
{

    // Inspector
    [SerializeField] private BoxCollider runningCollider;
    [SerializeField] private BoxCollider slidingCollider;
    [SerializeField] private GameObject shadowProjectorObj;
    [SerializeField] private Animator animatorController;

    // Managers

    public CharacterPhysic characterPhysic { get; private set; }
    


    // Attributes

    [HideInInspector] public BoxCollider currentCollider { get; private set; }
    private int turnCount = 0;
    private float slidingTime = 1.0f;

    private bool isSliding = false;
    private bool isStumple = false;
    private float stumpleTime = 5.0f;
    private bool canControl = true;

    private const int TURN_COUNT = 2;

    // Coroutine
    private Coroutine slideSequenceCoroutine;
    private Coroutine rotateSmoothlyCoroutine;
    private Coroutine stumpleCoroutine;

    // Lua
    private LuaTable script;
    private LuaFunction startFunc;
    private LuaFunction onTriggerEnterFunc;

    private struct InspectorObjects
    {
        public BoxCollider runningCollider;
        public BoxCollider slidingCollider;
        public GameObject shadowProjectorObj;
        public Animator animatorController;
    }

    private void Start()
    {

        //Initialize();
        //animatorController.Play("Run");

        script = LuaManager.Instance.LoadScript("Player");
        startFunc = script.Get<LuaFunction>("Start");
        onTriggerEnterFunc = script.Get<LuaFunction>("OnTriggerEnter");


        InspectorObjects inspectorObjects = new InspectorObjects
        {
            runningCollider = runningCollider,
            slidingCollider = slidingCollider,
            shadowProjectorObj = shadowProjectorObj,
            animatorController = animatorController
        };

        startFunc?.Call(script, transform, inspectorObjects);


    }




    //public void MyUpdate()
    //{

   
        //if (canControl)
        //{
        //    HandleMoving();
        //    HandleJump();
        //    //if (GameplayManager.Instance.inInvisibleState)
        //    //{
        //    //    HandleAutoTurn();
        //    //}
        //    //else
        //    //{
        //    //    HandleTurn();
        //    //}
        //    HandleAutoTurn();
        //    HandleSlide();
        //}

        //HandleShadow();
        //characterPhysic.Update();

    //}

    public void Say()
    {
        Debug.Log("Hello Lua!");
    }

    private void Initialize()
    {
        characterPhysic = new CharacterPhysic(transform, this);

        //InitLua();

        Reset();

    }




    private void HandleShadow()
    {
        LayerMask mask = LayerMask.GetMask("GroundMask");
        float maxDistance = 100f;
        float leftOffset = 4;
        Vector3 leftVector = Constants.DIRECTION_VECTOR[ UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, true)];
        Vector3 pos = transform.position + leftVector * leftOffset;


        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, maxDistance, mask))
        {
            shadowProjectorObj.transform.position =
                new Vector3(pos.x, transform.position.y - hit.distance + 2, pos.z);
        }
        else
        {
            shadowProjectorObj.transform.position = transform.position;
        }
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
            animatorController.SetBool("isSliding", false);

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
            animatorController.SetBool("isSliding", true);

            runningCollider.gameObject.SetActive(false);
            slidingCollider.gameObject.SetActive(true);
            currentCollider = slidingCollider;

            if (!slidingCollider.enabled)
            {
                slidingCollider.enabled = true;
                Debug.Log("Sliding collider was disabled, enabling it now");
            }

            if (slideSequenceCoroutine != null)
            {
                GameplayManager.Instance.Stop_Coroutine(slideSequenceCoroutine);
            }
            slideSequenceCoroutine = GameplayManager.Instance.RunCoroutine(SlideSequence());
            Physics.SyncTransforms();

        }
       
    }


    IEnumerator SlideSequence()
    {
        yield return new WaitForSeconds(slidingTime);

        if (characterPhysic.isJumping) yield return null ;
        isSliding = false;
        animatorController.SetBool("isSliding", false);

        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);
        currentCollider = runningCollider;
    }

    private void HandleTurn()
    {
        if (turnCount > 0) return;

        if (InputManager.Instance.GetInput(InputAction.TurnLeft, false) && characterPhysic.CanTurnLeft())
        {
          
            GameplayManager.Instance.ChangeDirection(true);
            //GameplayManager.Instance.cameraManager.Rotate();
            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            //transform.rotation = rotation;
            rotateSmoothlyCoroutine =  GameplayManager.Instance.RunCoroutine(RotateSmoothly(rotation));
            turnCount = TURN_COUNT;
        }
        else if (InputManager.Instance.GetInput(InputAction.TurnRight, false) && characterPhysic.CanTurnRight())
        {
            
            GameplayManager.Instance.ChangeDirection(false);
            //GameplayManager.Instance.cameraManager.Rotate();
            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            rotateSmoothlyCoroutine = GameplayManager.Instance.RunCoroutine(RotateSmoothly(rotation));
            //transform.rotation = rotation;
            turnCount = TURN_COUNT;

        }
    }

    IEnumerator RotateSmoothly(Quaternion targetRotation)
    {
        float rotateSpeed = 10.0f;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            if (GameplayManager.Instance.IsPlaying())
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
            
            yield return null; 
        }
    }

    private void HandleAutoTurn()
    {
        if (turnCount > 0) return;

        if (characterPhysic.CanTurnLeft())
        {

            GameplayManager.Instance.ChangeDirection(true);

            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            rotateSmoothlyCoroutine = GameplayManager.Instance.RunCoroutine(RotateSmoothly(rotation));
            turnCount = TURN_COUNT;
        }
        else if (characterPhysic.CanTurnRight())
        {

            GameplayManager.Instance.ChangeDirection(false);

            Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
            rotateSmoothlyCoroutine = GameplayManager.Instance.RunCoroutine(RotateSmoothly(rotation));
            turnCount = TURN_COUNT;

        }
    }

    IEnumerator StumpleCooldown()
    {
        yield return new WaitForSeconds(stumpleTime);
        isStumple = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        onTriggerEnterFunc?.Call(script, other);

        // if (other.CompareTag("SpawnTrigger"))
        // {
        //    // Debug.Log("Spawn");
        //     GameplayManager.Instance.SpawnSegment();
        //     other.gameObject.SetActive(false);
        //     turnCount -= 1;
        // }
        // else if (other.CompareTag("InstantDeathTrigger"))
        // {

        //     OnDeath();
        // }
        // else if (other.CompareTag("DeathTrigger") && !GameplayManager.Instance.inInvisibleState)
        // {

        //     OnDeath();
        // }
        // else if (other.CompareTag("StumpleTrigger"))
        // {
        //     other.gameObject.SetActive(false);
        //     if (isStumple)
        //     {
        //         OnDeath();
        //     }

        //     else
        //     {
        //         isStumple = true;
        //         stumpleCoroutine = GameplayManager.Instance.RunCoroutine(StumpleCooldown());
        //     }
        // }
        //else if (other.CompareTag("Coin"))
        // {

        //     GameplayManager.Instance.progressionManager.CollectCoin(other.gameObject);
        // }
        // else if (other.CompareTag("Item"))
        // {
        //     OnCollidWithItem(other);
        // }
    }

    private void OnCollidWithItem(Collider other)
    {

        GameplayManager.Instance.mapController.itemController.PickupItem(other.gameObject);
    }

    //private void OnCollidWithStumpleTrigger(Collider other)
    //{

    //}
    //private void OnCollideWithCoin((Collider other)
    //{

    //}

    private void OnDeath()
    {

        gameObject.SetActive(false);
        GameplayManager.Instance.GameOver();
    }

    public void Reset()
    {
        
        characterPhysic.Reset();

        transform.position = GameplayManager.Instance.plaerSpawnPoint;

        Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
        transform.rotation = rotation;

        runningCollider.gameObject.SetActive(true);
        slidingCollider.gameObject.SetActive(false);

        currentCollider = runningCollider;

        turnCount = 0;
        slidingTime = 1.0f;
        isSliding = false;


        isStumple = false;
        stumpleTime = 5.0f;
        canControl = true;
        //script.Get<LuaFunction>("Reset")?.Call();

        gameObject.SetActive(true);
        animatorController.SetBool("isSliding", false);
        //GameplayManager.Instance.Stop_Coroutine(stumpleCoroutine);
        //GameplayManager.Instance.Stop_Coroutine(rotateSmoothlyCoroutine);
        //GameplayManager.Instance.Stop_Coroutine(slideSequenceCoroutine);
    }


}




