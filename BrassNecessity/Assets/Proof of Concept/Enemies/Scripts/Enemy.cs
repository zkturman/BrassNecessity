using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyState
{
    rest,
    charge,
    attack
}



public class Enemy : MonoBehaviour
{

    GameObject player;
    Rigidbody rb;
    Animator animator;
    [SerializeField] EnemyState enemyState;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyState = EnemyState.rest;

    }


    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.rest:
                break;
            case EnemyState.charge:
                break;
            case EnemyState.attack:
                break;

        }
    }


    void EnemyStateRestLoop()
    {
        // 
    }


    void EnemyStateChargeEnter()
    {

    }


    void EnemyStateChargeLoop()
    {

    }



    IEnumerator Process()
    {
        yield return new WaitForSeconds(4f);

        ChangePlayerVisibility(true);

        yield return new WaitForSeconds(4f);

        ChangePlayerRange(true);

        yield return new WaitForSeconds(4f);

        ChangePlayerRange(false);

        yield return new WaitForSeconds(4f);

        ChangePlayerRange(true);

        yield return new WaitForSeconds(4f);

        ChangePlayerRange(false);

        yield return new WaitForSeconds(4f);

        ChangePlayerVisibility(false);

    }



    private void ChangePlayerVisibility(bool playerIsVisible)
    {
        animator.SetBool("CanSeePlayer", playerIsVisible);
    }


    private void ChangePlayerRange(bool playerIsInRange)
    {
        animator.SetBool("PlayerInHitDistance", playerIsInRange);
    }


}
