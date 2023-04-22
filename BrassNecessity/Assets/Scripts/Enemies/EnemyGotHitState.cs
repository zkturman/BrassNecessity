using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGotHitState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        // Trigger the 'getting hit' animation which will just loop until the laser contact is removed from the enemy
        context.animator.SetTrigger("EnemyGetsHit");
        
        // Turn off movement settings in case the enemy was moving when they were hit
        context.navAgent.isStopped = true;
        context.animator.SetBool("WalkForwards", false);
    }

    public override void UpdateState(EnemyController context)
    {
        // N/a - animation clip just loops, and damage is handled by EnemyControllerHealthHandler
    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // N/a   
    }


}
