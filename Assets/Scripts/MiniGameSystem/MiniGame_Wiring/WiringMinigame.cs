using System.Collections;
using System.Collections.Generic;
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
        [Header("configuration")]
        public int wireCount = 6;
        public List<Color> AvailableColors;

        [Header("references")]
        public GameObject WirePrefab;
        public GameObject NodePrefab;
        public Transform WireParent;
        public Transform LeftContainer;
        public Transform RightContainer;
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private float _lineThickness = 15f;

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
            for (int i = 0; i < AvailableColors.Count; i++) allColorIndices.Add(i);

            ShuffleList(allColorIndices);
            //grab all colors from our list we defined in inspector, then shuffle it

            int count = Mathf.Min(wireCount, AvailableColors.Count);
            List<int> selectedColors = allColorIndices.GetRange(0, count);

            //based on the amount we defined, spawn nodes on left side with colors from our shuffled list
            for (int i = 0; i < count; i++) 
            {
                int id = selectedColors[i];
                WiringNode node = Instantiate(NodePrefab, LeftContainer).GetComponent<WiringNode>();
                node.Setup(this, id, AvailableColors[id]);
                node.IsLeftSide = true;
                node.transform.localScale = new Vector3(-1, 1, 1);
            }

            ShuffleList(selectedColors);

            //based on the amount we defined, spawn nodes on right side with colors from our shuffled list
            // we also shuffle again to make sure both sides are different order than eachother.
            for (int i = 0; i < count; i++)
            {
                int id = selectedColors[i];
                WiringNode node = Instantiate(NodePrefab, RightContainer).GetComponent<WiringNode>();
                node.Setup(this, id, AvailableColors[id]);
                node.IsLeftSide = false;
            }
        }

        //called during onpointerdown from WiringNode, spawn a wire, grab it's node color
        public void AttemptConnectionStart(WiringNode node) 
        {
            if (_currentStartNode != null) return;

            _currentStartNode = node;

            GameObject newLine = Instantiate(WirePrefab, WireParent);
            _currentWire = newLine.GetComponent<RectTransform>();
            _currentWire.position = node.transform.position;
            newLine.GetComponent<Image>().color = AvailableColors[node.ColorID];

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

            _currentStartNode.SetConnected();
            endNode.SetConnected();

            _currentStartNode = null;
            _currentWire = null;
            _matchesMade++;
        

            if (_matchesMade >= Mathf.Min(wireCount, AvailableColors.Count))
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

        private void Update()
        {
            if (_currentStartNode != null && _currentWire != null)
            {
                UpdateLineVisual(_currentStartNode.transform.position, Input.mousePosition);
            }
        }

        public void SetHoveredNode(WiringNode node)
        {
            _currentHoveredNode = node;
        }

        //i'm not gonna lie this was from a tutorial but it works so who cares, calculates stretchy lines from two points
        private void UpdateLineVisual(Vector3 startPos, Vector3 endPos)
        {
            //setting the position
            float distance = Vector3.Distance(startPos, endPos);
            _currentWire.sizeDelta = new Vector2(distance/1.5f, _lineThickness);
            
            //setting the angle
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentWire.rotation = Quaternion.Euler(0, 0, angle);
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
            FinishGame(true);
        }
    }
}
