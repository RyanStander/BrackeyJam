using AudioManagement;
using FMODUnity;

namespace PersistentManager
{
    public static class AudioDataHandler
    {
        private static SoundLibrary _soundLibrary => PersistentManager.Instance.SoundLibrary;

        public static class TrainTravel
        {
            public static EventReference Departure() => _soundLibrary.TrainTravelSoundLibrary.Departure;
            public static EventReference FuelSpend() => _soundLibrary.TrainTravelSoundLibrary.FuelSpend;

        }

        public static class UI
        {
            public static EventReference ButtonClick() => _soundLibrary.UISoundLibrary.ButtonClick;
        }
    }
}
