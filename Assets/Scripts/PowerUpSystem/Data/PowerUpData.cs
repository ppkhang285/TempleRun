using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class PowerUpData : ScriptableObject
{

    public bool isItem = false;
    public GameObject itemPrefab;

    public abstract void Activate(int currentLevel);
}
