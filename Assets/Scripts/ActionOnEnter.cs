using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When the player enters the collider of this object it should trigger an event that can be used to trigger an action, such as a monster appearing or a door opening.
/// </summary>
public class ActionOnEnter : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnter.Invoke();
        }
    }
}
