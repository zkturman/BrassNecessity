using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    
    public override void EnterState(EnemyController context)
    {
        context.animator.SetBool("CanSeePlayer", true);

    }


    public override void UpdateState(EnemyController context)
    {
        // *** TEST *** Disabling the code to switch to attack mode 
          
        
        // Test if the player is in attack range
        Vector3 targetVector = context.target.transform.position - context.transform.position;
        float targetDistance = targetVector.magnitude;
        
        if (targetDistance <= context.attackDistance)
        {
            // Switch to the attack state
            //Debug.Log("EnemyMoveState.UpdateState() thinks it is within hitting distance of the player");
            context.SwitchState(context.AttackState);
        } else
        {
        
            context.charaController.Move(targetVector.normalized * context.moveSpeed * Time.deltaTime);
            Quaternion qtr = Quaternion.LookRotation(targetVector, Vector3.up);
            context.transform.rotation = qtr;

        
        }
        

        /*    // THIS ALSO DOESN'T SEEM TO WORK
        // Rotate towards player
        Vector3 targetDirection = context.target.transform.position - context.transform.position;
        Debug.DrawRay(context.transform.position, targetDirection, Color.red);
        context.transform.rotation.SetLookRotation(targetDirection);
        
         // Move character controller in current forwards direction
        Vector3 forwardDirectionGlobal = context.transform.TransformDirection(Vector3.forward);
        context.charaController.SimpleMove(targetDirection.normalized * context.moveSpeed);
         */





    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // Not needed as the walking animation simply loops
    }
}
