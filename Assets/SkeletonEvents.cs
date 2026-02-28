using AudioManagement;
using PersistentManager;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SkeletonAnimation anim = GetComponent<SkeletonAnimation>();

        anim.state.Event += OnEvent;
    }

    private void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        switch(e.Data.Name)
        {
            case "footstep":
                AudioManager.PlayOneShot(AudioDataHandler.Character.PlayerStep());
                break;
            case "stroke":
                AudioManager.PlayOneShot(AudioDataHandler.Character.PlayerSwimWeak());
                break;
            default:
                break;
        }
        
    }
}