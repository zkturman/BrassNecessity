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
        /*   
        
        // Test if the player is in attack range
        Vector3 targetVector = context.target.transform.position - context.transform.position;
        float targetDistance = targetVector.magnitude;
        
        if (targetDistance <= context.attackDistance)
        {
            // Switch to the attack state
            //Debug.Log("EnemyMoveState.UpdateState() thinks it is within hitting distance of the player");
            context.SwitchState(context.AttackState);
            return;
        } else
        {
        
        // ** Original code - to be removed when testing complete ***
        //Vector3 currentDirection = context.transform.forward;
        //Vector3 targetDirection = context.target.transform.position - context.transform.position;
        //float maxTurnPerFrame = context.turnSpeed * Time.deltaTime * Mathf.Deg2Rad;   // Vector3.RotateTowards requires 'maxTurn' value to be in radians not degrees
        //float maxChangeInMagnitude = 0f;    // Since we only care about the direction, we don't want the magnitude of the new Vector3 to increase (currentDirection is already normalized, so setting maxChangeInMagnitude to 0 means the magnitude is locked at 1)
        //Vector3 newDirection = Vector3.RotateTowards(currentDirection, targetDirection, maxTurnPerFrame, maxChangeInMagnitude);
        //context.transform.rotation = Quaternion.LookRotation(newDirection);
        
        }
        */

        /*    // THIS ALSO DOESN'T SEEM TO WORK
        // Rotate towards player
        Vector3 targetDirection = context.target.transform.position - context.transform.position;
        Debug.DrawRay(context.transform.position, targetDirection, Color.red);
        context.transform.rotation.SetLookRotation(targetDirection);
        
         // Move character controller in current forwards direction
        Vector3 forwardDirectionGlobal = context.transform.TransformDirection(Vector3.forward);
        context.charaController.SimpleMove(targetDirection.normalized * context.moveSpeed);
         */


        Vector3 targetDirection = context.target.transform.position - context.transform.position;
        context.charaController.Move(targetDirection.normalized * context.moveSpeed * Time.deltaTime);

        // Code from IHeartGameDev video
        Vector3 positionToLookAt;
        Quaternion currentRotation;



        



    }

    public override void CollisonEntered(EnemyController context, Collision collision)
    {

    }


    public override void AnimationClipFinished(EnemyController context)
    {
        // Not needed as the walking animation simply loops
    }
}
