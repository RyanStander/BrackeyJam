using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library", order = 0)]
    public class SoundLibrary : ScriptableObject
    {
        public CharacterSoundLibrary CharacterSoundLibrary;
        public UISoundLibrary UISoundLibrary;
        
        public StationMonsterSoundLibrary StationMonsterSoundLibrary;
        public StationUnderwaterSoundLibrary StationUnderwaterSoundLibrary;
        
        public MinigameBlackjackSoundLibrary MinigameBlackjackSoundLibrary;
        public MinigameValveSoundLibrary MinigameValveSoundLibrary;
        public MinigamePipesSoundLibrary MinigamePipesSoundLibrary;
        public MinigameWireSoundLibrary MinigameWireSoundLibrary;
        
        public TrainTravelSoundLibrary TrainTravelSoundLibrary;
    }
}
