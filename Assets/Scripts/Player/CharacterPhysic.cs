using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// Controls the player's movement and actions.
/// </summary>
public class CharacterPhysic 
{
    private Transform characterTransform;

    private float mass;
    private Vector3 velocity, acceleration;

    List<Vector3> forces;

    public CharacterPhysic(Transform characterTransform)
    {
        this.characterTransform = characterTransform;

        forces = new List<Vector3>();
        velocity = Vector3.zero;
        mass = Constants.CHARACTER_MASS;
    }


    public void Update()
    {
        

        // Add gravity force
        forces.Add(Vector3.down * Constants.GRAVITY * mass);

        Vector3 totalForce = Vector3.zero;
        foreach (Vector3 force in forces)
        {
            totalForce += force;
        }

        acceleration = totalForce / mass;

        if (velocity.y < 0 && CollideWithGround())
        {
            //Debug.Log("In ground");
            velocity.y = 0;
        }
        
        characterTransform.position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;

        forces.Clear();
    }

    public void AddForce(Vector3 force)
    {
        forces.Add(force);
    }

    public bool CollideWithGround()
    {
        return Physics.Raycast(characterTransform.position, Vector3.down, 1.1f);
    }

    public bool CollideLeft()
    {
        return false;
    }

    public bool CollideRight()
    {
        return false;
    }


}
