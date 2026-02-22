using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    public Transform NpcHolderToDisable;
    public Transform NpcHolderToEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NpcHolderToDisable.gameObject.SetActive(false);
            NpcHolderToEnable.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
