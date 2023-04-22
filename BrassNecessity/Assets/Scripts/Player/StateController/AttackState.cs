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

    private void Awake()
    {
        attackTimeoutHandler = new FrameTimeoutHandler(0f);
        input = GetComponentInParent<PlayerControllerInputs>();
        animData.Animator = GetComponentInParent<Animator>();
        mover = new InputAgnosticMover(moveData, jumpFallData);
        mover.AddAnimationManager(animData);
        applyState = GetComponent<ElementApplyState>();
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
            NextState = GetComponent<ElementApplyState>();
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
    }

    private bool shouldApplyElement()
    {
        return input.applyElement && applyState.HasElements();
    }
}
