using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// This script should be attached to any NPC GameObject that you want the player to interact with.
// It implements the IInteractable interface, allowing it to respond to player interactions.
// When the player interacts with the NPC, it will display dialogue lines in the console.
// </summary>

public class InteractNPC : MonoBehaviour, IInteractable
{
    //[SerializeField]
    //private string _prompt;
    //public string InteractionPrompt => _prompt;
    //public string InteractionPromptLines;

    [Header("NPC Dialogue Lines")]
    [Tooltip("Add dialogue lines here directly in the Inspector.")]
    [SerializeField] private string[] _dialogueLines;
    [SerializeField] private int _currentLineIndex = 0;
    public void Start()
    {
        if(_dialogueLines == null || _dialogueLines.Length == 0)
        {
            Debug.LogWarning("No dialogue lines assigned to " + gameObject.name);
            return;
        }
    }

    private void ShowCurrentDialogueLine()
    {
        if (_dialogueLines.Length == 0) return;
        Debug.Log(_dialogueLines[_currentLineIndex]);
    }

    public void ShowNextDialogueLine()
    {
        if(_dialogueLines.Length == 0) return;
        ShowCurrentDialogueLine();
        _currentLineIndex = (_currentLineIndex + 1) % _dialogueLines.Length;
    }

    public bool Interact(Interaction interaction)
    {
        ShowNextDialogueLine();
        return true;
    }   
}
