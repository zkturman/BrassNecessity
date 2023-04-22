using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMenuNavigator
{
    public Vector2 GetMenuMovement();

    public bool ShouldExecute();
}
