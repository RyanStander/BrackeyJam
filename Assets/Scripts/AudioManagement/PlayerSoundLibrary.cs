using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "PlayerSoundLibrary", menuName = "Audio/Player Sounds", order = 1)]
    public class PlayerSoundLibrary : ScriptableObject
    {
        public EventReference PlayerSwimStrong;
        public EventReference PlayerSwimWeak;
        public EventReference PlayerStep;
    }
}
