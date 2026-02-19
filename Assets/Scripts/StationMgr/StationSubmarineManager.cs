using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSubmarineManager : BaseStationManager
{
    public bool HasFixedPipes = false;
    public bool isLevelComplete = false;

    public int CurrentWaterLevel = 0;

    public GameObject Block1;
    public GameObject Block2;
    public GameObject CurrentBlock;

    public GameObject BarrierWall1;
    public GameObject BarrierWall2;
    public GameObject BarrierWall3;
    public GameObject CurrentWall;

    private void Awake()
    {
        CurrentWaterLevel = 0;
        CurrentBlock = Block1;
        CurrentWall = BarrierWall1;
    }

    public void CompleteFirstFloorPuzzle()
    {
        HasFixedPipes = true;
        CurrentWaterLevel = 1;

        CurrentWall.SetActive(false);
        CurrentWall = BarrierWall2;

        CurrentBlock.gameObject.SetActive(false);
        CurrentBlock = Block2;

        Debug.Log("Water Raised!");
        OnObjectiveUpdate();
    }

    public void CompleteSecondFloorPuzzle()
    {
        CurrentWaterLevel = 1;

        CurrentWall.SetActive(false);
        CurrentWall = BarrierWall3;
        CurrentWall.SetActive(false);

        CurrentBlock.gameObject.SetActive(false);

        OnObjectiveUpdate();
    }

    public void RemoveCurrentBlock()
    {

    }

    protected override void CheckObjectives()
    {
        if(HasFixedPipes)
        {
            isLevelComplete = true;
            Debug.Log("Submarine Station complete!");
            OnStationComplete.Invoke();
        }
    }
}