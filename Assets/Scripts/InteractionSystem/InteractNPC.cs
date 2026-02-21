using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// <summary>
// This script should be attached to any NPC GameObject that you want the player to interact with.
// It implements the IInteractable interface, allowing it to respond to player interactions.
// When the player interacts with the NPC, it will display dialogue lines in the console.
// </summary>

public class InteractNPC : MonoBehaviour, IInteractable
{
    [Header("NPC Dialogue Lines")]
    [Tooltip("Add dialogue lines here directly in the Inspector.")]
    [SerializeField] private string[] _dialogueLines;
    [SerializeField] private string[] _dialogueLines2;//maybe for alternate dialogue on objectives
    [SerializeField] private string[] _dialogueLines3;

    [SerializeField] private int _currentLineIndex = 0;

    [SerializeField] private DialogueController _dialogueController;

    public UnityEvent OnObjectiveComplete;

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
        _dialogueController.DisplayDialogue(new string[] { _dialogueLines[_currentLineIndex] });
    }

    public void ShowNextDialogueLine()
    {
        if(_dialogueLines.Length == 0) return;
        ShowCurrentDialogueLine();
        _currentLineIndex = (_currentLineIndex + 1) % _dialogueLines.Length;
    }

    

    public bool Interact(Interaction interaction)
    {
        if (_dialogueController.IsTyping)
        {
            _dialogueController.SkipDialogueToEnd();
            return true;
        }
        ShowNextDialogueLine();
        
        return true;

    }   
}
