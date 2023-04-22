using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[Serializable]
public class SceneButton : GenericButton
{
    public SceneButton(MenuButtonData menuButtonData, Button button) : base(menuButtonData, button) { }

    public override void Execute()
    {
        SceneNavigator.OpenScene(menuButtonData.SceneDestination);
    }
}
