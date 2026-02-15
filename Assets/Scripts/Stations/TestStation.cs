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
            SceneManager.LoadScene("TrainTravelScene");
            
            //TODO: Fuel Cost for failure
        }
    }
}
