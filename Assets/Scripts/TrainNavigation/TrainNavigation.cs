using System;
using System.Collections;
using PersistentManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace TrainNavigation
{
    /// <summary>
    /// We check if we have enough fuel to perform a standard trip, if not then we run out before we reach the destination
    /// If we do have fuel, then we expend the required fuel for a basic trip.
    /// </summary>
    public class TrainNavigation : MonoBehaviour
    {
        [SerializeField] private StopsData _stopsData;
        [SerializeField] private TravelUIHandler _travelUIHandler;

        private void OnValidate()
        {
            if (_travelUIHandler == null)
                _travelUIHandler = FindObjectOfType<TravelUIHandler>();
        }

        //we assume we have enough fuel to always depart
        public void DepartToMandatoryStop()
        {
            if(!TrainDataHandler.HasUpcomingStops())
            {
                TrainDataHandler.ExpendFuel(TravelCost.MandatoryStopFuelCost);
                DetermineStopEncounter();
                /*TrainDataHandler.AddUpcomingStop(
                    _stopsData.MandatoryStops[Random.Range(0, _stopsData.MandatoryStops.Count)]);*/
            }

            StartCoroutine(InitiateWarp());
        }

        private void DetermineStopEncounter()
        {
            if(TrainDataHandler.GetRouteProgress()>= _stopsData.MandatoryStops.Count)
                return;
            TrainDataHandler.AddUpcomingStop(_stopsData.MandatoryStops[TrainDataHandler.GetRouteProgress()]);
            TrainDataHandler.AdvanceRouteProgress();

            //no randoming for now :(
            /*float randomValue = Random.value;
             if(randomValue <= _stopsData.ServiceDisruptionEncounterChance && _stopsData.ServiceDisruptions.Count > 0)
            {
                TrainDataHandler.AddUpcomingStop(_stopsData.ServiceDisruptions[Random.Range(0, _stopsData.ServiceDisruptions.Count)]);
            }
            else if(randomValue <= _stopsData.FlagStopEncounterChance + _stopsData.ServiceDisruptionEncounterChance && _stopsData.FlagStops.Count > 0)
            {
                TrainDataHandler.AddUpcomingStop(_stopsData.FlagStops[Random.Range(0, _stopsData.FlagStops.Count)]);
            }*/
        }

        private IEnumerator InitiateWarp()
        {
            _travelUIHandler.StartTravelAnimation();
            
            //wait until ready to warp then load the next scene
            yield return new WaitUntil(() => _travelUIHandler.ReadyToWarp());
            
            SceneManager.LoadScene(TrainDataHandler.NextStop().SceneName);
        }
    }
}
