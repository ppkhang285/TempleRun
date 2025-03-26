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

    public Dictionary<SegmentType, int> GetSegmentWeightDict()
    {
        Dictionary<SegmentType, int> segmentTypeDict = new Dictionary<SegmentType, int>();
        for (int i = 0; i < data.Count; i++)
        {
            if (!segmentTypeDict.ContainsKey(data[i].segmentType))
            {
                segmentTypeDict.Add(data[i].segmentType, data[i].weight);
            }
        }
        return segmentTypeDict;
    }







}
