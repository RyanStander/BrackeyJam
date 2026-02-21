using FMODUnity;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "MinigamePipeSoundLibrary", menuName = "Audio/Minigame Pipe Sounds", order = 2)]
    public class MinigamePipesSoundLibrary : ScriptableObject
    {
        public EventReference PipeRotate;
    }
}
