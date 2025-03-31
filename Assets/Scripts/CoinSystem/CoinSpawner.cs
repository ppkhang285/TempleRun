using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner
{

    private const int MIN_COIN = 5;
    private const int MAX_COIN = 10;

    public CoinSpawner()
    {

    }

    public void SpawnCoin(MapSegment mapSegment)
    {
        if (mapSegment == null)
        {
            Debug.Log("Map Segment is null when spawning coin");
            return;
        }


    }

    /// <summary>
    /// Return List of coin's position. For both Straight and Slide
    /// </summary>

    public Vector3[] Pattern_Straight(GameObject segmentObj)
    {



        return new Vector3[0];
    }

    /// <summary>
    /// Return List of coin's position. For Jump
    /// </summary>
    public Vector3[] Patter_Jump(GameObject segmentObj)
    {
        return new Vector3[0];
    }
}
