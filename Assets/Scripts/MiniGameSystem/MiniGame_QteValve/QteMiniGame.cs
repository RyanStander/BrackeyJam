using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QteMiniGame : BaseMinigame
{
    public Transform needle;
    public Transform ValveWheel;

    public SpriteRenderer ValveEKey;
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
    private bool isRotating;
    private Coroutine activeRotationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        ValveEKey = ValveEKey.GetComponent<SpriteRenderer>();
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
        HighlightValveEKey();

        if (Input.GetKeyDown(KeyCode.E))
        {

            OnPress();
        }
    }

    private void DrainPressure()
    {
        currentPressure -= PressureDecreaseRate * Time.deltaTime;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
        //pressureBarImage.fillAmount = currentPressure / MaxPressure; fill this in later with some bar or something
        
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


    public void HighlightValveEKey()
    {
        if(CurrentNeedleAngle >= TargetMinAngle && CurrentNeedleAngle <= TargetMaxAngle)
        {
            ValveEKey.color = Color.green;
        }
        else
        {
            ValveEKey.color = Color.red;
        }
    }

    public void OnPress()
    {
        if (CurrentNeedleAngle >= TargetMinAngle && CurrentNeedleAngle <= TargetMaxAngle)
        {
            SuccessfulPress();
            Debug.Log("success! current pressure " + currentPressure);
            SpinValveWheel(-180f, 1.5f);
            if(currentPressure >= MaxPressure)
            {
                SpinValveWheel(-360f, 1f);
                EndGame();
                Debug.Log("Win!");
            }
        }
        else
        {
            FailedPress();
            Debug.Log("Failed!");
        }
    }

    private void SpinValveWheel(float amountToAdd, float time)
    {
        if (activeRotationCoroutine != null)
        {
            StopCoroutine(activeRotationCoroutine);
        }
        activeRotationCoroutine = StartCoroutine(RotateWheelOverTime(amountToAdd, time));
    }

    private IEnumerator RotateWheelOverTime(float amountToAdd, float duration)
    {
        isRotating = true;

        float startZAngle = ValveWheel.localEulerAngles.z;

        float targetZAngle = startZAngle + amountToAdd;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float currentZAngle = Mathf.Lerp(startZAngle, targetZAngle, t);

            ValveWheel.localRotation = Quaternion.Euler(0f, 0f, currentZAngle);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ValveWheel.localRotation = Quaternion.Euler(0f, 0f, targetZAngle);

        isRotating = false;
    }

    private void FailedPress()
    {
        currentPressure -= 10f;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
        SpinValveWheel(180f, 1.5f);
    }

    private void SuccessfulPress()
    {
        currentPressure += 10f;
        currentPressure = Mathf.Clamp(currentPressure, MinPressure, MaxPressure);
    }
    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);
        FinishGame(true);
    }

    public void EndGame()
    {
        if(!isGameActive) return;
        isGameActive = false;
        StartCoroutine(WinSequence());
        Debug.Log("Game Ended");
    }
}
