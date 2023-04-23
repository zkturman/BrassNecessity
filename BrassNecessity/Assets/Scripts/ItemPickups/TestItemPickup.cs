using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemPickup
{
    private MonoBehaviour testItem;
    private MeshRenderer testMesh;
    private Collider collider;
    private GameObject[] itemsToHide;
    public TestItemPickup(MonoBehaviour testItem, GameObject[] itemsToHide)
    {
        this.testItem = testItem;
        testMesh = testItem.GetComponent<MeshRenderer>();
        collider = testItem.GetComponent<Collider>();
        this.itemsToHide = itemsToHide;
    }

    public void PickupItem(float disabledTime)
    {  
        testItem.StartCoroutine(disablePickup(disabledTime));
    }

    private IEnumerator disablePickup(float disabledTime)
    {
        setEnabledStatus(false);
        yield return new WaitForSeconds(disabledTime);
        setEnabledStatus(true);
    }

    private void setEnabledStatus(bool shouldEnable)
    {
        if (testMesh != null)
        {
            testMesh.enabled = shouldEnable;
        }
        if (collider != null)
        {
            collider.enabled = shouldEnable;
        }
        if (itemsToHide != null)
        {
            for (int i = 0; i < itemsToHide.Length; i++)
            {
                itemsToHide[i].SetActive(shouldEnable);
            }
        }
    }
}

