using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// / <summary>
// / each node is a connection point for a wire. 
// / we begin by setting nodes to grey, then begin couroutines on each one to randomly flicker their color
// / when you click a node, it should stop flickering and show its real color, then spawn a wire that follows the mouse
// / </summary>
namespace MiniGameSystem.MiniGame_Wiring
{
    public class WiringNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
    {
        [Header("State")] public int ColorID;
        public bool IsLeftSide;
        [SerializeField] private bool _isConnected;
        [SerializeField] private Image _pupilImage;
        [SerializeField] private WireNodePupilFollow _pupilFollow;
        [SerializeField] private Animator _eyelidAnimator;
        [SerializeField] private float _blinkSpeedRange = 1.25f;
        private float _blinkInterval = 3.0f;
        private float _blinkSpeedMultiplier = 0.5f;

        private WiringMinigame _wireGame;
        private Color _realColor;
        private Coroutine _blinkCoroutine;

        private void OnValidate()
        {
            if (_pupilFollow == null)
                _pupilFollow = GetComponentInChildren<WireNodePupilFollow>();

            if (_eyelidAnimator == null)
                _eyelidAnimator = GetComponentInChildren<Animator>();
        }

        public void Setup(WiringMinigame wiregame, int id, Color color, float blinkInterval = -1,
            float blinkDuration = 0)
        {
            _wireGame = wiregame;
            ColorID = id;
            _realColor = color;
            _isConnected = false;

            if (_pupilImage == null)
                _pupilImage = GetComponent<Image>();

            _pupilImage.color = color;

            _blinkInterval = blinkInterval;
            _blinkSpeedMultiplier = blinkDuration;

            if (_blinkInterval > 0 && _blinkSpeedMultiplier > 0)
            {
                if (_blinkCoroutine != null)
                    StopCoroutine(_blinkCoroutine);

                _blinkCoroutine = StartCoroutine(FlickerRoutine());
            }
        }

        IEnumerator FlickerRoutine()
        {
            yield return new WaitForSeconds(Random.Range(0f, _blinkInterval));

            while (!_isConnected)
            {
                _eyelidAnimator.Play("EyeBlink", 0,
                    _blinkSpeedMultiplier +
                    Random.Range(_blinkSpeedMultiplier / _blinkSpeedRange, _blinkSpeedMultiplier * _blinkSpeedRange));
                yield return new WaitForSeconds(_blinkSpeedMultiplier *
                                                _eyelidAnimator.GetCurrentAnimatorStateInfo(0).length);

                if (_isConnected) break;
                
                yield return new WaitForSeconds(_blinkInterval + Random.Range(0f, _blinkInterval));
            }
        }

        //a bit hacky but I wanted to make sure the node color is set right once you connect it
        public void SetConnected(Transform eyeTarget)
        {
            _isConnected = true;
            if (_blinkCoroutine != null) StopCoroutine(_blinkCoroutine);
            //stop animation
            _eyelidAnimator.Play("EyesOpen", 0, 1);
            _pupilFollow.FollowEyeTarget(eyeTarget);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isConnected) _wireGame.SetHoveredNode(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _wireGame.SetHoveredNode(null);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _wireGame.EndDrag();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isConnected)
            {
                _wireGame.AttemptConnectionStart(this);
                _pupilImage.color = _realColor;
            }
        }
    }
}
