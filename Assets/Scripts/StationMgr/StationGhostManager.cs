using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGhostManager : BaseStationManager
{
    public bool HasFixedWires = false;
    public bool HasDoor1Key = false;
    public bool HasDoor2Key = false;
    public bool isLevelComplete = false;

    public GameObject BarrierWall1;
    public GameObject BarrierWall2;
    public GameObject BarrierWall3;

    public void CompleteWireTask()
    {
        HasFixedWires = true;
        Debug.Log("Wires fixed!");
        OnObjectiveUpdate();
    }

    public void CollectDoor1Key()
    {
        HasDoor1Key = true;
        Debug.Log("Door 1 key collected!");
        OnObjectiveUpdate();
    }

    protected override void CheckObjectives()
    {
        if(HasFixedWires && HasDoor1Key)
        {
            isLevelComplete = true;
            Debug.Log("Station One complete!");
            OnStationComplete.Invoke();
        }
    }
}