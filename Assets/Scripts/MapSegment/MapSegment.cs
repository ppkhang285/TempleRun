using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.Enums;
public class MapSegment 
{
    public SegmentType segmentType {  get; private set; }
    public MapBiome biome { get; private set; }
    public Transform segmentTransform { get; private set; }
    public bool canSpawnNext; // Can spawn next segment link with this segment
    public Direction direction; // Directioon of this Segment



    public MapSegment(SegmentType segmentType, MapBiome biome, Transform transform, Direction direction)
    {
        this.segmentType = segmentType;
        this.segmentTransform = transform;
        this.direction = direction;
        canSpawnNext = true;
    }


   

    public void MoveSegment(float speed, Vector3 direction)
    {
        segmentTransform.position += direction * speed * Time.deltaTime;
    }

    public void OnDestroy()
    {
        if (segmentTransform != null)
        {
            GameObject.Destroy(segmentTransform.gameObject);
        }
        
    }

    public void FlagNextSpawn()
    {
        canSpawnNext = false;
    }


    public Vector3 GetNeighborPos(GameObject newPref, bool getTurnBothLeft = false)
    {

        // Change to -> Get size from Collider

        Vector3 newSize = newPref.transform.GetChild(0).Find("sizeObj").GetComponent<BoxCollider>().size;
        Vector3 lastSize = this.segmentTransform.GetChild(0).Find("sizeObj").GetComponent<BoxCollider>().size;

        Vector3 lastPosition = this.segmentTransform.position;

        Direction nextDirection = GetNeighborDirection(getTurnBothLeft);

        
        
        

        Vector3 directionVector = Constants.DIRECTION_VECTOR[nextDirection];

        Vector3 newPosition = lastPosition + directionVector * (newSize.x + lastSize.x) / 2;
        newPosition.y = 0;

        return newPosition;
    }


    public Direction GetNeighborDirection(bool getTurnBothLeft = false)
    {
        Direction nextDirection = Direction.NULL;
        if (segmentType == SegmentType.Turn_Left)
        {
            nextDirection = UtilMethods.TurnDirection(direction, true);
        }
        else if (segmentType == SegmentType.Turn_Right)
        {
            nextDirection = UtilMethods.TurnDirection(direction, false);
        }
        else if (segmentType == SegmentType.Turn_Both)
        {
            nextDirection = UtilMethods.TurnDirection(direction, getTurnBothLeft);
        }
        else
        {
            nextDirection = direction;
        }

        return nextDirection;
    }

    public bool CanSpawnNeighbor()
    {
        return canSpawnNext;
    }
}
