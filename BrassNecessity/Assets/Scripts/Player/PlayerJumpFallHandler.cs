using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpFallHandler: IVerticalMoveHandler
{
    private ControllerJumpFallData jumpFallData;
    private ControllerAnimationManager animationManager;
    private float groundedVerticalVelocity = -2f;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private float fallTimeoutDelta;
    private bool shouldJump;
    private JumpStates jumpingState;
    private bool hasAnimationManager = false;

    public PlayerJumpFallHandler(ControllerJumpFallData jumpFallData)
    {
        this.jumpFallData = jumpFallData;
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
        processCurrentState();
        applyGravity();
        return generateFinalVerticalMovement();
    }

    private void processCurrentState()
    {
        switch (jumpingState)
        {
            case JumpStates.Grounded:
                handleGroundState();
                break;
            case JumpStates.Jumping:
                handleJumpingState();
                break;
            case JumpStates.Falling:
                handleFallingState();
                break;
            default:
                throw new System.Exception("A non-standard jumping state was encountered.");
        }
    }

    private void handleGroundState()
    {
        if (!isOnGround())
        {
            jumpingState = JumpStates.Falling;
        }
        if (shouldJump)
        {
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            verticalVelocity = Mathf.Sqrt(jumpFallData.JumpHeight * -2f * jumpFallData.Gravity);
            jumpingState = JumpStates.Jumping;
        }
    }

    private void handleJumpingState()
    {
        // fall timeout
        if (fallTimeoutDelta <= 0.0f)
        {
            resetFallTimeout();
            jumpingState = JumpStates.Falling;
        }
        else
        {
            fallTimeoutDelta -= Time.deltaTime;
        }
    }

    private void handleFallingState()
    {
        if (isOnGround())
        {
            verticalVelocity = groundedVerticalVelocity;
            jumpingState = JumpStates.Grounded;
        }
    }

    private void resetFallTimeout()
    {
        fallTimeoutDelta = jumpFallData.FallTimeout;
    }

    private void applyGravity()
    {
        if (verticalVelocity < terminalVelocity)
        {
            verticalVelocity += jumpFallData.Gravity * Time.deltaTime;
        }
    }

    private Vector3 generateFinalVerticalMovement()
    {
        return new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;
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
