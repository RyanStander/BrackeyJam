using PersistentManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainNavigation
{
    /// <summary>
    /// We check if we have enough fuel to perform a standard trip, if not then we run out before we reach the destination
    /// If we do have fuel, then we expend the required fuel for a basic trip.
    /// </summary>
    public class TrainNavigation : MonoBehaviour
    {
        [SerializeField] private StopsData _stopsData;
        [SerializeField] private TravelCostData _travelCostData;
        
        //we assume we have enough fuel to always depart
        public void DepartToMandatoryStop()
        {
            TrainDataHandler.ExpendFuel(_travelCostData.MandatoryStopFuelCost);
            
            DetermineStopEncounter();
            
            TrainDataHandler.AddUpcomingStop(_stopsData.MandatoryStops[Random.Range(0, _stopsData.MandatoryStops.Count)]);
            
            TravelToStop();
        }

        private void DetermineStopEncounter()
        {
            float randomValue = Random.value;

            if(randomValue <= _stopsData.ServiceDisruptionEncounterChance && _stopsData.ServiceDisruptions.Count > 0)
            {
                TrainDataHandler.AddUpcomingStop(_stopsData.ServiceDisruptions[Random.Range(0, _stopsData.ServiceDisruptions.Count)]);
            }
            else if(randomValue <= _stopsData.FlagStopEncounterChance + _stopsData.ServiceDisruptionEncounterChance && _stopsData.FlagStops.Count > 0)
            {
                TrainDataHandler.AddUpcomingStop(_stopsData.FlagStops[Random.Range(0, _stopsData.FlagStops.Count)]);
            }
        }

        private void TravelToStop()
        {
            SceneManager.LoadScene(TrainDataHandler.NextStop().SceneName);
        }
    }
}
