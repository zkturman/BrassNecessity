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

    private Player1DMover mover1D;
    private PlayerMover mover2D;
    
    private PlayerMover mover;
    private ElementApplyState applyState;

    private FrameTimeoutHandler attackTimeoutHandler;

    private void Awake()
    {
        attackTimeoutHandler = new FrameTimeoutHandler(0f);
        input = GetComponent<PlayerControllerInputs>();
        animData.Animator = GetComponent<Animator>();
        mover1D = new Player1DMover(moveData, jumpFallData);
        mover2D = new PlayerMover(moveData, jumpFallData);
        mover1D.AddAnimationManager(animData);
        mover2D.AddAnimationManager(animData);
        determineMover();
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
            determineMover();
            mover.MovePlayer(input);
        }
    }

    private void determineMover()
    {
        if (CurrentDevice.IsCurrentDeviceKeyboard())
        {
            mover = mover1D;
        }
        else
        {
            mover = mover2D;
        }
    }

    private bool shouldApplyElement()
    {
        return input.applyElement && applyState.HasElements();
    }
}
