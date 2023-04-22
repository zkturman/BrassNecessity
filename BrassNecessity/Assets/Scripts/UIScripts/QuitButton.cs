using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuitButton : GenericButton
{

    public QuitButton(MenuButtonData menuButtonData, Button button) : base(menuButtonData, button) { }

    public override void Execute()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
