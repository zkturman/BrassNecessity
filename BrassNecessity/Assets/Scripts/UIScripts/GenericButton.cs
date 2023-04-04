using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

[Serializable]
public class GenericButton : IMenuButton
{
    protected MenuButtonData menuButtonData;
    protected Button button;

    public GenericButton(MenuButtonData menuButtonData, Button button)
    {
        this.menuButtonData = menuButtonData;
        this.button = button;
    }

    public virtual void Execute()
    {
        Debug.Log(menuButtonData.ButtonName + " was selected.");
    }

    public void ToggleSelect()
    {
        button.ToggleInClassList("menuButtonSelect");
    }
}
