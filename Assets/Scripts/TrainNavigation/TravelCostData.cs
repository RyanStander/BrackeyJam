using UnityEngine;

namespace TrainNavigation
{
    /// <summary>
    /// This class holds all the data relating to the fuel cost of different events during train travel.
    /// </summary>
    [CreateAssetMenu(fileName = "TravelCosts", menuName = "TrainNavigation/TravelCost", order = 0)]
    public class TravelCostData : ScriptableObject
    {
        public int MandatoryStopFuelCost = 1;
        public int MandatoryStopSuccessRefuel = 1;
        public int MandatoryStopFailureFuelCost = 0;
        public int FlagStopFuelCost = 1;
        public int FlagStopSuccessRefuel = 4;
        public int FlagStopFailureFuelCost = 2;
        public int ServiceDisruptionFuelCost = 0;
        public int ServiceDisruptionSuccessRefuel = 0;
        public int ServiceDisruptionFailureFuelCost = 1;
    }
}
