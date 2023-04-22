using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2DNavigator : MenuNavigatorComponent, IMenuNavigator
{
    public Vector2 GetMenuMovement()
    {
        return new Vector2(controlHandler.GetHorizontalMovement(), controlHandler.GetVerticalMovement());
    }

    public bool ShouldExecute()
    {
        return controlHandler.GetNormalShootSelect() || controlHandler.GetStrafeShootSelect();
    }
}
