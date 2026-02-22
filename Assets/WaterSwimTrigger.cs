using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSwimTrigger : MonoBehaviour
{
    private SpineChracterController _controller;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _controller = collision.gameObject.GetComponent<SpineChracterController>();
            _controller.IsWaterLevel = true;
            Debug.Log("Player entered water.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_controller.IsWaterLevel = false;
            Debug.Log("Player exited water.");
        }
    }
}
