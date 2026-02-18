using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private BaseMinigame _minigamePrefab;
    [SerializeField] private BaseMinigame _currentMinigameInstance;

    public UnityEvent OnMiniGameComplete;

    public bool Interact(Interaction interaction)
    {
        return TrySpawnMinigame();
    }

    public bool TrySpawnMinigame()
    {
        if (_currentMinigameInstance != null && _currentMinigameInstance.gameObject.activeSelf)
        {
            Debug.LogWarning("Minigame already active.");
            return false;
        }

        if (_currentMinigameInstance == null)
        {
            if (_minigamePrefab == null)
            {
                Debug.LogError("Minigame Prefab is missing on " + gameObject.name);
                return false;
            }

            _minigamePrefab.gameObject.SetActive(true); // Ensure prefab is inactive
            _currentMinigameInstance = _minigamePrefab;
            _currentMinigameInstance.OnGameComplete.AddListener(OnMiniGameFinished);
        }

        _currentMinigameInstance.gameObject.SetActive(true);
        _currentMinigameInstance.StartMinigame();

        return true;
    }

    public void OnMiniGameFinished(bool isSuccess)
    {
        if (isSuccess)
        {
            OnMiniGameComplete?.Invoke();
            Debug.Log("Minigame completed successfully!");
            Destroy(_minigamePrefab.gameObject);
            _currentMinigameInstance = null;
            // TODO reward the player???
            // also, mark minigame object as completed so they can't try it again
        }
        else
        {
            Debug.Log("Minigame failed or exited.");
            // TODO err... can someone re-do a minigame? is it even possible to fail a minigame like the wires lol.
        }
    }
}