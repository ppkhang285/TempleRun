using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;
using static GameUtils.Enums;

public class ItemController 
{
    private Transform mapRoot;

    private List<GameObject> itemObjList;
    private Dictionary<int, PowerUpType> itemTypeDict;


    public ItemController(Transform mapRoot)
    {
        this.mapRoot = mapRoot;
        itemObjList = new List<GameObject> ();
        itemTypeDict = new Dictionary<int, PowerUpType> ();
    }

    public void SpawnItem(MapSegment mapSegment)
    {
        Vector3 pos = mapSegment.segmentTransform.position + Vector3.up * 15;
        Quaternion rotation = Constants.ROTATION_VECTOR[mapSegment.direction];

        PowerUpData powerUpData = GameplayManager.Instance.powerUpManager.GetRandomItemPowerUp();

        GameObject itemObj = GameObject.Instantiate(powerUpData.itemPrefab);

        itemObj.transform.position = pos;
        itemObj.transform.rotation = rotation;

        itemObj.transform.SetParent(mapRoot);

        itemTypeDict.Add(itemObj.GetInstanceID(), powerUpData.type);

        itemObjList.Add(itemObj);



    }

    public void HandleDeleteItem()
    {
        {
            for (int i = itemObjList.Count - 1; i >= 0; i--)
            {
                float distance = Vector3.Distance(itemObjList[i].transform.position, Vector3.zero);
                if (distance > Constants.DESTROY_DISTANCE)
                {
                    DespawnItemAt(i);
                }
            }
        }
    }

    private void DespawnItem(GameObject itemObj)
    {
        if (itemObjList.Contains(itemObj))
        {           
            itemObjList.Remove(itemObj);
            itemTypeDict.Remove(itemObj.GetInstanceID());

            GameObject.Destroy(itemObj);
        }
    }
 

    private void DespawnItemAt(int index)
    {
        GameObject itemObj = itemObjList[index];
        DespawnItem(itemObj);
    }

    public void MoveItems(float speed, Vector3 direction)
    {
        //itemObj.transform.position += direction * speed * Time.deltaTime;
        for(int i = 0; i < itemObjList.Count; i++)
        {
            itemObjList[i].transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Reset()
    {
        for(int i = itemObjList.Count - 1; i >= 0; i--)
        {
            GameObject itemObj = itemObjList[i];
            itemObjList.RemoveAt(i);
            GameObject.Destroy (itemObj);
        }
        itemTypeDict.Clear();
    }

    public void PickupItem(GameObject itemObj)
    {
        
        PowerUpType type = GetTypeFromObj(itemObj);
        if (type == PowerUpType.None)
        {
            Debug.LogError("Cannot find type of Item Object");
            return;
        }
        DespawnItem(itemObj);

        GameplayManager.Instance.powerUpManager.ActivatePowerUp(type);
    }

    public PowerUpType GetTypeFromObj(GameObject obj)
    {
        if (itemTypeDict.ContainsKey(obj.GetInstanceID()))
        {
            return itemTypeDict[obj.GetInstanceID()];
        }
        else return PowerUpType.None;
    }
}
