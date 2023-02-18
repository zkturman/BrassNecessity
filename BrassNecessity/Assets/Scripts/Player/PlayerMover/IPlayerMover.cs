using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMover
{
    public void AddAnimationManager(ControllerAnimationManager animationManager);
    public void MovePlayer(StarterAssets.StarterAssetsInputs input);
}
