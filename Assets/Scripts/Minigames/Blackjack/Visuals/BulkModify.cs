using System;
using UnityEngine;

namespace Minigames.Blackjack.Visuals
{
    /// <summary>
    /// editor script that allows you to modify multiple card visuals at once, for example to change the color of all cards in the hand when the player wins or loses
    /// </summary>
    public class BulkModify : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToModify;
        [SerializeField] private Vector3 _positionOffset;

        private void OnValidate()
        {
            if (_objectsToModify == null || _objectsToModify.Length == 0)
            {
                Debug.LogWarning("No objects assigned to BulkModify on " + gameObject.name);
                return;
            }

            ModifyObjects();
        }

        private void ModifyObjects()
        {
            Vector3 offset = _positionOffset;
            foreach (GameObject obj in _objectsToModify)
            {
                obj.transform.localPosition = offset;
                offset += _positionOffset;
            }
        }
    }
}
