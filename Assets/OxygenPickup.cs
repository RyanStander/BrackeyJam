using AudioManagement;
using PersistentManager;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.PlayOneShot(AudioDataHandler.StationUnderwater.OxygenPickup());
            Destroy(this.gameObject);
        }
    }
}
