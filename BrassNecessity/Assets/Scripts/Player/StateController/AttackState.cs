using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IControllerState
{
    public IControllerState NextState { get; private set; }
    public bool IsStrongAttack { get; set; }
    [SerializeField]
    private float lightAttackTimeout = 0.5f;
    [SerializeField]
    private float strongAttackTimeout = 1f;
    private FrameTimeoutHandler attackTimeoutHandler;

    private void Awake()
    {
        attackTimeoutHandler = new FrameTimeoutHandler(0f);
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateReset()
    {
        NextState = this;
        float timeout = determineTimeout();
        attackTimeoutHandler.ResetTimeout(timeout);
        Debug.Log(string.Format("Using {0} attack.", IsStrongAttack ? "strong" : "light"));
    }

    private float determineTimeout()
    {
        float newTimeout;
        if (IsStrongAttack)
        {
            newTimeout = strongAttackTimeout;
        }
        else
        {
            newTimeout = lightAttackTimeout;
        }
        return newTimeout;
    }

    public void StateUpdate()
    {
        attackTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (attackTimeoutHandler.HasTimeoutEnded())
        {
            NextState = GetComponent<ActionState>();
        }
    }
}
