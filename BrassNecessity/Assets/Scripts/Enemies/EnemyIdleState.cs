using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        // Stop movement, in case the enemy was previously in a movement state        
        context.navAgent.isStopped = true;
        context.animator.SetBool("WalkForwards", false);
    }


    public override void UpdateState(EnemyController context)
    {       
        // Check distance to the player
        float distance = context.DistanceToPlayer();
        //Debug.Log("Distance to player: " + distance.ToString());

        // Evaluate possible actions
        if (distance <= context.farAttackDistance)
        {
           //Debug.Log("IdleState: distance <= farAttackDistance");
            PrepareToAttack(context);
        }
        else
        {
            //Debug.Log("IdleState: distance > farAttackDistance");
            MoveForwards(context);
        }
    }


    private void PrepareToAttack(EnemyController context)
    {
        // Check if the enemy is facing towards the player, and change direction if needed before beginning attack
        if (context.EnemyIsFacingPlayer())
        {
            // Enemy is facing towards the player enough to begin attacking
            context.SwitchState(context.AttackState);
        }
        else
        {
            // enemy is facing too far away from player
            context.TurnTowardsPlayer();
        }
    }


    void MoveForwards(EnemyController context)
    {
        context.SwitchState(context.MoveState);
    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {

    }
}
