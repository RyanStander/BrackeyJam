using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library", order = 0)]
    public class SoundLibrary : ScriptableObject
    {
        public TrainTravelSoundLibrary TrainTravelSoundLibrary;
        public UISoundLibrary UISoundLibrary;
    }
}
