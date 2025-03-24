using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Use to control the map:<br/>
/// - Spawn Segment <br/>
/// - Move Segments (update)
/// </summary>
public class MapController 
{
    private Transform mapRoot;
    private MapGenerator mapGenerator;



    public MapController(Transform mapRoot)
    {
        this.mapRoot = mapRoot;

        mapGenerator = new MapGenerator();

    }




}
