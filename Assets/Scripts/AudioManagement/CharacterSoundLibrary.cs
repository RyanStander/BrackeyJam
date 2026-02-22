using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "CharacterSoundLibrary", menuName = "Audio/Character Sounds", order = 1)]
    public class CharacterSoundLibrary : ScriptableObject
    {
        [Header("Player")]
        public EventReference PlayerSwimStrong;
        public EventReference PlayerSwimWeak;
        public EventReference PlayerStep;

        [Header("Dealer")] public EventReference DealerDialogue;
        public EventReference DrawsCard;
        public EventReference HitsTable;
        public EventReference TauntLaugh;
        
        [Header("Other")]
        public EventReference BigSquidDialogue;

        public EventReference GeneralHighDialogue;
        public EventReference GeneralLowDialogue;
    }
}
