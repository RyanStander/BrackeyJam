using Events;
using PersistentManager;
using TrainNavigation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stations
{
    public class TestStation : MonoBehaviour
    {
        [SerializeField] private TravelCostData _travelCostData;
        public void CompleteStation()
        {
            SceneManager.LoadScene("TrainTravelScene");
            
            //TODO: Reward for success
        }
        
        public void FailStation()
        {
            //TODO: Game manager should trigger a failure after departure
            if(!TrainDataHandler.ExpendFuel(_travelCostData.MandatoryStopFailureFuelCost))
                EventManager.currentManager.AddEvent(new OutOfFuel());
            
            //TODO: Player has lost
        }
    }
}
