using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    public enum PipeType { Empty, Start, End, PipeI, PipeL, PipeT, PipeX }

    [Header("Level Editor")]
    public PipeType currentType;

    public Sprite emptySprite;
    public Sprite startSprite;
    public Sprite endSprite;
    public Sprite iSprite;
    public Sprite lSprite;
    public Sprite tSprite;
    public Sprite xSprite;

    public bool isStartNode;
    public bool isEndNode;

    [Header("Current Connections")]
    public bool up;
    public bool right;
    public bool down;
    public bool left;

    private Image pipeImage;

    private void OnValidate() //thanks heaven! this actually came in handy for testing in editor
    {
        if (pipeImage == null) pipeImage = GetComponent<Image>();
        if (pipeImage == null) return;

        isStartNode = false; isEndNode = false;
        up = false; right = false; down = false; left = false;

        transform.localEulerAngles = Vector3.zero;

        switch (currentType)
        {
            case PipeType.Empty:
                pipeImage.sprite = emptySprite;
                break;
            case PipeType.Start:
                isStartNode = true;
                down = true; 
                pipeImage.sprite = startSprite;
                break;
            case PipeType.End:
                isEndNode = true;
                down = true; 
                pipeImage.sprite = endSprite;
                break;
            case PipeType.PipeI:
                up = true; down = true;
                pipeImage.sprite = iSprite;
                break;
            case PipeType.PipeL:
                up = true; left = true;
                pipeImage.sprite = lSprite;
                break;
            case PipeType.PipeT:
                down = true; up = true; right = true;
                pipeImage.sprite = tSprite;
                break;
            case PipeType.PipeX:
                up = true; right = true; down = true; left = true;
                pipeImage.sprite = xSprite;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && currentType != PipeType.Empty)
        {
            RotatePipe();
        }
    }

    private void RotatePipe()
    {
        transform.Rotate(0, 0, -90f);

        bool temp = up;
        up = left;
        left = down;
        down = right;
        right = temp;

        FindObjectOfType<PipeGridManager>().CheckConnectivity();
    }
}