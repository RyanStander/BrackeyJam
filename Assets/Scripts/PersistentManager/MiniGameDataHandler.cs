namespace PersistentManager
{
    public static class MiniGameDataHandler
    {
        public static void CompletedMiniGame(int refuelAmount)
        {
            TrainDataHandler.Refuel(refuelAmount);
        }

        public static void FailedMiniGame(int fuelCost)
        {
            TrainDataHandler.ExpendFuel(fuelCost);
        }
    }
}
