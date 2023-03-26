using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{

    public abstract void EnterState(EnemyController context);

    public abstract void UpdateState(EnemyController context);

    public abstract void CollisonEntered(EnemyController context, Collision collision);
    
    public abstract void AnimationClipFinished(EnemyController context, string animName);


}
