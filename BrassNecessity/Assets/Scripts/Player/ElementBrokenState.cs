using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBrokenState : MonoBehaviour, IControllerState
{
    public IControllerState NextState { get; private set; }

    [SerializeField]
    private PlayerControllerInputs input;
    [SerializeField]
    private ControllerMoveData moveData;
    [SerializeField]
    private ControllerAnimationManager animData;
    [SerializeField]
    private ControllerJumpFallData jumpFallData;
    private InputAgnosticMover mover;
    
    [SerializeField]
    private float recoveryTimeoutInSeconds = 2f;
    private FrameTimeoutHandler recoveryTimeoutHandler;

    [SerializeField]
    private SoundEffectTrackHandler soundEffects;


    private void Awake()
    {
        recoveryTimeoutHandler = new FrameTimeoutHandler(recoveryTimeoutInSeconds);
        recoveryTimeoutHandler.UpdateTimePassed(recoveryTimeoutInSeconds);
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
        mover = new InputAgnosticMover(moveData, jumpFallData);
        animData.Animator = GetComponentInParent<Animator>();
        mover.AddAnimationManager(animData);
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateEnter()
    {
        soundEffects.PlayOnce(SoundEffectKey.ElementBreaking);
        recoveryTimeoutHandler.ResetTimeout();
        NextState = this;
    }

    public void StateUpdate()
    {
        recoveryTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (recoveryTimeoutHandler.HasTimeoutEnded())
        {
            NextState = GetComponent<ElementApplyState>();
        }
        else
        {
            mover.MovePlayer(input);
        }
    }

    public float RecoveryPercentRemaining()
    {
        return recoveryTimeoutHandler.TimeRemaining() / recoveryTimeoutInSeconds;
    }
}
