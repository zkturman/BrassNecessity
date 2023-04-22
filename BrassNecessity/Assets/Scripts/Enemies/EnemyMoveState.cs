using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyBaseState
{

    public override void EnterState(EnemyController context)
    {
        //Debug.Log("Entering Move State");
        
        // Update settings on enter
        context.animator.SetBool("WalkForwards", true);

        // Set destination
        UpdateDestination(context);
    }


    public override void UpdateState(EnemyController context)
    {
        //Debugging
        //Debug.Log(string.Format("MoveState: NavAgent.remainingDistance = {0}; distanceToPlayer = {1}", context.navAgent.remainingDistance.ToString(), context.DistanceToPlayer().ToString()));

        // Check if it is time to return to idle state
        bool returnToIdle = false;

        if (context.navAgent.remainingDistance < 0.01f) returnToIdle = true;
        if (context.navAgent.pathStatus == NavMeshPathStatus.PathInvalid) returnToIdle = true;
        if (context.navAgent.pathStatus == NavMeshPathStatus.PathPartial) returnToIdle = true;

        if (returnToIdle)
        {
            //Debug.Log("MoveState is returning to Idle");
            ReturnToIdleState(context);
        }
        else
        {
            UpdateDestination(context);
        }
    }


    private void UpdateDestination(EnemyController context)
    {
        context.navAgent.SetDestination(context.PositionToMoveTo(context.midAttackDistance));
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
