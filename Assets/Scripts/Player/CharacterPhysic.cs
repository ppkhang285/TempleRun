using System.Collections.Generic;
using UnityEngine;
using Utils;

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

        if (velocity.y >= 0)
        {
            forces.Add(Vector3.down * Constants.GRAVITY * mass);
        }
        else
        {
            forces.Add(Vector3.down * Constants.GRAVITY * mass * 2.5f);
        }

     
        Vector3 totalForce = Vector3.zero;
        foreach (Vector3 force in forces)
        {
            totalForce += force;
        }
        Vector3 acceleration = totalForce / mass;

      
        if (velocity.y < 0 && CollideWithGround())
        {
            velocity.y = 0;
            player.jumping = false;
            coyoteTimeCounter = coyoteTime; 
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

    
        characterTransform.position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;

        forces.Clear();
    }


    public void Jump()
    {
        if (coyoteTimeCounter > 0)
        {
            velocity.y = Constants.CHARACTER_JUMP_FORCE;
            coyoteTimeCounter = 0; 
            player.jumping = true;
        }
    }


    public bool CollideWithGround()
    {


        return Physics.BoxCast(characterTransform.position, Vector3.one * 4f, Vector3.down, Quaternion.identity, 1.1f);
    }

    public bool CollideLeft()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Vector3 leftVector = Constants.DIRECTION_VECTOR[Enums.MoveDirection.LEFT];
        return Physics.Raycast(characterTransform.position, leftVector, 1.1f, mask);
    }

    public bool CollideRight()
    {
        LayerMask mask = LayerMask.GetMask("WallMask");
        Vector3 rightVector = Constants.DIRECTION_VECTOR[Enums.MoveDirection.RIGHT];
        return Physics.Raycast(characterTransform.position, rightVector, 1.1f, mask);
    }
}
