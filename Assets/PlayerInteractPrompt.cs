using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractPrompt : MonoBehaviour
{
    [SerializeField] private GameObject _interactPrompt;
    [SerializeField] private float _interactionRadius = 1.5f;
    [SerializeField] private LayerMask _interactLayerMask;
    [SerializeField] private Transform _detectionPoint;

    [SerializeField] private float _spacingAboveObject = 0.5f;

    void Start()
    {
        if (_interactPrompt != null)
        {
            _interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        Vector2 checkPosition = _detectionPoint != null ? _detectionPoint.position : transform.position;

        Collider2D hit = Physics2D.OverlapCircle(checkPosition, _interactionRadius, _interactLayerMask);

        if (hit != null && _interactPrompt != null)
        {
            _interactPrompt.SetActive(true);

            float objectTopEdge = hit.bounds.max.y;
            float objectCenterX = hit.bounds.center.x;

            Vector3 promptPosition = new Vector3(objectCenterX, objectTopEdge + _spacingAboveObject, transform.position.z);

            _interactPrompt.transform.position = promptPosition;
            _interactPrompt.transform.rotation = Quaternion.identity;
        }
        else if (_interactPrompt != null)
        {
            _interactPrompt.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 checkPosition = _detectionPoint != null ? _detectionPoint.position : transform.position;
        Gizmos.DrawWireSphere(checkPosition, _interactionRadius);
    }
}