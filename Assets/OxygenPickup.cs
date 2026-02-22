using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenPickup : MonoBehaviour
{

    public UnityEvent OnPickupOxygen;

    public OxygenPickup(UnityEvent onPickupOxygen)
    {
        OnPickupOxygen = onPickupOxygen;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pikced up oxygen");
            OnPickupOxygen.Invoke();
            Destroy(this.gameObject);
        }
    }
}
