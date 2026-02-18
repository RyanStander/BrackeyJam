using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// / <summary>
// / each node is a connection point for a wire. 
// / we begin by setting nodes to grey, then begin couroutines on each one to randomly flicker their color
// / when you click a node, it should stop flickering and show its real color, then spawn a wire that follows the mouse
// / </summary>
public class WiringNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [Header("State")]
    public int ColorID;
    public bool IsLeftSide;
    public bool IsConnected;

    private WiringMinigame _wiregame;
    public Image PupilImage;
    //private Image PupilImage;

    public float FlickerInterval = 3.0f;
    public float FlickerDuration = 0.5f;

    private Color _realColor;
    private Coroutine _flickerCoroutine;

    public void Setup(WiringMinigame wiregame, int id, Color color)
    {
        _wiregame = wiregame;
        ColorID = id;
        _realColor = color;
        IsConnected = false;

        if (PupilImage == null) 
            PupilImage = GetComponent<Image>();

        PupilImage.color = Color.white;

        if (_flickerCoroutine != null) 
            StopCoroutine(_flickerCoroutine);

        _flickerCoroutine = StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));

        while (!IsConnected)
        {
            PupilImage.color = _realColor;
            yield return new WaitForSeconds(FlickerDuration);

            if (IsConnected) break;

            PupilImage.color = Color.white;
            yield return new WaitForSeconds(FlickerInterval + Random.Range(0f, 3f));
        }
        PupilImage.color = _realColor;
    }

    //a bit hacky but I wanted to make sure the node color is set right once you connect it
    public void SetConnected()
    {
        IsConnected = true;
        if (_flickerCoroutine != null) StopCoroutine(_flickerCoroutine);
        PupilImage.color = _realColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsConnected) _wiregame.SetHoveredNode(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _wiregame.SetHoveredNode(null);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsConnected)
        {
            PupilImage.color = Color.white; //wanted it to immediately turn to white when released to stop cheating
        }
        _wiregame.EndDrag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsConnected)
        {
            _wiregame.AttemptConnectionStart(this);
            PupilImage.color = _realColor;
        }
    }
}