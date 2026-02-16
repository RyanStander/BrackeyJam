using Events;
using PersistentManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stations
{
    public class TestStation : MonoBehaviour
    {
        public void CompleteStation()
        {
            MiniGameDataHandler.CompletedMiniGame(TravelCost.MandatoryStopSuccessRefuel);
        }
        
        public void FailStation()
        {
            MiniGameDataHandler.FailedMiniGame(TravelCost.MandatoryStopFailureFuelCost);
        }

        public void FinishStation()
        {
            //TODO: We perform the travel sequence start and then fade to black, from there we determine if the player has fuel to continue or not, if they do we load the travel scene, if not we load the game over screen
            SceneManager.LoadScene(TrainDataHandler.CanContinue() ? "TrainTravelScene" : "FailureScene");
        }
    }
}
