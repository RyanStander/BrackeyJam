using System.Collections;
using AudioManagement;
using PersistentManager;
using UnityEngine;

namespace StationMgr
{
    public class StationMonsterManager : BaseStationManager
    {
        #region First wire actions

        //todo, make the tentacle monster thank instead and then disappear
        [Header("First Wire Completion Actions")] [SerializeField]
        private Animator _tentacleMonsterAnimator;

        [SerializeField] private GameObject _largeTentacleMonster;

        #endregion

        #region Second wire actions

        [Header("Second Wire Completion Actions")] [SerializeField]
        private Animator _tentacleDoorAnimator;

        [SerializeField] private GameObject _tentacleDoorInteractable;

        #endregion

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
            AudioManager.Play(AudioDataHandler.StationMonster.Growl());
                StartCoroutine(WaitForTentacleToLeave());
        }

        private IEnumerator WaitForTentacleToLeave()
        {
            //wait until the animation is New State
            yield return new WaitUntil(() => _tentacleMonsterAnimator.GetCurrentAnimatorStateInfo(0).IsName("New State"));
            _largeTentacleMonster.SetActive(false);
        }

        public void SecondWireFixed()
        {
            _tentacleDoorAnimator.Play("TentacleOpen");
            _tentacleDoorInteractable.SetActive(true);
            AudioManager.Play(AudioDataHandler.StationMonster.Growl());
        }


        public void ThirdWireFixed()
        {
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
