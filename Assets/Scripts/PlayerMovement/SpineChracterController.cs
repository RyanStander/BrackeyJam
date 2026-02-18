using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class controls the animation state of the Spine2D character based on 
/// the playermovement script. I know I probably shouldn't have done a direct reference
/// but I wanted to keep it simple and not have to worry about events or delegates for now
/// </summary>

public class SpineChracterController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public SkeletonAnimation skeletonAnimation;
    public string IdleAnim = "Player_Idle";
    public string WalkAnim = "Player_Walk";
    public string RunAnim = "Player_Run";

    private string _currentAnimationState;
    // Start is called before the first frame update
    void Start()
    {
        if (playerMovement == null) 
            playerMovement = GetComponent<PlayerMovement>();
        if (skeletonAnimation == null) 
            skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = playerMovement.CurrentMovement.x;
        if (xInput > 0)
        {
            skeletonAnimation.skeleton.ScaleX = 1;
        }
        else if (xInput < 0)
        {
            skeletonAnimation.skeleton.ScaleX = -1;
        }

        if(playerMovement.CurrentMovement.magnitude == 0)
        {
            ChangeAnimationState(IdleAnim);
        }
        else if (playerMovement.IsSprinting)
        {
            ChangeAnimationState(RunAnim);
        }
        else
        {
            ChangeAnimationState(WalkAnim);
        }
    }

    private void ChangeAnimationState(string idleAnim)
    {
        if (_currentAnimationState == idleAnim) return;
        skeletonAnimation.AnimationName = idleAnim;
        _currentAnimationState = idleAnim;
    }
}
