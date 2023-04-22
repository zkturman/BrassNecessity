using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private ControllerMoveData _moveData;
    [SerializeField]
    private ControllerJumpFallData _jumpData;
    [SerializeField]
    private ControllerAnimationManager _animData;
    private ElementApplyState applyState;

    private ElementComponent nextElement;

    private PlayerControllerInputs _input;
    private CharacterController _controller;
    private InputAgnosticMover mover;
    public IControllerState NextState { get; private set; }

    private void Start()
    {
        _animData.Animator = GetComponentInParent<Animator>();
        _controller = GetComponentInParent<CharacterController>();
        _input = GetComponentInParent<PlayerControllerInputs>();
        applyState = GetComponent<ElementApplyState>();
        mover = new InputAgnosticMover(_moveData, _jumpData);
        mover.AddAnimationManager(_animData);
        NextState = this;
    }

    public IControllerState GetNextState()
    {
        return this;
    }

    public void StateEnter()
    {
        NextState = this;
        _input.pause = false;
        _input.jump = false;
    }

    public void StateUpdate()
    {
        if (_input.pause)
        {
            NextState = GetComponent<PauseState>();
        }
        else if (_input.shoot)
        {
            NextState = GetComponent<AttackState>(); ;
        }
        else if (_input.applyElement)
        {
            NextState = applyState;
        }
        else
        {
            mover.MovePlayer(_input);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_jumpData.Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - _jumpData.GroundedOffset, transform.position.z),
            _jumpData.GroundedRadius);
    }
}
