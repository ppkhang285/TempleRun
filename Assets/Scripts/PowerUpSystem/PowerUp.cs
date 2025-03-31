using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp 
{
    public int index {  get; private set; }
    public int currentLevel { get; private set; }
    public PowerUpData data;

    public PowerUp(int index, PowerUpData data, int level)
    {
        this.index = index;
        this.currentLevel = level;
    }
}
