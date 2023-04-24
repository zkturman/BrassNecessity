using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IItemPickup
{
    public virtual void PickupItem()
    {
        Destroy(gameObject);
    }
}
