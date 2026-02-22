using UnityEngine;

namespace StationMgr
{
    public class StationCasinoManager : BaseStationManager
    {
        [SerializeField] private GameObject[] _objectsToDisableOnCompletion;
        [SerializeField] private GameObject[] _objectsToEnableOnCompletion;
        
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
        }

        public void PlayerLost()
        {
        
        }
    }
}
