using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        Debug.Log("Entering Idle state");

    }

    public override void UpdateState(EnemyController context)
    {       
        // Check distance to the player
        float distance = context.DistanceToPlayer();
        Debug.Log("Distance to player: " + distance.ToString());

        // Evaluate possible actions
        if (distance < context.closeAttackDistance)
        {
            Debug.Log("IdleState: distance < closeAttackDistance");
            // If player is too close then backpedal
            MoveToBackPedal(context);
        }
        else if (distance < context.farAttackDistance)
        {
            Debug.Log("IdleState: distance < farAttackDistance");
            // If player is in attack range either attack...
            if (context.attackersTracker.IsOpenToAttack)
            {
                PrepareToAttack(context);
            }
            // Or backpedal to hang back distance
            else
            {
                MoveToBackPedal(context);
            }
            
        }
        else if (distance < context.hangBackDistance)
        {
            Debug.Log("IdleState: distance < hangBackDistance");
            // If player is between attack distance and hang back distance
            if (context.attackersTracker.IsOpenToAttack)
            {
                MoveForwards(context);
            } else
            {
                // Backpedal to the hang back distance
                MoveToBackPedal(context);
            }

        }
        else if (distance >= context.hangBackDistance)
        {
            Debug.Log("IdleState: distance >= hangBackDistance");
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


    void MoveToBackPedal(EnemyController context)
    {
        context.SwitchState(context.BackPedalState);
    }


    void MoveForwards(EnemyController context)
    {
        context.SwitchState(context.MoveState);
    }


    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }

    public override void AnimationClipFinished(EnemyController context, string animName)
    {

    }
}
