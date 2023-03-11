using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyController context)
    {
        context.animator.SetBool("PlayerTooClose", false);
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
        Debug.Log("Attack animation finished");
        
        // Do nothing if it's not an 'attack' animation that has finished
        if (animName != "Attack")
        {
            return;
        }

        // In case the player has moved position, update enemy rotation to face player
        // (Ideally this would happen smoothly, but for now it is instantaneous to see if it is noticeable to the player)
        //context.gameObject.transform.rotation.SetLookRotation(context.gameObject.transform.position - context.target.position);
        

        // Test if the player is still in range
        EnemyController.PlayerDistance playerDist = context.CheckPlayerDistance();

        if (playerDist == EnemyController.PlayerDistance.Far)
        {
            context.SwitchState(context.MoveState);
            return;
        } else if (playerDist == EnemyController.PlayerDistance.TooClose)
        {
            // The player is too close now, so back off
            context.SwitchState(context.BackPedalState);

        }



        // *** What if the player has moved slightly (or the enemy has been jostled) - update direction so next attack is still aimed at the player.

    }
}
