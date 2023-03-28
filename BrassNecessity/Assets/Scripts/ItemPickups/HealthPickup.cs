using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IItemPickup
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

    public virtual void PickupItem()
    {
        throw new System.Exception("Needs to be implemented");
    }
}
