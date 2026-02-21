using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library", order = 0)]
    public class SoundLibrary : ScriptableObject
    {
        public PlayerSoundLibrary PlayerSoundLibrary;
        public UISoundLibrary UISoundLibrary;
        
        public MinigameBlackjackSoundLibrary MinigameBlackjackSoundLibrary;
        public MinigameValveSoundLibrary MinigameValveSoundLibrary;
        public MinigamePipesSoundLibrary MinigamePipesSoundLibrary;
        public MinigameWireSoundLibrary MinigameWireSoundLibrary;
        
        public TrainTravelSoundLibrary TrainTravelSoundLibrary;
    }
}
