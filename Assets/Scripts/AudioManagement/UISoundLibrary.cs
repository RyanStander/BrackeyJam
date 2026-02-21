using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "UISoundLibrary", menuName = "Audio/UI Sounds", order = 1)]
    public class UISoundLibrary : ScriptableObject
    {
        public EventReference ButtonClick;
    }
}
