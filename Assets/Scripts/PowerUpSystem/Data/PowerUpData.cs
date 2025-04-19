using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtils.Enums;


public abstract class PowerUpData : ScriptableObject
{
    public PowerUpType type;
    public bool isItem = false;
    public GameObject itemPrefab;

    public abstract void Activate(int currentLevel);
}
