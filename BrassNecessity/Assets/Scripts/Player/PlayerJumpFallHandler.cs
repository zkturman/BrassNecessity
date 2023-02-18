using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpFallHandler: IVerticalMoveHandler
{
    private ControllerJumpFallData jumpFallData;
    private ControllerAnimationManager animationManager;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private bool shouldJump;
    private JumpStates jumpingState;
    private bool hasAnimationManager = false;

    public PlayerJumpFallHandler(ControllerJumpFallData jumpFallData)
    {
        this.jumpFallData = jumpFallData;
        resetJumpTimeout();
        resetFallTimeout();
        shouldJump = false;
        jumpingState = JumpStates.Grounded;
    }

    public void EnableAnimations(ControllerAnimationManager animManager)
    {
        if (animManager != null)
        {
            animationManager = animManager;
            hasAnimationManager = true;
        }
    }

    public Vector3 GenerateMove(bool shouldJump)
    {
        this.shouldJump = shouldJump;
        JumpStates previousJumpingState = jumpingState;
        Vector3 movement = JumpAndGravity();
        if (jumpingState != previousJumpingState)
        {
            if (hasAnimationManager)
            {
                setJumpingAnimation();
            }
        }
        return movement;
    }

    private Vector3 JumpAndGravity()
    {
        if (isOnGround())
        {
            handleGroundMechanics();
        }
        else
        {
            handleInAirMechanics();

        }
        applyGravity();
        return generateFinalVerticalMovement();
    }

    private void handleGroundMechanics()
    {
        resetFallTimeout();
        // stop our velocity dropping infinitely when grounded
        if (_verticalVelocity < 0.0f)
        {
            _verticalVelocity = -2f;
        }
        if (shouldJump)
        {
            tryToJump();
        }
        else
        {
            jumpingState = JumpStates.Grounded;
        }
        updateJumpTimeout();
    }

    private void resetFallTimeout()
    {
        _fallTimeoutDelta = jumpFallData.FallTimeout;
    }

    private void tryToJump()
    {
        if (_jumpTimeoutDelta <= 0.0f)
        {
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            _verticalVelocity = Mathf.Sqrt(jumpFallData.JumpHeight * -2f * jumpFallData.Gravity);
            jumpingState = JumpStates.Jumping;
        }
        else
        {
            jumpingState = JumpStates.Grounded;
        }
    }

    private void updateJumpTimeout()
    {
        if (_jumpTimeoutDelta >= 0.0f)
        {
            _jumpTimeoutDelta -= Time.deltaTime;
        }
    }

    private void handleInAirMechanics()
    {
        resetJumpTimeout();
        handleFalling();
        // if we are not grounded, do not jump
        shouldJump = false;
    }

    private void resetJumpTimeout()
    {
        _jumpTimeoutDelta = jumpFallData.JumpTimeout;
    }

    private void handleFalling()
    {
        // fall timeout
        if (_fallTimeoutDelta >= 0.0f)
        {
            _fallTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            jumpingState = JumpStates.Falling;
        }
    }

    private void applyGravity()
    {
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += jumpFallData.Gravity * Time.deltaTime;
        }
    }

    private Vector3 generateFinalVerticalMovement()
    {
        return new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
    }

    private bool isOnGround()
    {
        Transform transform = jumpFallData.TransformReference;
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - jumpFallData.GroundedOffset,
            transform.position.z);

        bool isOnGround = Physics.CheckSphere(spherePosition, jumpFallData.GroundedRadius, jumpFallData.GroundLayers,
            QueryTriggerInteraction.Ignore);
        return isOnGround;
    }

    private void setJumpingAnimation()
    {
        switch (jumpingState)
        {
            case JumpStates.Grounded:
                animationManager.TrySetAnimationGrounded(true);
                animationManager.TrySetAnimationFreeFall(false);
                animationManager.TrySetAnimationJump(false);
                break;
            case JumpStates.Jumping:
                animationManager.TrySetAnimationJump(true);
                animationManager.TrySetAnimationFreeFall(false);
                animationManager.TrySetAnimationGrounded(false);
                break;
            case JumpStates.Falling:
                animationManager.TrySetAnimationFreeFall(true);
                animationManager.TrySetAnimationGrounded(false);
                animationManager.TrySetAnimationJump(false);
                break;
            default:
                throw new System.Exception("Invalid jumping state used in jumping handler.");
        }
    }
}
