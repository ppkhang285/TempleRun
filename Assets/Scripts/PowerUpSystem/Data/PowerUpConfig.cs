using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Utils.Enums;


[CreateAssetMenu(fileName = "PowerUpConfig", menuName = "PowerUp/PowerUpConfig")]
public class PowerUpConfig: ScriptableObject
{

    public List<PowerUpData> powerUpList;

    public List<PowerUpType> itemTypeList;

    public Dictionary<PowerUpType,  PowerUpData> ToDict()
    {
        Dictionary<PowerUpType, PowerUpData> result = new Dictionary<PowerUpType, PowerUpData>();
        foreach(PowerUpData powerUp in powerUpList)
        {
            if (result.ContainsKey(powerUp.type))
            {
                Debug.LogError("Same type in Power Config");
                continue;
            }
            result[powerUp.type] = powerUp;

        }
        return result;


    }
}
