using UnityEngine;
using System;

[Serializable]
public class ControllerMoveData
{
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Tooltip("The transform of the game object this class is linked to.")]
    public Transform TransformReference;

    [Tooltip("The character controller of the game object this class is linked to.")]
    public CharacterController ControllerReference;
}
