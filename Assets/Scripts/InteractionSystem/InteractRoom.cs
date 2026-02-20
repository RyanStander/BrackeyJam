using System;
using System.Collections;
using UI;
using UnityEngine;

namespace InteractionSystem
{
    public class InteractRoom : MonoBehaviour, IInteractable
    {
        public Transform roomEntryPoint;
        [SerializeField] private GameObject[] _objectsToActivate;
        [SerializeField] private GameObject[] _objectsToDeactivate;
        [SerializeField] private FadeToBlack _fadeToBlack;
        private Coroutine _movePlayerCoroutine;

        private void OnValidate()
        {
            if (_fadeToBlack == null)
                _fadeToBlack = FindObjectOfType<FadeToBlack>();
        }

        public bool Interact(Interaction interaction)
        {
            _movePlayerCoroutine =StartCoroutine(MovePlayerToRoom(interaction.gameObject));
        
            return true;
        }

        private IEnumerator MovePlayerToRoom(GameObject player)
        {
                _fadeToBlack.StartFadeToBlack();
                while (_fadeToBlack.IsFading())
                {
                    yield return null;
                }
                foreach (GameObject activateObject in _objectsToActivate)
                {
                    activateObject.SetActive(true);
                }
        
                foreach (GameObject deactivateObject in _objectsToDeactivate)
                {
                    deactivateObject.SetActive(false);
                }
                
                player.transform.position = roomEntryPoint.position;
        }
    }
}
