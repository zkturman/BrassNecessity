using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMover: IPlayerMover
{
    private ICartesianMoveHandler walkHandler;
    private ICartesianMoveHandler runHandler;
    private ControllerMoveData moveData;
    private ControllerAnimationManager animManager;
    public Player2DMover(ControllerMoveData moveData)
    {
        walkHandler = new PlayerRunWalkHandler(moveData);
        runHandler = new PlayerRunWalkHandler(moveData);
        ((PlayerRunWalkHandler)runHandler).Sprint = true;
    }

    public void AddAnimationManager(ControllerAnimationManager animManager)
    {
        this.animManager = animManager;
        walkHandler.EnableAnimations(animManager);
        runHandler.EnableAnimations(animManager);
    }

    public void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler moveHandler = determineMoveHandler(input.sprint);
        Vector3 movement = moveHandler.GenerateMove(input.move);
        moveData.ControllerReference.Move(movement);
    }

    private ICartesianMoveHandler determineMoveHandler(bool isSprinting)
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
