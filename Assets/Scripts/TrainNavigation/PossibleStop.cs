using UnityEngine;

namespace TrainNavigation
{
    [CreateAssetMenu(fileName = "PossibleStop", menuName = "TrainNavigation/PossibleStop", order = 0)]
    public class PossibleStop : ScriptableObject
    {
        public string SceneName;
        [TextArea]public string Description;
    }
}
