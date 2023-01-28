using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        context.animator.SetBool("PlayerInHitDistance", true);
    }

    public override void UpdateState(EnemyController context)
    {

    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // Do nothing if it's not an 'attack' animation that has finished
        if (animName != "Attack")
        {
            return;
        }


        // Test if the player is still in range
        Vector3 targetVector = context.target.transform.position - context.transform.position;
        float targetDistance = targetVector.magnitude;

        if (targetDistance > context.attackDistance)
        {
            context.animator.SetBool("PlayerInHitDistance", false);
            context.SwitchState(context.MoveState);
            return;
        }
    }
}
