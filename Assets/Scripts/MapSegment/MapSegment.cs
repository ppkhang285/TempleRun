using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.Enums;
public class MapSegment 
{
    private SegmentType segmentType;
    public Transform segmentTransform { get; private set; }
    public bool canSpawnNext; // Can spawn next segment link with this segment
    public Direction direction; // Directioon of this Segment



    public MapSegment(SegmentType segmentType, Transform transform, Direction direction)
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
    /// <summary>
    /// If not TurnBoth -> Pos is normal vector3
    /// If is TurnBoth :
    /// x: z
    /// </summary>

    public Vector3 GetNeighborPos(GameObject newPref)
    {


        Vector3 directionVector = Constants.DIRECTION_VECTOR[GameplayManager.Instance.currentDirecion];


        // Change to -> Get size from Collider

        Vector3 newSize = newPref.transform.GetChild(0).Find("sizeObj").GetComponent<BoxCollider>().size;
        Vector3 lastSize = this.segmentTransform.GetChild(0).Find("sizeObj").GetComponent<BoxCollider>().size;


        Vector3 lastPosition = this.segmentTransform.position;
        //Debug.Log(lastSize);
        //Debug.Log(newSize);


        Vector3 newPosition = lastPosition + directionVector * (newSize.x + lastSize.x) / 2;
        newPosition.y = 0;

        return newPosition;
    }
}
