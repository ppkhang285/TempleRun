using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "MegaCoinData", menuName = "PowerUp/MegaCoinData")]
public class MegaCoinData : PowerUpData
{

    public int[] extraCoin = new int[5];

    public override void Activate(int currentLevel)
    {
        if (currentLevel <= 0 || currentLevel > Constants.MAX_POWERUP_LEVEL)
        {
            Debug.LogError("Wrong Level");
            return;
        }
        int coin = extraCoin[currentLevel-1];

        GameplayManager.Instance.progressionManager.GainCoin(coin);
    }


}
