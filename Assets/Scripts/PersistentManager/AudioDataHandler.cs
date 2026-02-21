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

        public static class Player
        {
            public static EventReference PlayerSwimStrong() => _soundLibrary.PlayerSoundLibrary.PlayerSwimStrong;
            public static EventReference PlayerSwimWeak() => _soundLibrary.PlayerSoundLibrary.PlayerSwimWeak;
            public static EventReference PlayerStep() => _soundLibrary.PlayerSoundLibrary.PlayerStep;
        }

        public static class MinigameWiring
        {
            public static EventReference EyeStretchLong() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchLong;
            public static EventReference EyeStretchShort() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchShort;
            public static EventReference WireCircuit() => _soundLibrary.MinigameWireSoundLibrary.WireCircuit;
        }
    }
}
