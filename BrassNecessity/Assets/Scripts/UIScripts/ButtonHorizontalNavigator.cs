using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHorizontalNavigator : MenuNavigatorComponent, IMenuNavigator
{
    public Vector2 GetMenuMovement()
    {
        return new Vector2(controlHandler.GetHorizontalMovement(), 0);
    }

    public bool ShouldExecute ()
    {
        return controlHandler.GetStrafeShootSelect();
    }
}
