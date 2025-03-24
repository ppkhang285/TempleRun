using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;

[CreateAssetMenu(fileName = "DifficultProfile", menuName = "MapData/Difficulty/DifficultProfile")]
public class DifficultProfile : ScriptableObject
{
    
  
    public int difficulty;
    public float movementSpeed;

    [Serializable]
    public struct SegmentTypeWithWeight
    {
        public SegmentType segmentType;
        public int weight;
    }

    public List<SegmentTypeWithWeight> data;

    

    

    


}
