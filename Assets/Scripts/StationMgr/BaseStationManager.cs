using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseStationManager : MonoBehaviour
{
    public UnityEvent OnStationComplete;

    protected abstract void CheckObjectives();

    protected virtual void OnObjectiveUpdate()
    {
        CheckObjectives();
    }
}