using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVerticalMoveHandler
{
    public Vector3 GenerateMove(bool shouldJump);
    public void EnableAnimations(ControllerAnimationManager animManager);

}
