using AudioManagement;
using PersistentManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StationSubmarineManager : BaseStationManager
{
    public int Floor1MinigamesCompleted = 0;
    public int Floor2MinigamesCompleted = 0;
    public int Floor3MinigamesCompleted = 0;

    public bool isLevelComplete = false;
    public int CurrentWaterLevel = 0;

    public GameObject Block1;
    public GameObject Block2;
    public GameObject CurrentBlock;

    public GameObject BarrierWall1;
    public GameObject BarrierWall2;
    public GameObject BarrierWall3;
    public GameObject CurrentWall;

    public UnityEvent OnMiniGameSetComplete;

    public Transform[] NpcHolder;

    private void Awake()
    {
        CurrentWaterLevel = 0;
        CurrentBlock = Block1;
        CurrentWall = BarrierWall1;
        AudioManager.Play(AudioDataHandler.StationUnderwater.UnderwaterMusic());
        AudioManager.Play(AudioDataHandler.StationUnderwater.UnderwaterAmbience());
    }

    public void CompleteFloor1Minigame()
    {
        Floor1MinigamesCompleted++;

        if (Floor1MinigamesCompleted >= 1 && CurrentWaterLevel == 0)
        {
            CurrentWaterLevel = 1;

            if (CurrentWall != null) CurrentWall.SetActive(false);
            CurrentWall = BarrierWall2;

            if (CurrentBlock != null) CurrentBlock.SetActive(false);
            CurrentBlock = Block2;

            Debug.Log("Water Raised!");
            NpcHolder[0].gameObject.SetActive(false); //puz1
            NpcHolder[1].gameObject.SetActive(true);//puz1 solved
            OnMiniGameSetComplete?.Invoke();
            OnObjectiveUpdate();
        }
    }

    public void CompleteFloor2Minigame()
    {
        Floor2MinigamesCompleted++;

        if (Floor2MinigamesCompleted >= 2 && CurrentWaterLevel == 1)
        {
            CurrentWaterLevel = 2;

            if (CurrentWall != null) CurrentWall.SetActive(false);
            CurrentWall = BarrierWall3;

            if (CurrentBlock != null) CurrentBlock.SetActive(false);

            Debug.Log("Water Raised!");
            NpcHolder[1].gameObject.SetActive(false);
            NpcHolder[2].gameObject.SetActive(false);
            OnMiniGameSetComplete?.Invoke();
            OnObjectiveUpdate();
        }
    }

    public void CompleteFloor3Minigame()
    {
        Floor3MinigamesCompleted++;

        if (Floor3MinigamesCompleted >= 1 && CurrentWaterLevel == 2)
        {
            CurrentWaterLevel = 3;

            if (CurrentWall != null) CurrentWall.SetActive(false);
            NpcHolder[4].gameObject.SetActive(false);
            NpcHolder[5].gameObject.SetActive(true);
            NpcHolder[6].gameObject.SetActive(true);
            NpcHolder[7].gameObject.SetActive(true);

            OnMiniGameSetComplete?.Invoke();
            OnObjectiveUpdate();
        }
    }

    protected override void CheckObjectives()
    {
        if (Floor1MinigamesCompleted >= 1 && Floor2MinigamesCompleted >= 2 && Floor3MinigamesCompleted >= 1)
        {
            if (!isLevelComplete)
            {
                isLevelComplete = true;
                Debug.Log("Submarine Station complete!");
                OnStationComplete?.Invoke();
            }
        }
    }
}