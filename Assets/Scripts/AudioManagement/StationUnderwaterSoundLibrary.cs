using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "StationUnderwaterSoundLibrary", menuName = "Audio/Station Underwater Sounds", order = 3)]
    public class StationUnderwaterSoundLibrary : ScriptableObject
    {
        public EventReference UnderwaterMusic;
        public EventReference UnderwaterAmbience;
        public EventReference MiscWaterMove;
        public EventReference PrawnsScatter;
        public EventReference WaterLevelRises;
    }
}
