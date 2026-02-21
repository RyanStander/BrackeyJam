using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseMenuCanvas;
        [SerializeField] private Animator _pauseScreenAnimator;
        [SerializeField] private Animator _pauseScreenBackgroundAnimator;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if (_pauseMenuCanvas.gameObject.activeInHierarchy)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        private void PauseGame()
        {
            _pauseMenuCanvas.gameObject.SetActive(true);
            _pauseScreenAnimator.Play("SettingsSlideIn");
            _pauseScreenBackgroundAnimator.Play("BackgroundFadeIn");
            Time.timeScale = 0f;
        }

        public void ExitGame()
        {
            EditorApplication.isPlaying = false;
        }

        public void ResumeGame()
        {
            StartCoroutine(ResumeGameCoroutine());
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        
        private IEnumerator ResumeGameCoroutine()
        {
            _pauseScreenAnimator.Play("SettingsSlideOut");
            _pauseScreenBackgroundAnimator.Play("BackgroundFadeOut");

            yield return new WaitUntil(()=> _pauseScreenAnimator.GetCurrentAnimatorStateInfo(0).IsName("SettingsSlideOut") &&
                _pauseScreenBackgroundAnimator.GetCurrentAnimatorStateInfo(0).IsName("BackgroundFadeIn"));

            _pauseMenuCanvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        
    }
}
