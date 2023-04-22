using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MenuRow
{
    [SerializeField]
    private UIElementData[] rowData;

    public UIElementData this[int i] => rowData[i];
    public int Length { get => rowData.Length; }
}
