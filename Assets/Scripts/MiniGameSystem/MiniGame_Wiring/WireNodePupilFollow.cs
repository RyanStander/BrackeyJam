using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameSystem.MiniGame_Wiring
{
    public class WireNodePupilFollow : MonoBehaviour
    {
        [SerializeField] private float _maxDistancePercent = 0.4f; // 40% of parent width
        [SerializeField] private float _followSpeed = 10f;
        private bool _followMouse = true;
        private Transform _eyeTarget;

        private RectTransform _parentRect;

        private void Start()
        {
            _parentRect = transform.parent as RectTransform;
        }

        private void FixedUpdate()
        {
            Vector2 targetPosition;

            if (_followMouse)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _parentRect,
                    Input.mousePosition,
                    null,
                    out targetPosition
                );
            }
            else
            {
                targetPosition = _parentRect.InverseTransformPoint(_eyeTarget.position);
            }

            // Max distance based on parent's width (in canvas units)
            float maxDistance = _parentRect.rect.width * _maxDistancePercent;

            Vector2 offset = targetPosition;
            Vector2 clampedOffset = Vector2.ClampMagnitude(offset, maxDistance);

            transform.localPosition =
                Vector2.MoveTowards(transform.localPosition, clampedOffset, _followSpeed * Time.deltaTime);
        }
        
        public void FollowEyeTarget(Transform target)
        {
            _eyeTarget = target;
            _followMouse = false;
        }
    }
}
