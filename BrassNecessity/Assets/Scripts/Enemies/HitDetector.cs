using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public float boxcastDistance = 1f;
    public Vector3 boxcastSize = new Vector3(0.5f, 0.5f, 0.5f);
    public LayerMask layerMask;
    public Color debugColor = Color.red;
    PlayerHealthHandler playerHealthHandler;

    private void Awake()
    {
        //boxCollider = GetComponent<BoxCollider>();
        playerHealthHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthHandler>();
    }


    public void DetectHit(float damage, EnemyController context)
    {
        // Cast a box-shaped cast from the character's position forward
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, boxcastSize, transform.forward, out hit, transform.rotation, boxcastDistance))
        {
            // Check if the hit object matches the object we're testing for
            if (hit.collider.transform == context.playerTransform)
            {
                // The object is in the defined space
                
                playerHealthHandler.DamagePlayer(damage);
                Debug.Log("Player hit!  Player health = " + playerHealthHandler.Health);
            }
        }


        // Visualize the BoxCast in the Scene view
        Debug.DrawRay(transform.position, transform.forward * boxcastDistance, debugColor);
        Debug.DrawRay(transform.position + transform.right * boxcastSize.x, transform.forward * boxcastDistance, debugColor);
        Debug.DrawRay(transform.position - transform.right * boxcastSize.x, transform.forward * boxcastDistance, debugColor);
        Debug.DrawRay(transform.position + transform.up * boxcastSize.y, transform.forward * boxcastDistance, debugColor);
        Debug.DrawRay(transform.position - transform.up * boxcastSize.y, transform.forward * boxcastDistance, debugColor);
        Debug.DrawRay(transform.position + transform.forward * boxcastSize.z, transform.right * boxcastSize.x * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * boxcastSize.z, transform.right * boxcastSize.x * 2, debugColor);
        Debug.DrawRay(transform.position + transform.forward * boxcastSize.z, transform.up * boxcastSize.y * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * boxcastSize.z, transform.up * boxcastSize.y * 2, debugColor);

    }



}
