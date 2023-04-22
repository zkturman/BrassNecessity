using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using System.Linq;

[Serializable]
public abstract class GenericButton : IMenuButton
{
    protected MenuButtonData menuButtonData;
    protected Button button;

    public GenericButton(MenuButtonData menuButtonData, Button button)
    {
        this.menuButtonData = menuButtonData;
        this.button = button;
    }

    public abstract void Execute();

    public void ToggleSelect()
    {
        string[] test = button.GetClasses().ToArray();
        button.ToggleInClassList("menuButtonSelect");
        test = button.GetClasses().ToArray();
    }
}
