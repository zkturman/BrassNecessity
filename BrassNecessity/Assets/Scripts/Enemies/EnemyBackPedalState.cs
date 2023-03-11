using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBackPedalState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        context.animator.SetBool("PlayerTooClose", true);
        context.animator.SetBool("PlayerInHitDistance", true);

        // Turn off NavMeshAgent rotation to keep the enemy facing the player while it backpedals
        context.navAgent.updateRotation = false;

        // Set backpedal position to retreat to



    }

    public override void UpdateState(EnemyController context)
    {
        // Check the latest distance from the player
        EnemyController.PlayerDistance playerDist = context.CheckPlayerDistance();

        if (playerDist == EnemyController.PlayerDistance.AttackRange)
        {
            context.navAgent.updateRotation = true;   // revert to allowing the NavMeshAgent to change rotation
            context.SwitchState(context.AttackState);
            return;
        }
        else if (playerDist == EnemyController.PlayerDistance.Far)
        {
            context.navAgent.updateRotation = true;   // revert to allowing the NavMeshAgent to change rotation
            context.SwitchState(context.MoveState);
            return;
        }

        // Keep backpedalling


    }


    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }

    public override void AnimationClipFinished(EnemyController context, string animName)
    {

    }

}
