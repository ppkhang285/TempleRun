using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DifficultData", menuName = "MapData/Difficulty/DifficultData")]
public class DifficultData : ScriptableObject
{
    public int maxDifficulty;

    [Serializable]
    public struct DifficultProfileWithThreshold
    {
        public DifficultProfile profile;
        public int difficultyThreshold;
    }


    public List<DifficultProfileWithThreshold> profiles;
}
