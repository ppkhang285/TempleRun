using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using static Utils.Enums;


/// <summary>
/// Controls the player's movement and actions.
/// </summary>
public class CharacterPhysic
{
    private Transform characterTransform;
    private Player player;

    private float mass;
    private Vector3 velocity;
    private List<Vector3> forces;

    private float coyoteTime = 0.1f; 
    private float coyoteTimeCounter = 0f;

    // for test only
    private float pullDownForce = 30f;
    private float jumpForce = 25f;
    private float jumpTime = 0.5f;

    public bool isJumping { get; private set; } = false;
    private bool isFallingDown = false;


    public CharacterPhysic(Transform characterTransform, Player player)
    {
        this.characterTransform = characterTransform;
        this.player = player;

        forces = new List<Vector3>();
        velocity = Vector3.zero;
        mass = Constants.CHARACTER_MASS;
    }

    public void Update()
    {
        if (coyoteTimeCounter > 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }


       // Debug.Log($"isJumping: {isJumping} isFallingDown: {isFallingDown}");
        if (isFallingDown == isJumping)
        {
            PullingDown();
        }
        else
        if (isJumping && !isFallingDown)
        {
            JumpingUp();
        }
    }
    public void JumpingUp()
    {
           
        characterTransform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        
    }
    public void PullingDown()
    {
        if (CollideWithGround())
        {
            if (isJumping && isFallingDown)
            {
                isJumping = false;
                isFallingDown = false;
            }
            
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            characterTransform.Translate(Vector3.down * pullDownForce * Time.deltaTime);
        }
    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(jumpTime);
        isFallingDown = true;
 
    }

    public void Jump()
    {
        if (coyoteTimeCounter > 0 && !isJumping)
        {
            isJumping = true;
            coyoteTimeCounter = 0;
            GameplayManager.Instance.RunCoroutine(JumpSequence());
        }
    }


    public bool CollideWithGround()
    {
        LayerMask mask = LayerMask.GetMask("GroundMask");

        return Physics.BoxCast(characterTransform.position, Vector3.one * 4f, Vector3.down, Quaternion.identity, 1.1f, mask);
    }

    public bool CollideLeft()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");

        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, true);
        Vector3 leftVector = Constants.DIRECTION_VECTOR[turnDirect];
        return Physics.Raycast(characterTransform.position, leftVector, 1.1f, mask);
    }

    public bool CollideRight()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, false);


        Vector3 rightVector = Constants.DIRECTION_VECTOR[turnDirect];
        return Physics.Raycast(characterTransform.position, rightVector, 1.1f, mask);
    }
 
    public bool CanTurnLeft()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, true);
        Vector3 leftVector = Constants.DIRECTION_VECTOR[turnDirect];

   
        return !Physics.Raycast(characterTransform.position, leftVector, 70f, mask);
    }

    public bool CanTurnRight()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, false);

        Vector3 rightVector = Constants.DIRECTION_VECTOR[turnDirect];

        return !Physics.Raycast(characterTransform.position, rightVector, 70f, mask);
    }

   

    
}
