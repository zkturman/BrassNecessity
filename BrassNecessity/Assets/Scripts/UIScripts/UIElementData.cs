using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UIElementData
{
    [SerializeField]
    protected string elementName;
    public string ElementName
    {
        get => elementName;
    }
}
