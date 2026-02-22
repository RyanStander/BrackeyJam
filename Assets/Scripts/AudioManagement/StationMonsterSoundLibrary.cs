using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "StationMonsterSoundLibrary", menuName = "Audio/Station Monster Sounds", order = 3)]
    public class StationMonsterSoundLibrary : ScriptableObject
    {
        public EventReference MonsterMusic;
        public EventReference MonsterAmbience;
        public EventReference Growl;
        public EventReference Idle;
        public EventReference Squish;
        public EventReference SquishStep;
    }
}
