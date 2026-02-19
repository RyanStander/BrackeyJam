using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterRaise : MonoBehaviour
{
    public Vector3 WaterRaiseAmountLocalScale;
    public Vector3 WaterRaiseAmountPosition;
    public Vector3 CurrentWaterRaisePoint;

    public Transform WaterObject;

    public float RaiseDuration = 2f;
    private bool _isRaising;

    private void Awake()
    {
        RaiseWater();
    }
    public void RaiseWater()
    {
        if (WaterObject == null)
        {
            Debug.LogWarning(nameof(WaterRaise) + ": WaterObject is not assigned.");

            return;
        }

        if (_isRaising)
            return;

        StartCoroutine(RaiseCoroutine());
        CurrentWaterRaisePoint = this.transform.position;
    }

    private IEnumerator RaiseCoroutine()
    {
        _isRaising = true;

        var startScale = WaterObject.localScale;
        var targetScale = startScale + WaterRaiseAmountLocalScale;

        Vector3 startPos = WaterObject.position;
        Vector3 targetPos = startPos + WaterRaiseAmountPosition;

        var elapsed = 0f;

        while (elapsed < RaiseDuration)
        {
            var t = elapsed / RaiseDuration;
            var smoothT = Mathf.SmoothStep(0f, 1f, t);

            WaterObject.localScale = Vector3.Lerp(startScale, targetScale, smoothT);

            WaterObject.localPosition = Vector3.Lerp(startPos, targetPos, smoothT);

            elapsed += Time.deltaTime;
            yield return null;
        }

        WaterObject.localScale = targetScale;
        WaterObject.localPosition = targetPos;

        _isRaising = false;
    }
}
