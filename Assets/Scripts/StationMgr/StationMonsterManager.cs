using System.Collections;
using AudioManagement;
using PersistentManager;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StationMgr
{
    public class StationMonsterManager : BaseStationManager
    {
        #region First wire actions

        //todo, make the tentacle monster thank instead and then disappear
        [Header("First Wire Completion Actions")] [SerializeField]
        private Animator _tentacleMonsterAnimator;

        [SerializeField] private GameObject[] _firstWireObjectsToDisable;
        [SerializeField] private GameObject[] _firstWireObjectsToEnable;

        [SerializeField] private GameObject _largeTentacleMonster;

        #endregion

        #region Second wire actions

        [Header("Second Wire Completion Actions")] [SerializeField]
        private Animator _tentacleDoorAnimator;

        [SerializeField] private GameObject[] _secondWireObjectsToDisable;
        [SerializeField] private GameObject[] _secondWireObjectsToEnable;

        [SerializeField] private GameObject _tentacleDoorInteractable;

        #endregion

        #region Third wire actions

        [Header("Third Wire Completion Actions")] [SerializeField]
        private GameObject[] _thirdWireObjectsToDisable;

        [SerializeField] private GameObject[] _thirdWireObjectsToEnable;

        #endregion
        
        [SerializeField] private Animator _rewardAnimator;

        //not sure what onobjectiveupdate does, but keeping this here for later
        /*public void CompleteWireTask()
        {
            HasFixedWires = true;
            Debug.Log("Wires fixed!");
            OnObjectiveUpdate();
        }*/

        public void FirstWireFixed()
        {
            _tentacleMonsterAnimator.Play("MonsterUpTentacleLeave");
            foreach (GameObject obj in _firstWireObjectsToDisable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in _firstWireObjectsToEnable)
            {
                obj.SetActive(true);
            }

            AudioManager.Play(AudioDataHandler.StationMonster.Squish());
            StartCoroutine(WaitForTentacleToLeave());
        }

        private IEnumerator WaitForTentacleToLeave()
        {
            //wait until the animation is New State
            yield return new WaitUntil(
                () => _tentacleMonsterAnimator.GetCurrentAnimatorStateInfo(0).IsName("New State"));
            _largeTentacleMonster.SetActive(false);
        }

        public void SecondWireFixed()
        {
            _tentacleDoorAnimator.Play("TentacleOpen");
            _tentacleDoorInteractable.SetActive(true);
            foreach (GameObject obj in _secondWireObjectsToDisable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in _secondWireObjectsToEnable)
            {
                obj.SetActive(true);
            }

            AudioManager.Play(AudioDataHandler.StationMonster.Growl());
        }


        public void ThirdWireFixed()
        {
            foreach (GameObject obj in _thirdWireObjectsToDisable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in _thirdWireObjectsToEnable)
            {
                obj.SetActive(true);
            }

            AudioManager.Play(AudioDataHandler.StationMonster.Growl());
        }

        public void PlayerTookReward()
        {
            TrainDataHandler.Refuel(1);
            AudioManager.PlayOneShot(AudioDataHandler.TrainTravel.FuelPickup());
            _rewardAnimator.Play("RewardTentacleLeave");
            AudioManager.Play(AudioDataHandler.StationMonster.Squish());
        }

        public void ReturnToSpace()
        {
            StartCoroutine(FadeToLoad());
        }

        private IEnumerator FadeToLoad()
        {
            FadeToBlack fadeToBlack = FindObjectOfType<FadeToBlack>();
            fadeToBlack.StartFadeToBlack();

            yield return new WaitUntil(() => !fadeToBlack.IsFading());
            
            SceneManager.LoadScene("TrainTravelScene");
        }

        protected override void CheckObjectives()
        {
            /*if(HasFixedWires && HasDoor1Key)
            {
                isLevelComplete = true;
                Debug.Log("Station One complete!");
                OnStationComplete.Invoke();
            }*/
        }
    }
}
