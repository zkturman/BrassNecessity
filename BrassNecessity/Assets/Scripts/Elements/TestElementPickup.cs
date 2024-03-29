using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestElementPickup : ElementPickup
{
    [SerializeField]
    private float respawnDelayInSeconds = 3f;

    [SerializeField]
    private GameObject[] itemsToHide = new GameObject[0];

    private TestItemPickup testPickup;

    protected override void Awake()
    {
        base.Awake();
        testPickup = new TestItemPickup(this, itemsToHide);   
    }

    protected override void Update() { }

    public override void PickupItem()
    {
        testPickup.PickupItem(respawnDelayInSeconds);
    }
}
