using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ControllerAnimationManager
{
  
    [SerializeField]
    private string SpeedAnimationId = "Speed";

    [SerializeField]
    private string GroundAnimationId = "Grounded";

    [SerializeField]
    private string JumpAnimationId = "Jump";

    [SerializeField]
    private string FreeFallAnimationId = "FreeFall";
   
    [SerializeField]    
    private string MotionSpeedAnimationId = "MotionSpeed";
    

    public Animator Animator { get; set; }
    public bool HasAnimator
    {
        get => Animator != null;
    }

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    public ControllerAnimationManager()
    {
        assignAnimationIDs();
    }

    private void assignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash(SpeedAnimationId);
        _animIDGrounded = Animator.StringToHash(GroundAnimationId);
        _animIDJump = Animator.StringToHash(JumpAnimationId);
        _animIDFreeFall = Animator.StringToHash(FreeFallAnimationId);
        _animIDMotionSpeed = Animator.StringToHash(MotionSpeedAnimationId);
    }

    public void TrySetAnimationSpeed(float valueToSet)
    {
        setAnimationKeyValue(_animIDSpeed, valueToSet);
    }

    public void TrySetAnimationMotionSpeed(float valueToSet)
    {
        setAnimationKeyValue(_animIDMotionSpeed, valueToSet);
    }

    private void setAnimationKeyValue(int animationKey, float valueToSet)
    {
        if (HasAnimator)
        {
            this.Animator.SetFloat(animationKey, valueToSet);
        }
    }

    public void TrySetAnimationGrounded(bool isGrounded)
    {
        setAnimationKeyBool(_animIDGrounded, isGrounded);
    }

    public void TrySetAnimationFreeFall(bool isFreeFall)
    {
        setAnimationKeyBool(_animIDFreeFall, isFreeFall);
    }

    public void TrySetAnimationJump(bool isJump)
    {
        setAnimationKeyBool(_animIDJump, isJump);
    }

    private void setAnimationKeyBool(int animationKey, bool valueToSet)
    {
        if (HasAnimator)
        {
            this.Animator.SetBool(animationKey, valueToSet);
        }
    }
}
