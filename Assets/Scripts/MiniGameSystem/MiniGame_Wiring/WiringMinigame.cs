using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiringMinigame : BaseMinigame
{
    [Header("configuration")]
    public int wireCount = 6; 
    public List<Color> AvailableColors;

    [Header("references")]
    public GameObject WirePrefab;    
    public WiringNode NodePrefab;    
    public Transform WireParent;     
    public Transform LeftContainer;  
    public Transform RightContainer; 

    private WiringNode _currentStartNode;
    private RectTransform _currentWire;
    private int _matchesMade = 0;

    public override void StartMinigame()
    {
        base.StartMinigame();
        
        _matchesMade = 0;
        _currentStartNode = null;
        _currentWire = null;

        List<int> allColorIndices = new List<int>();
        for (int i = 0; i < AvailableColors.Count; i++) allColorIndices.Add(i);

        ShuffleList(allColorIndices);

        int count = Mathf.Min(wireCount, AvailableColors.Count);
        List<int> selectedColors = allColorIndices.GetRange(0, count);

        for (int i = 0; i < count; i++)
        {
            int id = selectedColors[i];
            WiringNode node = Instantiate(NodePrefab, LeftContainer);
            node.Setup(this, id, AvailableColors[id]);
            node.IsLeftSide = true;
        }

        ShuffleList(selectedColors);

        for (int i = 0; i < count; i++)
        {
            int id = selectedColors[i];
            WiringNode node = Instantiate(NodePrefab, RightContainer);
            node.Setup(this, id, AvailableColors[id]);
            node.IsLeftSide = false; //kind of hacky but it works
            node.transform.localScale = new Vector3(-1, 1, 1); 
        }
    }


    public void AttemptConnectionStart(WiringNode node)
    {
        if (_currentStartNode != null) return; 

        _currentStartNode = node;

        GameObject newLine = Instantiate(WirePrefab, WireParent);
        _currentWire = newLine.GetComponent<RectTransform>();
        _currentWire.position = node.transform.position;
        newLine.GetComponent<Image>().color = AvailableColors[node.ColorID];
        //Debug.Log(newLine.name + " created at " + _currentWire.position);
    }

    public void AttemptConnectionEnd(WiringNode endNode)
    {
        if (_currentStartNode == null) 
            return;
        if (_currentStartNode == endNode) 
            return;
        if (_currentStartNode.IsLeftSide == endNode.IsLeftSide) 
            return;

        if (_currentStartNode.ColorID == endNode.ColorID)
        {
            FinishConnection(endNode);
        }
    }

    private void FinishConnection(WiringNode endNode)
    {
        UpdateLineVisual(_currentStartNode.transform.position, endNode.transform.position);
        Debug.Log("Connection made between " + _currentStartNode.name + " and " + endNode.name);

        _currentStartNode.IsConnected = true;
        endNode.IsConnected = true;

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
            Destroy(_currentWire.gameObject);
            _currentStartNode = null;
            _currentWire = null;
        }
    }

    private void Update()
    {
        if (_currentStartNode != null && _currentWire != null)
        {
            UpdateLineVisual(_currentStartNode.transform.position, Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
            {  
                EndDrag();
            }  
        }
    }

    private void UpdateLineVisual(Vector3 startPos, Vector3 endPos)
    {
        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;
        _currentWire.sizeDelta = new Vector2(distance, 15f); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //thank god for tutorials
        _currentWire.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int rnd = Random.Range(i, list.Count);
            list[i] = list[rnd]; //thank god for tutorials x2
            list[rnd] = temp;
        }
    }
    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("delay done, closing minigame");
        FinishGame(true);
    }
}