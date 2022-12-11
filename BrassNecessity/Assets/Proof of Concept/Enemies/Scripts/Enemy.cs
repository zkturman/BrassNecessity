using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public Vector3 target;
    Rigidbody rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        target = new Vector3(0f, 0f, 0f);
    }


    private void FixedUpdate()
    {
        // Work in progress....
        /*
        Quaternion current = transform.rotation;
        Quaternion targetDirection = Quaternion.LookRotation(target - transform.position);
        rb.RotateTowards(transform.rotation, targetDirection, rotateSpeed * Time.deltaTime);
        */
    }

}
