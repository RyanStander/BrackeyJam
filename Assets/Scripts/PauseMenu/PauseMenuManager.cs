using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas PauseMenuCanvas;
    void Start()
    {
        PauseMenuCanvas = FindObjectOfType<Canvas>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuCanvas.gameObject.activeInHierarchy)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenuCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        EditorApplication.isPlaying = false;
    }

    public void ResumeGame()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
