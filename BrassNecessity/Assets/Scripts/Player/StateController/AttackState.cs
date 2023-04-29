using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IControllerState
{
    public IControllerState NextState { get; private set; }
    [SerializeField]
    private float shootTimeout = 0.5f;

    [SerializeField]
    private WeaponBehaviour laserGun;
    [SerializeField]
    private PlayerControllerInputs input;
    [SerializeField]
    private ControllerAnimationManager animData;
    [SerializeField]
    private ControllerMoveData moveData;
    [SerializeField]
    private ControllerJumpFallData jumpFallData;
    private InputAgnosticMover mover;
    private ElementApplyState applyState;

    private FrameTimeoutHandler attackTimeoutHandler;

    [SerializeField]
    private SoundEffectTrackHandler soundEffects;

    private void Awake()
    {
        attackTimeoutHandler = new FrameTimeoutHandler(0f);
        input = GetComponentInParent<PlayerControllerInputs>();
        animData.Animator = GetComponentInParent<Animator>();
        mover = new InputAgnosticMover(moveData, jumpFallData);
        mover.AddAnimationManager(animData);
        applyState = GetComponent<ElementApplyState>();
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateEnter()
    {
        NextState = this;
        float timeout = determineTimeout();
        attackTimeoutHandler.ResetTimeout(timeout);
    }

    private float determineTimeout()
    {
        return shootTimeout;
    }

    public void StateUpdate()
    {
        if (shouldApplyElement())
        {
            NextState = applyState;
            laserGun.ReleaseLaser();
        }
        else if (!input.shoot)
        {
            NextState = GetComponent<ActionState>();
            laserGun.ReleaseLaser();
        }
        else
        {
            laserGun.FireLaser();
            mover.MovePlayer(input);
        }
        if (laserGun.IsElementBroken)
        {
            NextState = GetComponent<ElementBrokenState>();
            laserGun.ReleaseLaser();
        }
    }

    private bool shouldApplyElement()
    {
        return input.applyElement && applyState.HasElements();
    }
}
