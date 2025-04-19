using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static GameUtils.Enums;

public class SegmentRule
{

    private List<SegmentType> envList;


    public SegmentRule()
    {

    }

    /// <summary>
    /// Input: List to filter and all segmentList (Enviroment) <br/>
    /// Output: List of valid segment type
    /// </summary>
    /// 
    /// 

    private void Reset()
    {
        envList?.Clear();
    }
    public List<SegmentType> Filter(List<SegmentType> inputList, List<SegmentType> envList)
    {
        Reset();

        this.envList = envList;

        List<SegmentType> filterList = new List<SegmentType>(inputList);

        // filter
        filterList = Rule_1(filterList);
        filterList = Rule_2(filterList);
        filterList = Rule_3(filterList);
        filterList = Rule_4(filterList);
        //
        return filterList;

    }


    /// <summary>
    /// Rule 1: At least 4 straight segment in a row after Start
    /// </summary>
    ///
    /// 
    public List<SegmentType> Rule_1(List<SegmentType> inputList)
    {
        if (envList.Count <=4)
        {
            return new List<SegmentType> { SegmentType.Straight};
        }
        return inputList;
    }


    /// <summary>
    /// No 2 obstacles in a row
    /// </summary>

    public List<SegmentType> Rule_2(List<SegmentType> inputList)
    {
        List<SegmentType> obstacleList = new List<SegmentType> { 
            SegmentType.Slide,
            SegmentType.Jump,
            SegmentType.NarrowLeft,
            SegmentType.NarrowRight
        };

    
        if (obstacleList.Contains(envList.Last()))
        {
            inputList.RemoveAll(x => obstacleList.Contains(x));
            
        }
        return inputList;
    }

    /// <summary>
    /// At least 5 segments between 2 turns
    /// </summary>
    public List<SegmentType> Rule_3(List<SegmentType> inputList)
    {
        if (envList.Count <= 5)
        {
            return inputList;
        }

        bool turned = false;
        for(int i = envList.Count-1; i>= envList.Count - 1 -5; i--)
        {
            if (envList[i] == SegmentType.Turn_Left || envList[i] == SegmentType.Turn_Right || envList[i] == SegmentType.Turn_Both)
            {
                turned = true;
                
            }
        }

        if (turned)
        {
            inputList.RemoveAll(x => x == SegmentType.Turn_Left || x == SegmentType.Turn_Right || x == SegmentType.Turn_Both);
           
        }

        return inputList;
    }

    /// <summary>
    /// After a turn, it's straight
    /// </summary>
    public List<SegmentType> Rule_4(List<SegmentType> inputList)
    {
        
        if (envList[envList.Count-1] == SegmentType.Turn_Both ||
            envList[envList.Count - 1] == SegmentType.Turn_Left ||
            envList[envList.Count - 1] == SegmentType.Turn_Right
            )
        {
            return new List<SegmentType>() { SegmentType.Straight };
        }
        return inputList;
    }

    




}
