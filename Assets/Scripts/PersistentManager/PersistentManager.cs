using System;
using System.Collections.Generic;
using TrainNavigation;
using UnityEngine;

namespace PersistentManager
{
    /// <summary>
    /// This class holds all important data that needs to persist between scenes.
    /// No functions should be in this class, only data
    /// Handler classes should be created to manage this data
    /// </summary>
    public class PersistentManager : MonoBehaviour
    {
        public static PersistentManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #region Train Data

        [Header("Train Data")] public int Fuel;
        public TravelCostData TravelCostData;
        public int RouteProgress;
        public PossibleStop CurrentStop;
        public Queue<PossibleStop> UpcomingStops = new();
        #endregion
    }
}
