using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IItemPickup
{
    public virtual void PickupItem()
    {
        throw new System.Exception("Needs to be implemented in non-test scenarios.");
    }
}
