using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StationMgr
{
    public class StationCasinoManager : BaseStationManager
    {
        [SerializeField] private GameObject[] _objectsToDisableOnCompletion;
        [SerializeField] private GameObject[] _objectsToEnableOnCompletion;
        [SerializeField] private FadeToBlack _fadeToBlack;

        private void OnValidate()
        {
            if (_fadeToBlack==null)
                _fadeToBlack = FindObjectOfType<FadeToBlack>();
        }

        protected override void CheckObjectives()
        {
        }
        
        public void PlayerWon()
        {
            foreach (GameObject obj in _objectsToDisableOnCompletion)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in _objectsToEnableOnCompletion)
            {
                obj.SetActive(true);
            }
        }
        
        public void GoToWinScreen()
        {
            //todo: go to win screen
            SceneManager.LoadScene("MainMenu");
        }

        public void PlayerLost()
        {
            StartCoroutine(PlayerLostCoroutine());
        }
        
        private IEnumerator PlayerLostCoroutine()
        {
            _fadeToBlack.StartFadeToBlack();
            yield return new WaitUntil(()=>!_fadeToBlack.IsFading());
            SceneManager.LoadScene("FailureScene");
        }
    }
}
