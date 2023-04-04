using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigationControls
{
    private PlayerControllerInputs input;

    public MenuNavigationControls(PlayerControllerInputs input)
    {
        this.input = input;
    }

    public int GetVerticalMovement()
    {
        float verticalMovement = input.move.y;
        return getDirectionValue(verticalMovement);
    }

    public int GetHorizontalMovement()
    {
        float horizontalMovement = input.move.x;
        return getDirectionValue(horizontalMovement);
    }

    private int getDirectionValue(float movementValue)
    {
        int directionValue = 0;
        if (!Mathf.Approximately(movementValue, 0f))
        {
            directionValue = (int)Mathf.Sign(movementValue);
        }
        return directionValue;
    }
}
