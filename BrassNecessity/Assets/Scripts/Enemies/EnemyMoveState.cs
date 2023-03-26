using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyBaseState
{

    public override void EnterState(EnemyController context)
    {
        Debug.Log("Entering Move State");
        
        // Update settings on enter
        //context.navAgent.isStopped = false;
        context.animator.SetBool("WalkForwards", true);


        // Set destination
        float distance = context.DistanceToPlayer();
        if (distance <= context.hangBackDistance)
        {
            // If the enemy is already closer than the HangBackDistance then assume moving to attack distance
            float avgAttackDistance = context.closeAttackDistance + ((context.farAttackDistance - context.closeAttackDistance) / 2);
            context.navAgent.SetDestination(context.PositionToMoveTo(avgAttackDistance));
        } else
        {
            // Assume moving forwards to hangback location
            context.navAgent.SetDestination(context.PositionToMoveTo(context.hangBackDistance - 0.5f));   // Minus 0.5f to make sure enemy finished inside the hangback range.
        }

    }


    public override void UpdateState(EnemyController context)
    {
        // Check if it is time to return to idle state
        bool returnToIdle = false;

        //Debugging
        Debug.Log(string.Format("MoveState: NavAgent.remainingDistance = {0}; distanceToPlayer = {1}", context.navAgent.remainingDistance.ToString(), context.DistanceToPlayer().ToString()));


        if (context.navAgent.remainingDistance < 0.01f) returnToIdle = true;
        if (context.navAgent.pathStatus == NavMeshPathStatus.PathInvalid) returnToIdle = true;


        if (returnToIdle)
        {
            Debug.Log("MoveState is returning to Idle");
            ReturnToIdleState(context);
        }
    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        
    }


    void ReturnToIdleState(EnemyController context)
    {
        UpdateSettingsOnExit(context);
        context.SwitchState(context.IdleState);
    }


    void UpdateSettingsOnExit(EnemyController context)
    {
        context.animator.SetBool("WalkForwards", false);
        //context.navAgent.isStopped = true;
    }
}
