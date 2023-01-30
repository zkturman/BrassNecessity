using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementMultiplierGrid
{
    /// <summary>
    /// Each row represents an ElementType and the damage multiplier
    /// for it attacking each type. Each row and column matches the index
    /// for the corresponding ElementType value. To add a new type, a new row and column
    /// must be added.
    /// </summary>
    private static float[,] elementMultipliers = new float[,]
    {
      {1f, 2f, 1f, 1f, .5f },
      {.5f, 1f, 2f, 1f, 2f },
      {1f, .5f, 1f, 2f, 2f },
      {1f, 2f, 1f, 1f, 1f},
      {2f, 1f, .5f, 2f, 1f}
    };

    public static float GetAttackMultiplier(ElementType attackingType, ElementType defendingType)
    {
        return elementMultipliers[(int)attackingType, (int)defendingType];
    }
}
