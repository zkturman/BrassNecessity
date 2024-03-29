using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementPair
{
    [SerializeReference]
    private Element.Type _primary;
    public Element.Type Primary 
    { 
        get => _primary; 
        private set => _primary = value; 
    }

    [SerializeReference]
    private Element.Type _secondary;
    public Element.Type Secondary 
    { 
        get => _secondary; 
        private set => _secondary = value; 
    }

    public ElementPair()
    {
        Primary = Element.GenerateRandomType();
        Secondary = Element.Type.None;
    }

    public ElementPair(Element.Type primary)
    {
        Primary = primary;
        Secondary = Element.Type.None;
    }

    public ElementPair(Element.Type primary, Element.Type secondary)
    {
        Primary = primary;
        Secondary = secondary;
    }

    public float GetAttackingMultiplier(ElementPair defendingType)
    {
        float finalMultiplier = ElementMultiplierGrid.GetAttackMultiplier(Primary, defendingType.Primary);
        if (Secondary != Element.Type.None)
        {
            finalMultiplier *= ElementMultiplierGrid.GetAttackMultiplier(Secondary, defendingType.Secondary);
        }
        return finalMultiplier;
    }
}
