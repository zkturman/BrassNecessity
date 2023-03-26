using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBackPedalState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        Debug.Log("Entering Backpedal state");

        // Set NavMeshAgent and Animator settings for backpedal
        context.navAgent.updateRotation = false;   // Enemy keeps facing the enemy while backing away
        //context.navAgent.isStopped = false;
        context.animator.SetBool("BackPedal", true);

        // Calculate destination
        SetBackPedalDestination(context);

    }

    public override void UpdateState(EnemyController context)
    {
        // Keep facing player as the enemy backs away
        if (!context.EnemyIsFacingPlayer())
        {
            context.TurnTowardsPlayer();
        }

        // Check to see if enemy is ready to return to Idle
        bool returnToIdle = false;
        float distance = context.DistanceToPlayer();
        if (distance > context.closeAttackDistance && context.attackersTracker.IsOpenToAttack) returnToIdle = true;
        if (context.navAgent.remainingDistance < context.navAgent.stoppingDistance) returnToIdle = true;
        if (context.navAgent.pathStatus == NavMeshPathStatus.PathPartial) returnToIdle = true;

        if (returnToIdle)
        {
            ReturnToIdle(context);
        }

    }


    private void SetBackPedalDestination(EnemyController context)
    {
        // Set destination to HangBackDistance...
        if (!context.navAgent.SetDestination(context.PositionToMoveTo(context.hangBackDistance)))
        {
            // If it returns an invalid destination then try again with the CloseAttackDistance...
            if (!context.navAgent.SetDestination(context.PositionToMoveTo(context.closeAttackDistance)))
            {
                // And if that also returns an invalid location just revert to idle...
                // *** THIS CODE CAN BE IMPROVED IN FUTUR ***
                ReturnToIdle(context);
            }
        }

        

    }


    void ReturnToIdle(EnemyController context)
    {
        UpdateSettingsOnExitState(context);
        context.SwitchState(context.IdleState);
    }


    void UpdateSettingsOnExitState(EnemyController context)
    {
        context.navAgent.updateRotation = true;
        //context.navAgent.isStopped = true;
        context.animator.SetBool("BackPedal", false);
    }




    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }

    public override void AnimationClipFinished(EnemyController context, string animName)
    {

    }

}
