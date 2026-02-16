using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool Interact(Interaction interaction)
    {
        Debug.Log("Interacted with the cube!");
        return true;
    }   
}
