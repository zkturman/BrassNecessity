using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    bool firstAttack = true;   // Use this to flip between the two different attacks

    public override void EnterState(EnemyController context)
    {
        //Debug.Log("Entering Attack state");

        // Choose which attack to use this time        
        if (firstAttack)
        {
            context.animator.SetTrigger("StartAttack01");
            firstAttack = false;
        } else
        {
            context.animator.SetTrigger("StartAttack02");
            firstAttack = true;
        }

        // // Original method: activate trigger collider on EnemyWeapon component.
        //context.enemyWeapon.ActivateWeapon();
        // // This has been replaced.  The attack animations now trigger an event on the EnemyController to detect if the player has been hit.

       
    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        if (animName == "Attack")
        {
            // Attack animation has completed.
            context.SwitchState(context.IdleState);
        }
    }



    public override void UpdateState(EnemyController context)
    {

    }


    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


}
