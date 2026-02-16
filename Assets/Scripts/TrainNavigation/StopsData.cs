using System.Collections.Generic;
using UnityEngine;

namespace TrainNavigation
{
    [CreateAssetMenu(fileName = "StopsData", menuName = "TrainNavigation/StopsData", order = 0)]
    public class StopsData : ScriptableObject
    {
        [Header("Stops Data")] public List<PossibleStop> MandatoryStops;
        public List<PossibleStop> FlagStops;
        public List<PossibleStop> ServiceDisruptions;

        [Header("Encounter Chances")]
        [Range(0f, 1f),
         Tooltip(
             "This calculation is performed against Service Disruption chance, 1-FlagStop+ServiceDistruption=Chance of neither happening.")]
        public float FlagStopEncounterChance = 0.20f;

        [Range(0f, 1f),
         Tooltip(
             "This calculation is performed against FlagStop chance, 1-FlagStop+ServiceDistruption=Chance of neither happening.")]
        public float ServiceDisruptionEncounterChance = 0.10f;
    }
}
