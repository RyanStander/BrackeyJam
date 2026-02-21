using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "TrainTravelSoundLibrary", menuName = "Audio/Train Travel Sounds", order = 1)]
    public class TrainTravelSoundLibrary : ScriptableObject
    {
        public EventReference Departure;
        public EventReference FuelSpend;
    }
}
