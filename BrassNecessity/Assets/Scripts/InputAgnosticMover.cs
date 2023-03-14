using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAgnosticMover : IPlayerMover
{
    private PlayerMover mover2D;
    private Player1DMover mover1D;
    private PlayerMover mover;

    public InputAgnosticMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData)
    {
        mover1D = new Player1DMover(moveData, jumpFallData);
        mover2D = new PlayerMover(moveData, jumpFallData);
    }

    public void AddAnimationManager(ControllerAnimationManager animationManager)
    {
        mover1D.AddAnimationManager(animationManager);
        mover2D.AddAnimationManager(animationManager);
    }

    public void MovePlayer(PlayerControllerInputs input)
    {
        determineMover();
        mover.MovePlayer(input);
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
}
