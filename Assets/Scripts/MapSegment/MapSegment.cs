using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;
public class MapSegment 
{
    private SegmentType segmentType;
    private Transform transform;
    
    public MapSegment(SegmentType segmentType, Transform transform)
    {
        this.segmentType = segmentType;
        this.transform = transform;
    }


    public void MoveSegment(float speed)
    {
        //transform.position += Vector3.left * speed * Time.deltaTime;
    }

}
