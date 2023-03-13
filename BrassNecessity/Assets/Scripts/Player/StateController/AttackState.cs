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

    private PlayerMover mover;

    private FrameTimeoutHandler attackTimeoutHandler;

    private void Awake()
    {
        attackTimeoutHandler = new FrameTimeoutHandler(0f);
        input = GetComponent<PlayerControllerInputs>();
        animData.Animator = GetComponent<Animator>();
        if (input.analogMovement)
        {
            mover = new PlayerMover(moveData, jumpFallData);
        }
        else
        {
            mover = new Player1DMover(moveData, jumpFallData);
        }
        mover.AddAnimationManager(animData);
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
        if (!input.shoot){
            NextState = GetComponent<ActionState>();
            laserGun.ReleaseLaser();
        }
        else
        {
            laserGun.FireLaser();
            mover.MovePlayer(input);

        }
    }
}
