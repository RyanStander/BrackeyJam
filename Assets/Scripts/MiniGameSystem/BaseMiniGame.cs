using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMinigame : MonoBehaviour
{
    [Header("Minigame Settings")]
    public string gameName;
    public UnityEvent<bool> OnGameComplete;

    public virtual void StartMinigame()
    {
        gameObject.SetActive(true);
    }

    protected void FinishGame(bool isSuccess)
    {
        OnGameComplete?.Invoke(isSuccess);
    }

    public void ExitEarly()
    {
        FinishGame(false);
    }
}