using UnityEngine;

public interface ICartesianMoveHandler
{
    public Vector3 GenerateMove(Vector2 moveDirection);
    public void EnableAnimations(ControllerAnimationManager animManager);
}
