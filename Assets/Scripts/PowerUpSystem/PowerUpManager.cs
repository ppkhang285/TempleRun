using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using static PowerUpConfig;
using static Utils.Enums;



public class PowerUpManager 
{


    private Dictionary<PowerUpType, PowerUpData> m_powerUpdataDict;
    private Dictionary<PowerUpType, PowerUp> m_powerUpDict;
    private List<PowerUpType> m_itemList;

    public PowerUpManager()
    {
        LoadPowerUpConfig();
        InitPowerUpDict();   
    
    }

    public void InitPowerUpDict()
    {
        m_powerUpDict = new Dictionary<PowerUpType, PowerUp>();

        Dictionary<PowerUpType, int> loadDataList = LoadDataFromStorage();

        foreach (KeyValuePair<PowerUpType, PowerUpData> entry in m_powerUpdataDict)
        {
            PowerUpType type = entry.Key;               
            PowerUpData powerUpData = entry.Value;
            int level;

            if (!loadDataList.ContainsKey(type))
            {
                level = 1;
            }
            else
            {
                level = loadDataList[type];
            }

            PowerUp powerUp = new PowerUp(type, powerUpData, level);
            m_powerUpDict.Add(type, powerUp);
            
        }

        Debug.Log(m_powerUpDict);

    }
    [Serializable]
    private struct PowerUpStorageData
    {
        public PowerUpType type;
        public int level;
        public PowerUpStorageData(PowerUpType type, int level)
        {
            this.type = type;
            this.level = level;
        }

       

    }

    private Dictionary<PowerUpType, int> LoadDataFromStorage()
    {
        // Get data

        //
        Dictionary<PowerUpType, int> result = new Dictionary<PowerUpType, int>();

        //
        PowerUpStorageData fakeData = new PowerUpStorageData(0, 3);
        result.Add(fakeData.type, fakeData.level);

        //
        return result;
    }

    public void SaveDataToStorage()
    {


    }

    private void LoadPowerUpConfig()
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
        m_powerUpdataDict = config.ToDict();
        m_itemList = config.itemTypeList.ToList();

    }

    public void ActivatePowerUp()
    {
        PowerUpType type = PowerUpType.Invisibility;
        int level = m_powerUpDict[type].level;
        m_powerUpDict[type].data.Activate(level);
        
    }
}
