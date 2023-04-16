using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public float hitDetectDistance = 1f;
    public Vector3 hitDetectBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
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
        if (Physics.BoxCast(transform.position, hitDetectBoxSize, transform.forward, out hit, transform.rotation, hitDetectDistance))
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
        Debug.DrawRay(transform.position, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.right * hitDetectBoxSize.x, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position - transform.right * hitDetectBoxSize.x, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.up * hitDetectBoxSize.y, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position - transform.up * hitDetectBoxSize.y, transform.forward * hitDetectDistance, debugColor);
        Debug.DrawRay(transform.position + transform.forward * hitDetectBoxSize.z, transform.right * hitDetectBoxSize.x * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * hitDetectBoxSize.z, transform.right * hitDetectBoxSize.x * 2, debugColor);
        Debug.DrawRay(transform.position + transform.forward * hitDetectBoxSize.z, transform.up * hitDetectBoxSize.y * 2, debugColor);
        Debug.DrawRay(transform.position - transform.forward * hitDetectBoxSize.z, transform.up * hitDetectBoxSize.y * 2, debugColor);

    }



}
