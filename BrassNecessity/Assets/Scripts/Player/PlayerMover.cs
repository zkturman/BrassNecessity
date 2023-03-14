using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : IPlayerMover
{
    protected ICartesianMoveHandler walkHandler;
    protected ICartesianMoveHandler runHandler;
    protected ControllerMoveData moveData;
    protected IVerticalMoveHandler jumpFallHandler;
    private ControllerJumpFallData jumpFallData;
    protected ControllerAnimationManager animManager;
    
    protected PlayerMover(ControllerMoveData moveData)
    {
        this.moveData = moveData;
        walkHandler = new PlayerRunWalkHandler(moveData);
        runHandler = new PlayerRunWalkHandler(moveData);
        ((PlayerRunWalkHandler)runHandler).Sprint = true;
    }
    
    public PlayerMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData) : this(moveData)
    {
        jumpFallHandler = new PlayerJumpFallHandler(jumpFallData);
    }

    public virtual void AddAnimationManager(ControllerAnimationManager animManager)
    {
        this.animManager = animManager;
        walkHandler.EnableAnimations(animManager);
        runHandler.EnableAnimations(animManager);
        jumpFallHandler.EnableAnimations(animManager);
    }

    public virtual void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler planarMoveHandler = determineMoveHandler(input.sprint);
        Vector3 planarMovement = planarMoveHandler.GenerateMove(input.move);
        Vector3 verticalMovement = jumpFallHandler.GenerateMove(false);
        Vector3 totalMovement = planarMovement + verticalMovement;
        moveData.ControllerReference.Move(totalMovement);
    }

    protected ICartesianMoveHandler determineMoveHandler(bool isSprinting)
    {
        ICartesianMoveHandler handler;
        if (isSprinting)
        {
            handler = runHandler;
        }
        else
        {
            handler = walkHandler;
        }
        return handler;
    }
}
