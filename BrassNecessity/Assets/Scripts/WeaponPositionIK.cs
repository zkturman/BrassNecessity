using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPositionIK : MonoBehaviour
{
    [SerializeField]
    private Animator characterAnim;

    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform rightElbow;

    [SerializeField]
    private Transform leftHand;

    private void OnAnimatorIK(int layerIndex)
    {
        setHandPosition(AvatarIKGoal.LeftHand, leftHand);
        setHandPosition(AvatarIKGoal.RightHand, rightHand);
    }

    private void setHandPosition(AvatarIKGoal goal, Transform handObject)
    {
        characterAnim.SetIKPositionWeight(goal, 1);
        characterAnim.SetIKRotationWeight(goal, 1);
        characterAnim.SetIKPosition(goal, handObject.position);
        characterAnim.SetIKRotation(goal, handObject.rotation);
    }
}
