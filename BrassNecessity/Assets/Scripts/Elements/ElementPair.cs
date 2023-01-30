using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementPair
{ 
    public ElementType Primary { get; private set; }
    public ElementType Secondary { get; private set; }
    public ElementPair()
    {
        Primary = generateRandomType();
        Secondary = generateRandomType();
    }

    public ElementPair(ElementType primary)
    {
        Primary = primary;
        Secondary = ElementType.None;
    }

    public ElementPair(ElementType primary, ElementType secondary)
    {
        Primary = primary;
        Secondary = secondary;
    }

    public float GetAttackingMultiplier(ElementPair defendingType)
    {
        float primaryMultiplier = ElementMultiplierGrid.GetAttackMultiplier(Primary, defendingType.Primary);
        float secondaryMultiplier = ElementMultiplierGrid.GetAttackMultiplier(Secondary, defendingType.Secondary);
        return primaryMultiplier * secondaryMultiplier;
    }

    private ElementType generateRandomType()
    {
        int numberOfElements = Enum.GetNames(typeof(ElementType)).Length;
        int diceRoll = UnityEngine.Random.Range(0, numberOfElements);
        return (ElementType)diceRoll;
    }
}
