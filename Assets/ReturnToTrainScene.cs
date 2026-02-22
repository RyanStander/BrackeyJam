using AudioManagement;
using PersistentManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTrainScene : MonoBehaviour, IInteractable
{
    public bool Interact(Interaction Interaction)
    {
        AudioManager.StopMusic(true);
        SceneManager.LoadScene("TrainTravelScene");
        return true;
    }
}
