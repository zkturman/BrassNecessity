using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData : MonoBehaviour
{
    [SerializeField]
    private Color[] batteryLightColor;

    [SerializeField]
    private Material[] batteryColor;

    public Color GetLight(ElementPair info)
    {
        int typeId = Element.TypeToInt(info.Primary);
        Color elementColor = Color.white;
        if (typeId < batteryLightColor.Length)
        {
            elementColor = batteryLightColor[typeId];
        }
        return elementColor;
    }

    public Material GetBatteryMaterial(ElementPair info)
    {
        int typeId = Element.TypeToInt(info.Primary);
        Material elementMaterial = null;
        if (typeId < batteryColor.Length)
        {
            elementMaterial = batteryColor[typeId];
        }
        return elementMaterial;
    }

    public ElementComponent TryGetElementPair()
    {
        ElementComponent weaponElement = GetComponent<ElementComponent>();
        if (weaponElement == null)
        {
            Debug.Break();
        }
        return weaponElement;
    }
}
