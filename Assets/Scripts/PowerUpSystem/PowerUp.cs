using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;



public class PowerUp 
{
    public PowerUpType type {  get; private set; }

    public PowerUpData data {  get; private set; }

    public int level {  get; private set; }
    public PowerUp(PowerUpType type, PowerUpData data, int level)
    {
        this.type = type;
        this.data = data;
        this.level = level;
    }
}
