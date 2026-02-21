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
        public static void ExpendFuel(int amount)
        {
            PersistentManager.Instance.Fuel -= amount;
            EventManager.currentManager.AddEvent(new FuelChanged());
        }

        public static void Refuel(int amount)
        {
            PersistentManager.Instance.Fuel += amount;
        }
        
        public static int GetFuelLevel()
        {
            return PersistentManager.Instance.Fuel;
        }

        public static bool CanContinue()
        {
            return PersistentManager.Instance.Fuel > 0;
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
        
        public static bool HasUpcomingStops()
        {
            return PersistentManager.Instance.UpcomingStops.Count > 0;
        }
        
        public static int GetRouteProgress()
        {
            return PersistentManager.Instance.RouteProgress;
        }
    }
}
