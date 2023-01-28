using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        // Set animator to trigger death state
        context.animator.SetTrigger("EnemyKilled");
    }

    public override void UpdateState(EnemyController context)
    {

    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }

    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // Check that it is the 'death' animation clip which has finished (
        if (animName != "DeathAnim")
        {
            // This happens when the preceding animation finished before 
            return;
        }
        
        
        // Enemy has now finished its death animation and should vanish (and notify the EnemySpawnManager)
        
        
        // Notify the spawn manager
        context.spawnManager?.EnemyHasDied();
        
        // Remove the dead body
        Object.Destroy(context.gameObject);
        
    }





}
