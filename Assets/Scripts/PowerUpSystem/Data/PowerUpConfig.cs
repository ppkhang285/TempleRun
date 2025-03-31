using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUpConfig", menuName = "PowerUp/PowerUpConfig")]
public class PowerUpConfig: ScriptableObject
{
    public List<PowerUpData> powerUpList;
}
