using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrash : MonoBehaviour
{
    public Transform thisTrashTransform;
    public float RandomXOffsetRangeMin = -2.5f;
    public float RandomXOffsetRangeMax = 2.5f;
    public float RandomYOffsetRangeMin = -2.5f;
    public float RandomYOffsetRangeMax = 2.5f;

    private void Awake()
    {
        thisTrashTransform = this.transform;
    }

    void Start()
    {
        StartRandomRotated();
        StartRandomPositionOffset();
    }

    private void StartRandomRotated()
    {
        thisTrashTransform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }

    private void StartRandomPositionOffset()
    {
        Vector3 currentPosition = thisTrashTransform.position;

        float randomXOffset = Random.Range(RandomXOffsetRangeMin, RandomXOffsetRangeMax);
        float randomYOffset = Random.Range(RandomYOffsetRangeMin, RandomYOffsetRangeMax);

        thisTrashTransform.position = new Vector3(
            currentPosition.x + randomXOffset,
            currentPosition.y + randomYOffset,
            currentPosition.z
        );
    }
}