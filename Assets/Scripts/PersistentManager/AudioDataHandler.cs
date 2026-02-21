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

        #region Stations

        public static class StationUnderwater
        {
            public static EventReference UnderwaterMusic() => _soundLibrary.StationUnderwaterSoundLibrary.UnderwaterMusic;
            public static EventReference UnderwaterAmbience() => _soundLibrary.StationUnderwaterSoundLibrary.UnderwaterAmbience;
            public static EventReference MiscWaterMove() => _soundLibrary.StationUnderwaterSoundLibrary.MiscWaterMove;
            public static EventReference PrawnsScatter() => _soundLibrary.StationUnderwaterSoundLibrary.PrawnsScatter;
            public static EventReference WaterLevelRises() => _soundLibrary.StationUnderwaterSoundLibrary.WaterLevelRises;
        }
        
        public static class StationMonster
        {
            public static EventReference Growl() => _soundLibrary.StationMonsterSoundLibrary.Growl;
            public static EventReference Idle() => _soundLibrary.StationMonsterSoundLibrary.Idle;
            public static EventReference Squish() => _soundLibrary.StationMonsterSoundLibrary.Squish;
            public static EventReference SquishStep() => _soundLibrary.StationMonsterSoundLibrary.SquishStep;
        }

        #endregion
        
        #region Minigames
        
        public static class MinigameWiring
        {
            public static EventReference EyeStretchLong() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchLong;
            public static EventReference EyeStretchShort() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchShort;
            public static EventReference WireCircuit() => _soundLibrary.MinigameWireSoundLibrary.WireCircuit;
        }
        
        public static class MinigamePipes
        {
            public static EventReference PipeRotate() => _soundLibrary.MinigamePipesSoundLibrary.PipeRotate;
        }
        
        public static class MinigameValve
        {
            public static EventReference HandleTurn() => _soundLibrary.MinigameValveSoundLibrary.HandleTurn;
        }
        
        public static class MinigameBlackjack
        {
            public static EventReference Deal() => _soundLibrary.MinigameBlackjackSoundLibrary.Deal;
            public static EventReference Hit() => _soundLibrary.MinigameBlackjackSoundLibrary.Hit;
            public static EventReference Stab() => _soundLibrary.MinigameBlackjackSoundLibrary.Stab;
        }
        
        #endregion

        
    }
}
