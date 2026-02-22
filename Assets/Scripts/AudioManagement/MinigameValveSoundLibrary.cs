using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "MinigameValveSoundLibrary", menuName = "Audio/Minigame Valve Sounds", order = 2)]
    public class MinigameValveSoundLibrary : ScriptableObject
    {
        public EventReference HandleTurn;
        public EventReference GaugeTick;
        public EventReference QteHit;
        public EventReference QteMiss;
        public EventReference QteSuccess;
    }
}
