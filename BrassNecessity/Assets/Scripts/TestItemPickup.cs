using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemPickup
{
    private MonoBehaviour testItem;
    private MeshRenderer testMesh;
    private Collider collider;
    public TestItemPickup(MonoBehaviour testItem)
    {
        this.testItem = testItem;
        testMesh = testItem.GetComponent<MeshRenderer>();
        collider = testItem.GetComponent<Collider>();
    }

    public void PickupItem(float disabledTime)
    {  
        testItem.StartCoroutine(disablePickup(disabledTime));
    }

    private IEnumerator disablePickup(float disabledTime)
    {
        testMesh.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(disabledTime);
        testMesh.enabled = true;
        collider.enabled = true;
    }
}

