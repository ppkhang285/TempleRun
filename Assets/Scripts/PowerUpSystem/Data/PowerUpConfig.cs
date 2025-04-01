using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUpConfig", menuName = "PowerUp/PowerUpConfig")]
public class PowerUpConfig: ScriptableObject
{
    [Serializable]
    public struct PowerUpWithIndex
    {
        public int index;
        public PowerUpData data;
    }
    public List<PowerUpWithIndex> powerUpList;
    public Dictionary<int,  PowerUpData> ToDict()
    {
        Dictionary<int, PowerUpData> result = new Dictionary<int, PowerUpData>();
        foreach(PowerUpWithIndex powerUp in powerUpList)
        {
            if (result.ContainsKey(powerUp.index))
            {
                Debug.LogError("Same index in Power Config");
                continue;
            }
            result[powerUp.index] = powerUp.data;

        }
        return result;


    }
}
