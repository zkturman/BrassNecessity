using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
   

    public override void EnterState(EnemyController context)
    {
        context.navAgent.isStopped = false;
        context.navAgent.SetDestination(context.target.position);
        context.animator.SetBool("PlayerInHitDistance", false);
        context.animator.SetBool("PlayerTooClose", false);
        context.animator.SetBool("CanSeePlayer", true);
    }


    public override void UpdateState(EnemyController context)
    {
        // Test if the player is in attack range
        EnemyController.PlayerDistance playerDist = context.CheckPlayerDistance();

        if (playerDist == EnemyController.PlayerDistance.AttackRange)
        {
            // Switch to the attack state
            context.SwitchState(context.AttackState);
        } else if (playerDist == EnemyController.PlayerDistance.TooClose)
        {
            // If the enemy has already got too close
            context.SwitchState(context.BackPedalState);
        } else
        {
            // Update NavMeshAgent to move to player's latest position
            context.navAgent.SetDestination(context.target.position);
        }
    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // Not needed as the walking animation simply loops
    }
}
