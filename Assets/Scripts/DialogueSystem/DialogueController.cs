using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// <summary>
// This script is responsible for grabbing the lines from the Interact NPC, then displaying them in a type
// writer manner. The display box disappers after a few seconds, and you can fast forward the text to the end.
// </summary>

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text _textComponent;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] protected float _lineDisplayDuration = 4f;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private GameObject _dialogueBox;

    public bool IsTyping;

    private void OnValidate()
    {
        if (_textComponent == null)
            _textComponent = GetComponentInChildren<TMP_Text>();
    }

    private void Awake()
    {
        if (_textComponent == null)
            _textComponent = GetComponentInChildren<TMP_Text>();
        _dialogueBox.SetActive(false);
    }

    public IEnumerator DisplayDialogue(string[] lines)
    {
        StopAllCoroutines();
        dialogueLines = lines;

        _dialogueBox.SetActive(true);

        yield return StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(_lineDisplayDuration);
        }

        _textComponent.text = string.Empty;
        _dialogueBox.SetActive(false);
    }

    public void SkipDialogueToEnd()
    {
        StopAllCoroutines();
        _textComponent.maxVisibleCharacters = _textComponent.textInfo.characterCount;
        IsTyping = false;
    }

    private IEnumerator TypeLine(string line)
    {
        _textComponent.text = line;
        _textComponent.ForceMeshUpdate();

        _textComponent.maxVisibleCharacters = 0;
        IsTyping = true;

        for (int i = 0; i <= _textComponent.textInfo.characterCount; i++)
        {
            _textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(_typingSpeed);
        }

        _textComponent.maxVisibleCharacters = _textComponent.textInfo.characterCount;
        IsTyping = false;
    }

    public void CloseDialogue()
    {
        _dialogueBox.SetActive(false);
    }
}
