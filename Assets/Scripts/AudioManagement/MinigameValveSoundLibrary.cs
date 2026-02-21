using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "MinigameValveSoundLibrary", menuName = "Audio/Minigame Valve Sounds", order = 2)]
    public class MinigameValveSoundLibrary : ScriptableObject
    {
        public EventReference HandleTurn;
    }
}
