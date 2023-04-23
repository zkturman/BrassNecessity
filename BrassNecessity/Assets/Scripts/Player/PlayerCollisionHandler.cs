using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private CharacterController controller;
    private ElementApplyState applyState;
    private PlayerHealthHandler healthHandler;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        applyState = GetComponentInChildren<ElementApplyState>();
        healthHandler = GetComponent<PlayerHealthHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectPickupCollision();
    }

    private void detectPickupCollision()
    {
        RaycastHit collision;
        Vector3 boxCenter = new Vector3(transform.position.x, transform.position.y + controller.height / 2, transform.position.z);
        Vector3 extents = new Vector3(controller.radius / 2, controller.height / 4, controller.radius / 2);
        if (Physics.BoxCast(boxCenter, extents, transform.forward, out collision, Quaternion.identity, .25f))
        {
            ElementPickup elementItem;
            if (collision.collider.TryGetComponent<ElementPickup>(out elementItem))
            {
                ElementComponent element = elementItem.Element;
                applyState.AddElement(element);
                elementItem.PickupItem();
            }
            HealthPickup healthItem;
            if (collision.collider.TryGetComponent<HealthPickup>(out healthItem))
            {
                if (!healthHandler.AtMaxHealth())
                {
                    healthHandler.HealPlayer(healthItem.HealthValue);
                    healthItem.PickupItem();
                }
            }
        }
    }
}
