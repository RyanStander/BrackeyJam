namespace PersistentManager
{
    /// <summary>
    /// Returns all travel cost values
    /// </summary>
    public static class TravelCost
    {
        public static float MandatoryStopFuelCost => PersistentManager.Instance.TravelCostData.MandatoryStopFuelCost;
        public static float MandatoryStopSuccessFuelCost => PersistentManager.Instance.TravelCostData.MandatoryStopSuccessFuelCost;
        public static float MandatoryStopFailureFuelCost => PersistentManager.Instance.TravelCostData.MandatoryStopFailureFuelCost;
        public static float FlagStopFuelCost => PersistentManager.Instance.TravelCostData.FlagStopFuelCost;
        public static float FlagStopSuccessFuelCost => PersistentManager.Instance.TravelCostData.FlagStopSuccessFuelCost;
        public static float FlagStopFailureFuelCost => PersistentManager.Instance.TravelCostData.FlagStopFailureFuelCost;
        public static float ServiceDisruptionFuelCost => PersistentManager.Instance.TravelCostData.ServiceDisruptionFuelCost;
        public static float ServiceDisruptionSuccessFuelCost => PersistentManager.Instance.TravelCostData.ServiceDisruptionSuccessFuelCost;
        public static float ServiceDisruptionFailureFuelCost => PersistentManager.Instance.TravelCostData.ServiceDisruptionFailureFuelCost;
        
    }
}
