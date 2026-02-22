using System;
using System.Collections;
using AudioManagement;
using PersistentManager;
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

        private void Start()
        {
            AudioManager.PlayMusic(AudioDataHandler.StationCasino.CasinoMusic());
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
            //SceneManager.LoadScene("MainMenu");
            Application.Quit();
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
