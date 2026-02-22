using AudioManagement;
using PersistentManager;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxScript : MonoBehaviour
{
    [SerializeField] public SkeletonAnimation skeleton;
    // Start is called before the first frame update

    private void Awake()
    {
        this.GetComponentInChildren<SkeletonAnimation>().timeScale = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponentInChildren<SkeletonAnimation>().timeScale = 1;
            AudioManager.Play(AudioDataHandler.StationUnderwater.PrawnsScatter());
            StartCoroutine(DestroyPrawns());
        }
  
    }

    IEnumerator DestroyPrawns()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        //yield return null;
    }
}
