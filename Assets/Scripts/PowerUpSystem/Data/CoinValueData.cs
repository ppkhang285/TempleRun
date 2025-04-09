using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName= "CoinValueData", menuName = "PowerUp/CoinValueData")]
public class CoinValueData : PowerUpData
{
    public int[] values = new int[5];

    public override void Activate(int currentLevel)
    {
        throw new System.NotImplementedException();
    }
}
