using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MenuButtonData
{
    [SerializeField]
    private string buttonName;
    public string ButtonName
    {
        get => buttonName;
    }
    [SerializeField]
    private MenuButtonType type;
    public MenuButtonType Type
    {
        get => type;
    }

    [SerializeField]
    private string sceneDestinationKey;
    public string SceneDestinationKey
    {
        get => sceneDestinationKey;
    }
}
