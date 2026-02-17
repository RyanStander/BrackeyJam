using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas PauseMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuCanvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGame()
    {
        EditorApplication.isPlaying = false;
    }

    public void ResumeGame()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
    }
}
