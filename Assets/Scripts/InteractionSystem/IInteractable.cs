using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// This interface defines the contract for any interactable object in the game.
// Any object that implements this interface can be interacted with by the player.
// </summary>
public interface IInteractable
{
    //public string InteractionPrompt { get; }
    public bool Interact(Interaction Interaction);
}
