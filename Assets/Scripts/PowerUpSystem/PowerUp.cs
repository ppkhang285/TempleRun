using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUp 
{
    public int index {  get; private set; }

    public PowerUpData data {  get; private set; }

    public int level {  get; private set; }
    public PowerUp(int index, PowerUpData data, int level)
    {
        this.index = index;
        this.data = data;
        this.level = level;
    }
}
