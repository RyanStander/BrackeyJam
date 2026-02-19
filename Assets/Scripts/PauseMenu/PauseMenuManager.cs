using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas PauseMenuCanvas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
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
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        EditorApplication.isPlaying = false;
    }

    public void ResumeGame()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
