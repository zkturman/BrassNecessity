using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickup : MonoBehaviour
{
    [SerializeField]
    private float disableTime = 3f;

    public void PickupItem()
    {
        StartCoroutine(disablePickup());
    }

    private IEnumerator disablePickup()
    {
        MeshRenderer item = GetComponent<MeshRenderer>();
        BoxCollider collider = GetComponent<BoxCollider>();
        item.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(disableTime);
        item.enabled = true;
        collider.enabled = true;
    }
}

