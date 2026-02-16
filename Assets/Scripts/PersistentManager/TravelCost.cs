namespace PersistentManager
{
    /// <summary>
    /// Returns all travel cost values
    /// </summary>
    public static class TravelCost
    {
        public static int MandatoryStopFuelCost => PersistentManager.Instance.TravelCostData.MandatoryStopFuelCost;
        public static int MandatoryStopSuccessRefuel => PersistentManager.Instance.TravelCostData.MandatoryStopSuccessRefuel;
        public static int MandatoryStopFailureFuelCost => PersistentManager.Instance.TravelCostData.MandatoryStopFailureFuelCost;
        public static int FlagStopFuelCost => PersistentManager.Instance.TravelCostData.FlagStopFuelCost;
        public static int FlagStopSuccessRefuel => PersistentManager.Instance.TravelCostData.FlagStopSuccessRefuel;
        public static int FlagStopFailureFuelCost => PersistentManager.Instance.TravelCostData.FlagStopFailureFuelCost;
        public static int ServiceDisruptionFuelCost => PersistentManager.Instance.TravelCostData.ServiceDisruptionFuelCost;
        public static int ServiceDisruptionSuccessRefuel => PersistentManager.Instance.TravelCostData.ServiceDisruptionSuccessRefuel;
        public static int ServiceDisruptionFailureFuelCost => PersistentManager.Instance.TravelCostData.ServiceDisruptionFailureFuelCost;
        
    }
}
