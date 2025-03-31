using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoostData", menuName = "PowerUp/BoostData")]
public class BoostData : PowerUpData
{
    public float[] boostDistances = new float[5];
}
