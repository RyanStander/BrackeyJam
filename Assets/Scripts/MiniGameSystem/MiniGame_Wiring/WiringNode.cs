using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WiringNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [Header("State")]
    public int ColorID;
    public bool IsLeftSide;
    public bool IsConnected;

    private WiringMinigame _wiregame;
    private Image _wireNodeImage;

    private void Awake()
    {
        _wireNodeImage = GetComponent<Image>();
    }

    public void Setup(WiringMinigame wiregame, int id, Color color)
    {
        _wiregame = wiregame;
        ColorID = id;
        IsConnected = false;

        if (_wireNodeImage == null) _wireNodeImage = GetComponent<Image>();
        _wireNodeImage.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsConnected) _wiregame.AttemptConnectionStart(this);
        //Debug.Log("Clicked on node with color ID: " + ColorID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsConnected) _wiregame.AttemptConnectionEnd(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _wiregame.EndDrag();
        //Debug.Log("Released node with color ID: " + ColorID);
    }
}