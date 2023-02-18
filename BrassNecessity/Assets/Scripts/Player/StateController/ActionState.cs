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

    private StarterAssets.StarterAssetsInputs _input;
    private GameObject _mainCamera;
    private CharacterController _controller;
    private Player3DMover mover;
    private void Awake()
    {
    }
    private void Start()
    {
        _animData.Animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        mover = new Player3DMover(_moveData, _jumpData);
        mover.AddAnimationManager(_animData);
    }

    public IControllerState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public void LateStateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        mover.MovePlayer(_input);
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
