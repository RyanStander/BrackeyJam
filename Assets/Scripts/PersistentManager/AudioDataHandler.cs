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
            public static EventReference FuelPickup() => _soundLibrary.TrainTravelSoundLibrary.FuelPickup;
        }

        public static class UI
        {
            public static EventReference ButtonClick() => _soundLibrary.UISoundLibrary.ButtonClick;
        }

        public static class Character
        {
            public static EventReference PlayerSwimStrong() => _soundLibrary.CharacterSoundLibrary.PlayerSwimStrong;
            public static EventReference PlayerSwimWeak() => _soundLibrary.CharacterSoundLibrary.PlayerSwimWeak;
            public static EventReference PlayerStep() => _soundLibrary.CharacterSoundLibrary.PlayerStep;
            
            public static EventReference DealerDialogue() => _soundLibrary.CharacterSoundLibrary.DealerDialogue;
            public static EventReference DrawsCard() => _soundLibrary.CharacterSoundLibrary.DrawsCard;
            public static EventReference HitsTable() => _soundLibrary.CharacterSoundLibrary.HitsTable;
            public static EventReference TauntLaugh() => _soundLibrary.CharacterSoundLibrary.TauntLaugh;
            
            public static EventReference BigSquidDialogue() => _soundLibrary.CharacterSoundLibrary.BigSquidDialogue;
            public static EventReference GeneralHighDialogue() => _soundLibrary.CharacterSoundLibrary.GeneralHighDialogue;
            public static EventReference GeneralLowDialogue() => _soundLibrary.CharacterSoundLibrary.GeneralLowDialogue;
        }

        #region Stations

        public static class StationUnderwater
        {
            public static EventReference UnderwaterMusic() => _soundLibrary.StationUnderwaterSoundLibrary.UnderwaterMusic;
            public static EventReference UnderwaterAmbience() => _soundLibrary.StationUnderwaterSoundLibrary.UnderwaterAmbience;
            public static EventReference MiscWaterMove() => _soundLibrary.StationUnderwaterSoundLibrary.MiscWaterMove;
            public static EventReference OxygenPickup() => _soundLibrary.StationUnderwaterSoundLibrary.OxygenPickup;
            public static EventReference PrawnsScatter() => _soundLibrary.StationUnderwaterSoundLibrary.PrawnsScatter;
            public static EventReference WaterLevelRises() => _soundLibrary.StationUnderwaterSoundLibrary.WaterLevelRises;
        }
        
        public static class StationMonster
        {
            public static EventReference MonsterMusic() => _soundLibrary.StationMonsterSoundLibrary.MonsterMusic;
            public static EventReference MonsterAmbience() => _soundLibrary.StationMonsterSoundLibrary.MonsterAmbience;
            public static EventReference Growl() => _soundLibrary.StationMonsterSoundLibrary.Growl;
            public static EventReference Idle() => _soundLibrary.StationMonsterSoundLibrary.Idle;
            public static EventReference Squish() => _soundLibrary.StationMonsterSoundLibrary.Squish;
            public static EventReference SquishStep() => _soundLibrary.StationMonsterSoundLibrary.SquishStep;
        }

        public static class StationCasino
        {
            public static EventReference BlackjackMusic() => _soundLibrary.MinigameBlackjackSoundLibrary.BlackjackMusic;
            public static EventReference CasinoMusic()=> _soundLibrary.MinigameBlackjackSoundLibrary.CasinoMusic;
        }

        #endregion
        
        #region Minigames
        
        public static class MinigameWiring
        {
            public static EventReference EyeStretchLong() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchLong;
            public static EventReference EyeStretchShort() => _soundLibrary.MinigameWireSoundLibrary.EyeStretchShort;
            public static EventReference WireCabinetOpen() => _soundLibrary.MinigameWireSoundLibrary.WireCabinetOpen;
            public static EventReference WireCabinetClose() => _soundLibrary.MinigameWireSoundLibrary.WireCabinetClose;
        }
        
        public static class MinigamePipes
        {
            public static EventReference PipeRotate() => _soundLibrary.MinigamePipesSoundLibrary.PipeRotate;
            public static EventReference PipesDone() => _soundLibrary.MinigamePipesSoundLibrary.PipesDone;
        }
        
        public static class MinigameValve
        {
            public static EventReference HandleTurn() => _soundLibrary.MinigameValveSoundLibrary.HandleTurn;
            public static EventReference GaugeTick() => _soundLibrary.MinigameValveSoundLibrary.GaugeTick;
            public static EventReference QteHit() => _soundLibrary.MinigameValveSoundLibrary.QteHit;
            public static EventReference QteMiss() => _soundLibrary.MinigameValveSoundLibrary.QteMiss;
            public static EventReference QteSuccess() => _soundLibrary.MinigameValveSoundLibrary.QteSuccess;
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
