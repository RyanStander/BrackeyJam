using UnityEngine;

namespace TrainNavigation
{
    [CreateAssetMenu(fileName = "TravelCosts", menuName = "TrainNavigation/TravelCost", order = 0)]
    public class TravelCostData : ScriptableObject
    {
        public float MandatoryStopFuelCost = 10f;
        public float MandatoryStopFailureFuelCost = 10f;
        public float FlagStopFuelCost = 5f;
        public float FlagStopFailureFuelCost = 5f;
        public float ServiceDisruptionFuelCost = 0f;
        public float ServiceDisruptionFailureFuelCost = 1f;
    }
}
