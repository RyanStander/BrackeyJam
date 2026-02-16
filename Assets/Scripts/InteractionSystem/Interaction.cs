using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius = 1f;
    [SerializeField] private LayerMask _interactionLayer;

    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFoundColliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _numFoundColliders = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, 
            _interactionRadius, _colliders, _interactionLayer);

        if(_numFoundColliders > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if(interactable != null & Input.GetKeyDown(KeyCode.F))
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(_interactionPoint.position, Vector3.forward, _interactionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
    }
}
