using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QteMiniGame : MonoBehaviour
{
    public Transform needle;
    //public Image PressureBarImage;
    public float NeedleSpeed = 100f;
    public float CurrentNeedleAngle = 0f;
    public float PressureIncreaseRate = 0.5f;
    public float PressureDecreaseRate = 0.3f;
    public float MaxPressure = 50f;
    public float MinPressure = 0f;
    [SerializeField] private float currentPressure = 0f;
    private bool isGameActive;
    public float TargetMinAngle = -45f;
    public float TargetMaxAngle = 45f;
    public bool movingRight;

    public float dialMinAngle = 0f;
    public float dialMaxAngle = 180f;
    public float visualMultiplier = 2.1f;
    public bool isIncrease  = true;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentNeedleAngle = TargetMinAngle;
        currentPressure = 20f;
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;
        MoveNeedle();
        DrainPressure();

        if(Input.GetKeyDown(KeyCode.Space))
        {

            OnPress();
        }
    }

    private void DrainPressure()
    {
        currentPressure -= PressureDecreaseRate * Time.deltaTime;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
        //pressureBarImage.fillAmount = currentPressure / MaxPressure; fill this in later with some bar or something
        if(currentPressure <= MinPressure)
        {
            Debug.Log("Game Over! Pressure dropped to zero.");
            EndGame();
        }
    }

    private void MoveNeedle()
    {
        if(movingRight)
        {
            CurrentNeedleAngle += NeedleSpeed * Time.deltaTime;
            if (CurrentNeedleAngle >= dialMaxAngle)
            {
                CurrentNeedleAngle = dialMaxAngle;
                movingRight = false;
            }
        }
        else
        {
            CurrentNeedleAngle -= NeedleSpeed * Time.deltaTime;
            if (CurrentNeedleAngle <= dialMinAngle)
            {
                CurrentNeedleAngle = dialMinAngle;
                movingRight = true;
            }
        }
        needle.localRotation = Quaternion.Euler(0f, 0f, -(CurrentNeedleAngle * visualMultiplier));
    }

    public void OnPress()
    {
        if (CurrentNeedleAngle >= TargetMinAngle && CurrentNeedleAngle <= TargetMaxAngle)
        {
            SuccessfulPress();
            Debug.Log("Success! Current pressure " + currentPressure);
            if(currentPressure >= MaxPressure)
            {
                EndGame();
                Debug.Log("You Win! Pressure reached maximum.");
            }
        }
        else
        {
            FailedPress();
            Debug.Log("Failed!");
        }
    }

    private void FailedPress()
    {
        currentPressure -= 10f;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
    }

    private void SuccessfulPress()
    {
        currentPressure += 10f;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
    }

    public void EndGame()
    {
        if(!isGameActive) return;
        isGameActive = false;
        Debug.Log("Game Ended");
    }
}
