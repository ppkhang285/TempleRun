using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    
    public GameObject itemObj {  get; private set; }

    public Item()
    {

    }

    public void MoveItem(float speed, Vector3 direction)
    {
        itemObj.transform.position += direction * speed * Time.deltaTime;
    }
}
