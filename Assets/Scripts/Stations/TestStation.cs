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
            SceneManager.LoadScene("TrainTravelScene");
            
            //TODO: Reward for success
        }
        
        public void FailStation()
        {
            //TODO: Game manager should trigger a failure after departure
            if(!TrainDataHandler.ExpendFuel(TravelCost.MandatoryStopFailureFuelCost))
                EventManager.currentManager.AddEvent(new OutOfFuel());
            
            //TODO: Player has lost
        }
    }
}
