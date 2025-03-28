using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
public class MapSegment 
{
    private SegmentType segmentType;
    public Transform segmentTransform { get; private set; }
    public bool spawnNext; // Can spawn next segment link with this segment
    public Direction direction; // Directioon of this Segment



    public MapSegment(SegmentType segmentType, Transform transform)
    {
        this.segmentType = segmentType;
        this.segmentTransform = transform;

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

}
