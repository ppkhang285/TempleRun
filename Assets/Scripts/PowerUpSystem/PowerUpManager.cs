using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using static PowerUpConfig;



public class PowerUpManager 
{


    private Dictionary<int, PowerUpData> m_powerUpdataDict;
    private Dictionary<int, PowerUp> m_powerUpDict;   
    

    public PowerUpManager()
    {
        LoadPowerUpConfig();
        InitPowerUpDict();   
    
    }

    public void InitPowerUpDict()
    {
        m_powerUpDict = new Dictionary<int, PowerUp>();

        Dictionary<int, int> loadDataList = LoadDataFromStorage();

        foreach (KeyValuePair<int, PowerUpData> entry in m_powerUpdataDict)
        {
            int index = entry.Key;               
            PowerUpData powerUpData = entry.Value;
            int level;

            if (!loadDataList.ContainsKey(index))
            {
                level = 1;
            }
            else
            {
                level = loadDataList[index];
            }

            PowerUp powerUp = new PowerUp(index, powerUpData, level);
            m_powerUpDict.Add(index, powerUp);
            
        }

        Debug.Log(m_powerUpDict);

    }
    [Serializable]
    private struct PowerUpStorageData
    {
        public int index;
        public int level;
        public PowerUpStorageData(int index, int level)
        {
            this.index = index;
            this.level = level;
        }

       

    }

    private Dictionary<int, int> LoadDataFromStorage()
    {
        // Get data

        //
        Dictionary<int, int> result = new Dictionary<int, int>();

        //
        PowerUpStorageData fakeData = new PowerUpStorageData(0, 3);
        result.Add(fakeData.index, fakeData.level);

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
        
    }

    public void ActivatePowerUp()
    {
        int index = 1;
        int level = m_powerUpDict[index].level;
        m_powerUpDict[index].data.Activate(level);
    }
}
