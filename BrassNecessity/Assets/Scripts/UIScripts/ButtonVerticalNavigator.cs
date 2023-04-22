using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVerticalNavigator : MenuNavigatorComponent, IMenuNavigator
{
    public Vector2 GetMenuMovement()
    {

        return new Vector2(0, controlHandler.GetVerticalMovement());
    }

    public bool ShouldExecute()
    {
        return controlHandler.GetNormalShootSelect();
    }
}
