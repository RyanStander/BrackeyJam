using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineAudioController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (skeletonAnimation == null)
            skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepAudio()
    {
        // This method can be called from Spine animation events to play footstep sounds
        // You can implement your audio playing logic here, for example:
        // AudioManager.Instance.Play("FootstepSound");
        AnimationState state;
        
    }
}
