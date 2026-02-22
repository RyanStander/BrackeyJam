using System.Collections;
using System.Collections.Generic;
using AudioManagement;
using PersistentManager;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameSystem.MiniGame_Wiring
{
    /// <summary>
    /// going to spam comments here because this is like the most complicated thing i've made
    /// - each node needs to know its color and whether it's on the left or right side
    /// - when you click a node, it should spawn a line and follow the mouse until you release it
    /// - if you release it over a node of the opposite side and same color, it should snap to that node and mark both as connected
    /// - if you release it anywhere else, it should just disappear and reset the state so you can try again
    /// - once all nodes are connected, it should trigger the win condition
    /// </summary>
    public class WiringMinigame : BaseMinigame
    {
        [Header("configuration")] [SerializeField]
        private int _wireCount = 6;

        [SerializeField] private List<Color> _availableColors;
        [SerializeField] private float _nodeBlinkInterval = 3f;
        [SerializeField] private float _nodeBlinkSpeedMultiplier = 1f;
        [SerializeField] private float _lineThickness = 15f;

        [Header("references")] public GameObject WirePrefab;
        [SerializeField] private GameObject _nodePrefab;
        [SerializeField] private Transform _wireParent;
        [SerializeField] private Transform _leftContainer;
        [SerializeField] private Transform _rightContainer;
        [SerializeField] private RectTransform _canvas;


        private WiringNode _currentStartNode;
        private RectTransform _currentWire;
        private int _matchesMade = 0;

        // track the node we are currently hovering over
        private WiringNode _currentHoveredNode;

        public override void StartMinigame()
        {
            base.StartMinigame();

            _matchesMade = 0;
            _currentStartNode = null;
            _currentWire = null;

            List<int> allColorIndices = new List<int>();
            for (int i = 0; i < _availableColors.Count; i++) allColorIndices.Add(i);

            ShuffleList(allColorIndices);
            //grab all colors from our list we defined in inspector, then shuffle it

            int count = Mathf.Min(_wireCount, _availableColors.Count);
            List<int> selectedColors = allColorIndices.GetRange(0, count);

            //based on the amount we defined, spawn nodes on left side with colors from our shuffled list
            for (int i = 0; i < count; i++)
            {
                int id = selectedColors[i];
                WiringNode node = Instantiate(_nodePrefab, _leftContainer).GetComponent<WiringNode>();
                node.Setup(this, id, _availableColors[id], _nodeBlinkInterval, _nodeBlinkSpeedMultiplier);
                node.IsLeftSide = true;
                node.transform.localScale = new Vector3(-1, 1, 1);
            }

            ShuffleList(selectedColors);

            //based on the amount we defined, spawn nodes on right side with colors from our shuffled list
            // we also shuffle again to make sure both sides are different order than eachother.
            for (int i = 0; i < count; i++)
            {
                int id = selectedColors[i];
                WiringNode node = Instantiate(_nodePrefab, _rightContainer).GetComponent<WiringNode>();
                node.Setup(this, id, _availableColors[id], _nodeBlinkInterval, _nodeBlinkSpeedMultiplier);
                node.IsLeftSide = false;
            }
            
            AudioManager.PlayOneShot(AudioDataHandler.MinigameWiring.WireCabinetOpen());
        }

        //called during onpointerdown from WiringNode, spawn a wire, grab it's node color
        public void AttemptConnectionStart(WiringNode node)
        {
            if (_currentStartNode != null) return;

            //we dont want player to touch right side
            if (!node.IsLeftSide)
                return;
            
            _currentStartNode = node;
            AudioManager.PlayOneShot(AudioDataHandler.MinigameWiring.EyeStretchLong());

            GameObject newLine = Instantiate(WirePrefab, _wireParent);
            _currentWire = newLine.GetComponent<RectTransform>();
            _currentWire.position = node.transform.position;
            newLine.GetComponent<Image>().color = _availableColors[node.ColorID];

            newLine.GetComponent<Image>().raycastTarget = false;
        }

        //called on onpointer up from WiringNode, make sure it's a valid node and the colorID's match, then finish connect
        public void AttemptConnectionEnd(WiringNode endNode)
        {
            if (_currentStartNode == null) return;
            if (_currentStartNode == endNode) return;
            if (_currentStartNode.IsLeftSide == endNode.IsLeftSide) return;

            if (_currentStartNode.ColorID == endNode.ColorID)
            {
                FinishConnection(endNode);
            }
        }

        private void FinishConnection(WiringNode endNode)
        {
            UpdateLineVisual(_currentStartNode.transform.position, endNode.transform.position);

            _currentStartNode.SetConnected(endNode.transform);
            endNode.SetConnected(_currentStartNode.transform);

            _currentStartNode = null;
            _currentWire = null;
            _matchesMade++;


            if (_matchesMade >= Mathf.Min(_wireCount, _availableColors.Count))
            {
                StartCoroutine(WinSequence());
            }
        }

        public void EndDrag()
        {
            if (_currentStartNode != null && _currentWire != null)
            {
                if (_currentHoveredNode != null)
                {
                    AttemptConnectionEnd(_currentHoveredNode);
                }

                if (_currentStartNode != null)
                {
                    Destroy(_currentWire.gameObject);
                    _currentStartNode = null;
                    _currentWire = null;
                }
            }
        }

        //Captains log 1453: the harsh storms of whatever the original of this was has caused many sailors to lose their lives,
        //but we have managed to push through, it is currently 2am and I have work in 4 hours, but at least no more lives
        //will be lost to a line rendered...
        private void Update()
        {
            if (_currentStartNode != null && _currentWire != null)
            {
                // convert screen mouse to world position in the canvas
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, Input.mousePosition,
                    null, out Vector3 mouseWorldPos);

                UpdateLineVisual(_currentStartNode.transform.position, mouseWorldPos);
            }
        }

        public void SetHoveredNode(WiringNode node)
        {
            _currentHoveredNode = node;
        }

        //i'm not gonna lie this was from a tutorial but it works so who cares, calculates stretchy lines from two points 
        //edit to the above: I hate whoever gave you this tutorial.
        private void UpdateLineVisual(Vector3 startPos, Vector3 endPos)
        {
            Vector3 startLocal = _wireParent.InverseTransformPoint(startPos);
            Vector3 endLocal = _wireParent.InverseTransformPoint(endPos);

            _currentWire.localPosition = startLocal;

            Vector3 direction = endLocal - startLocal;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentWire.localRotation = Quaternion.Euler(0, 0, angle);

            // scale the width along local x-axis
            _currentWire.sizeDelta = new Vector2(direction.magnitude, _lineThickness);
        }

        //fisher-yates shuffle, also from a tutorial
        private void ShuffleList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int temp = list[i];
                int rnd = Random.Range(i, list.Count);
                list[i] = list[rnd];
                list[rnd] = temp;
            }
        }

        IEnumerator WinSequence()
        {
            yield return new WaitForSeconds(2f);
            AudioManager.PlayOneShot(AudioDataHandler.MinigameWiring.WireCabinetClose());
            FinishGame(true);
        }
    }
}
