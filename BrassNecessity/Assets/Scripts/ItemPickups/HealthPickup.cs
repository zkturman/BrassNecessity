using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : ItemPickup, IItemPickup
{
    [SerializeField]
    protected float healthValue;

    public float HealthValue
    {
        get => healthValue;
        private set => healthValue = value;
    }

    protected virtual void Awake()
    {

    }

    public override void PickupItem()
    {
        base.PickupItem();
    }
}
