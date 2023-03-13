using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
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
    private Player3DMover mover;
    public IControllerState NextState { get; private set; }

    private void Start()
    {
        _animData.Animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<PlayerControllerInputs>();
        applyState = GetComponent<ElementApplyState>();
        mover = new Player3DMover(_moveData, _jumpData);
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
            detectCollision();
        }
    }

    private void detectCollision()
    {
        RaycastHit collision;
        Vector3 boxCenter = new Vector3(transform.position.x, transform.position.y + _controller.height / 2, transform.position.z);
        Vector3 extents = new Vector3(_controller.radius / 2, _controller.height / 4, _controller.radius / 2);
        if (Physics.BoxCast(boxCenter, extents, transform.forward, out collision, Quaternion.identity, .25f))
        {
            ElementPickup pickup;
            if (collision.collider.TryGetComponent<ElementPickup>(out pickup))
            {
                ElementComponent element = collision.collider.GetComponent<ElementComponent>();
                applyState.AddElement(element);
                pickup.PickupItem();
            }
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
