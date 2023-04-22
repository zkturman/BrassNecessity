using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SliderData : UIElementData
{
    [SerializeField]
    private SliderType type;
    public SliderType Type
    {
        get => type;
    }
}
