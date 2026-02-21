using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem
{
    public class InteractPerformAction : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent _onEnter;
        public bool Interact(Interaction Interaction)
        {
            _onEnter.Invoke();
            return true;
        }
    }
}
