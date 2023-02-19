using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DMover : IPlayerMover
{
    private ICartesianMoveHandler walkHandler;
    private ICartesianMoveHandler runHandler;
    private IVerticalMoveHandler jumpFallHandler;
    private ControllerMoveData moveData;
    private ControllerJumpFallData jumpFallData;
    private ControllerAnimationManager animationManager;
    
    public Player3DMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData)
    {
        this.moveData = moveData;
        walkHandler = new PlayerRunWalkHandler(moveData);
        runHandler = new PlayerRunWalkHandler(moveData);
        ((PlayerRunWalkHandler)runHandler).Sprint = true;
        this.jumpFallData = jumpFallData;
        jumpFallHandler = new PlayerJumpFallHandler(jumpFallData);
    }

    public void AddAnimationManager(ControllerAnimationManager animationManager)
    {
        this.animationManager = animationManager;
        jumpFallHandler.EnableAnimations(animationManager);
        runHandler.EnableAnimations(animationManager);
        walkHandler.EnableAnimations(animationManager);
    }

    public void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler planarMoveHandler = determinePlanarMoveHandler(input.sprint);
        Vector3 planarMovement = planarMoveHandler.GenerateMove(input.move);
        Vector3 verticalMovement = jumpFallHandler.GenerateMove(input.jump);
        Vector3 totalMovement = planarMovement + verticalMovement;
        moveData.ControllerReference.Move(totalMovement);
        input.jump = false;
    }

    private ICartesianMoveHandler determinePlanarMoveHandler(bool isSprinting)
    {
        ICartesianMoveHandler moveHandler;
        if (isSprinting)
        {
            moveHandler = runHandler;
        }
        else
        {
            moveHandler = walkHandler;
        }
        return moveHandler;
    }
}
