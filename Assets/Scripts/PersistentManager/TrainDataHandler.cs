using Events;
using TrainNavigation;
using UnityEngine;

namespace PersistentManager
{
    /// <summary>
    /// Handles all data relating to the train
    /// </summary>
    public static class TrainDataHandler
    {
        public static bool ExpendFuel(float amount)
        {
            PersistentManager.Instance.Fuel -= amount;

            return PersistentManager.Instance.Fuel <= 0;
        }

        public static void Refuel(float amount)
        {
            PersistentManager.Instance.Fuel += amount;
        }
        
        public static void AdvanceRouteProgress()
        {
            PersistentManager.Instance.RouteProgress++;
        }
        
        public static void AddUpcomingStop(PossibleStop stop)
        {
            PersistentManager.Instance.UpcomingStops.Enqueue(stop);
        }

        public static PossibleStop NextStop()
        {
            if (PersistentManager.Instance.UpcomingStops.Count > 0)
            {   
                PersistentManager.Instance.CurrentStop = PersistentManager.Instance.UpcomingStops.Dequeue();
                return PersistentManager.Instance.CurrentStop;
            }

            Debug.LogError("No upcoming stops in queue.");
            return null;
        }
    }
}
