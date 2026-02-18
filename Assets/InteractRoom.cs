using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRoom : MonoBehaviour, IInteractable
{
    public Transform roomEntryPoint;
    public bool Interact(Interaction Interaction)
    {
        Debug.Log("Player is entering room");
        MovePlayerToRoom(Interaction.gameObject);
        return true;
    }

    public void MovePlayerToRoom(GameObject player)
    {
        player.transform.position = roomEntryPoint.position;
    }
}
