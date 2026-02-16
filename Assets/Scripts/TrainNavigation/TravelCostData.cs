using UnityEngine;

namespace TrainNavigation
{
    /// <summary>
    /// This class holds all the data relating to the fuel cost of different events during train travel.
    /// </summary>
    [CreateAssetMenu(fileName = "TravelCosts", menuName = "TrainNavigation/TravelCost", order = 0)]
    public class TravelCostData : ScriptableObject
    {
        public float MandatoryStopFuelCost = 10f;
        public float MandatoryStopSuccessFuelCost = 0f;
        public float MandatoryStopFailureFuelCost = 10f;
        public float FlagStopFuelCost = 5f;
        public float FlagStopSuccessFuelCost = 0f;
        public float FlagStopFailureFuelCost = 5f;
        public float ServiceDisruptionFuelCost = 0f;
        public float ServiceDisruptionSuccessFuelCost = 0f;
        public float ServiceDisruptionFailureFuelCost = 1f;
    }
}
