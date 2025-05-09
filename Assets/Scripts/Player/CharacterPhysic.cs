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

    //private float mass;
    //private Vector3 velocity;
    //private List<Vector3> forces;

    private float coyoteTime = 0.1f; 
    private float coyoteTimeCounter = 0f;

    // for test only
    private float pullDownForce = 80f;
    private float jumpForce = 60f;
    private float jumpTime = 0.5f;
    private float jumpHeight = 25;


    public bool isJumping { get; private set; } = false;
    private bool isFallingDown = false;

    private Coroutine jumpCoroutine;

    public CharacterPhysic(Transform characterTransform, Player player)
    {
        this.characterTransform = characterTransform;
        this.player = player;

        //forces = new List<Vector3>();
        //velocity = Vector3.zero;
        //mass = Constants.CHARACTER_MASS;
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
           
        //characterTransform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        characterTransform.position += Vector3.up * jumpForce * Time.deltaTime;
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
            //characterTransform.Translate(Vector3.down * pullDownForce * Time.deltaTime);
            characterTransform.position += Vector3.down * pullDownForce * Time.deltaTime;
        }
    }

    IEnumerator JumpSequence()
    {
        float baseHeight = characterTransform.position.y;
        while (characterTransform.position.y < baseHeight + jumpHeight)
        {
            //characterTransform.position += Vector3.up * jumpForce * Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(jumpTime);
        isFallingDown = true;
 
    }

    public void Jump()
    {
        if (coyoteTimeCounter > 0 && !isJumping)
        {
            isJumping = true;
            coyoteTimeCounter = 0;
            jumpCoroutine = GameplayManager.Instance.RunCoroutine(JumpSequence());
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
        float rayDistance = 2.1f + player.currentCollider.size.z /2;

        return Physics.Raycast(characterTransform.position, leftVector, rayDistance, mask);
    }

    public bool CollideRight()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, false);

        Vector3 rightVector = Constants.DIRECTION_VECTOR[turnDirect];

        float rayDistance = 2.1f + player.currentCollider.size.z / 2;

        return Physics.Raycast(characterTransform.position, rightVector, rayDistance, mask);
    }
 
    public bool CanTurnLeft()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, true);
        Vector3 leftVector = Constants.DIRECTION_VECTOR[turnDirect];
        Vector3 rayPos = characterTransform.position;

        return !Physics.BoxCast(characterTransform.position, player.currentCollider.size + Vector3.one, leftVector, Quaternion.identity,50.0f, mask);

        //return !Physics.Raycast(rayPos, leftVector, 70f, mask);
    }

    public bool CanTurnRight()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Direction turnDirect = UtilMethods.TurnDirection(GameplayManager.Instance.currentDirecion, false);

        Vector3 rightVector = Constants.DIRECTION_VECTOR[turnDirect];
        Vector3 rayPos = characterTransform.position;

        return !Physics.BoxCast(characterTransform.position, player.currentCollider.size +Vector3.one , rightVector, Quaternion.identity, 50.0f, mask);

      //  return !Physics.Raycast(rayPos, rightVector, 40f, mask);
    }

   
    public void Reset()
    {
        isJumping = false;
        isFallingDown = false;
        coyoteTimeCounter = 0f;

    //GameplayManager.Instance.Stop_Coroutine(jumpCoroutine);
}
    
}
