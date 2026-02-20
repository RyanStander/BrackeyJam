using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogueController : MonoBehaviour
{
    private TMP_Text _textComponent;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private float _lineDisplayDuration = 2f;
    [SerializeField] private string[] dialogueLines;
    void Start()
    {
        _textComponent = GetComponentInChildren<TMP_Text>();
        //StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(_lineDisplayDuration);
        }
    }
    public void DisplayDialogue(string[] lines)
    {
        dialogueLines = lines;
        StartCoroutine(StartDialogue());
    }

    private IEnumerator TypeLine(string line)
    {
        _textComponent.text = line;
        _textComponent.maxVisibleCharacters = 0;

        for (int i = 0; i <= _textComponent.textInfo.characterCount; i++)
        {
            _textComponent.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(_typingSpeed);
        }

        _textComponent.maxVisibleCharacters = _textComponent.textInfo.characterCount;
    }
}
