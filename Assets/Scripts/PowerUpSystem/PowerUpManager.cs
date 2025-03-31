using System.Collections.Generic;

using UnityEngine;
using Utils;



public class PowerUpManager 
{
    private List<PowerUpData> powerUpDataList;

   private List<PowerUp> powerUpList;

    public PowerUpManager()
    {
        LoadPowerUpData();
    }


    private void LoadPowerUpData()
    {
        PowerUpConfig config = Resources.Load<PowerUpConfig>(Paths.POWERUP_DATA);

        if (config == null)
        {
            Debug.LogError("Cannot load PowerUpData Config");
        }
        else
        {
            Debug.Log("Load PowerUpData Config successfully");
        }

            powerUpDataList = config.powerUpList;
    }
}
