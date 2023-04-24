using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickup : ItemPickup
{
    [SerializeField]
    private ElementComponent element;

    public ElementComponent Element
    {
        get => element;
        private set => element = value;
    }

    protected virtual void Awake()
    {
        if (element == null)
        {
            this.element = GetComponent<ElementComponent>();
        }
    }

    public override void PickupItem()
    {
        base.PickupItem();
    }
}
