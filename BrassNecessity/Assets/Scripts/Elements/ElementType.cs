using System;

/// <summary>
/// To expand list, ElementMultiplierGrid must also be expanded.
/// </summary>
public enum ElementType
{
    None,
    Carbon,
    Electric,
    Gravity,
    Nuclear,
}

public static class ElementTypeHelper
{
    public static ElementType GenerateRandomType()
    {
        int numberOfElements = Enum.GetNames(typeof(ElementType)).Length;
        int diceRoll = UnityEngine.Random.Range(0, numberOfElements);
        return (ElementType)diceRoll;
    }
}
