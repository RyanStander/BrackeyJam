using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "MinigameWireSoundLibrary", menuName = "Audio/Minigame Wire Sounds", order = 2)]
    public class MinigameWireSoundLibrary : ScriptableObject
    {
        public EventReference EyeStretchLong;
        public EventReference EyeStretchShort;
        public EventReference WireCabinetClose;
        public EventReference WireCabinetOpen;
    }
}
