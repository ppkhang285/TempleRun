using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "InvisibilityData", menuName = "PowerUp/InvisibilityData")]
public class InvisibilityData : PowerUpData
{
    public float[] duration = new float[5];

    public override void Activate(int currentLevel)
    {
        throw new System.NotImplementedException();
    }
}
