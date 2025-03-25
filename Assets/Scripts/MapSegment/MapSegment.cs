using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
public class MapSegment 
{
    private SegmentType segmentType;
    public Transform transform { get; private set; }
    
    public MapSegment(SegmentType segmentType, Transform transform)
    {
        this.segmentType = segmentType;
        this.transform = transform;
    }


   

    public void MoveSegment(float speed, Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
