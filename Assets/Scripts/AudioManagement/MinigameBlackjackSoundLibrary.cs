using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "MinigameBlackjackSoundLibrary", menuName = "Audio/Minigame Blackjack Sounds", order = 2)]
    public class MinigameBlackjackSoundLibrary : ScriptableObject
    {
        public EventReference Deal;
        public EventReference Hit;
        public EventReference Stab;
    }
}
