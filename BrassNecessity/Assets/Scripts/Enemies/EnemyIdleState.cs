using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        context.animator.SetBool("CanSeePlayer", false);
    }

    public override void UpdateState(EnemyController context)
    {
        // *** Add code here to look for player and only transition to MoveState when the player is in view ***

        context.SwitchState(context.MoveState);

    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }

    public override void AnimationClipFinished(EnemyController context, string animName)
    {

    }
}
