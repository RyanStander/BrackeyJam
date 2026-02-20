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
        [Header("State")]
        public int ColorID;
        public bool IsLeftSide;
        [SerializeField] private bool _isConnected;
        [SerializeField] private Image _pupilImage;
        [SerializeField] private WireNodePupilFollow _pupilFollow;
        private float _blinkInterval = 3.0f;
        private float _blinkDuration = 0.5f;
        
        private WiringMinigame _wireGame;
        private Color _realColor;
        private Coroutine _flickerCoroutine;

        private void OnValidate()
        {
            if (_pupilFollow == null) 
                _pupilFollow = GetComponentInChildren<WireNodePupilFollow>();
        }

        public void Setup(WiringMinigame wiregame, int id, Color color, float blinkInterval = -1, float blinkDuration = 0)
        {
            _wireGame = wiregame;
            ColorID = id;
            _realColor = color;
            _isConnected = false;

            if (_pupilImage == null) 
                _pupilImage = GetComponent<Image>();

            _pupilImage.color = color;

            _blinkInterval = blinkInterval;
            _blinkDuration = blinkDuration;
            
            if (_blinkInterval > 0 && _blinkDuration > 0)
            {
                _pupilImage.color = Color.white;
                if (_flickerCoroutine != null)
                    StopCoroutine(_flickerCoroutine);
                
                _flickerCoroutine = StartCoroutine(FlickerRoutine());
            }
        }

        IEnumerator FlickerRoutine()
        {
            yield return new WaitForSeconds(Random.Range(0f, 3f));

            while (!_isConnected)
            {
                _pupilImage.color = _realColor;
                yield return new WaitForSeconds(_blinkDuration);

                if (_isConnected) break;

                _pupilImage.color = Color.white;
                yield return new WaitForSeconds(_blinkInterval + Random.Range(0f, 3f));
            }
            _pupilImage.color = _realColor;
        }

        //a bit hacky but I wanted to make sure the node color is set right once you connect it
        public void SetConnected(Transform eyeTarget)
        {
            _isConnected = true;
            if (_flickerCoroutine != null) StopCoroutine(_flickerCoroutine);
            _pupilImage.color = _realColor;
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
            if (!_isConnected)
            {
                _pupilImage.color = Color.white; //wanted it to immediately turn to white when released to stop cheating
            }
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
