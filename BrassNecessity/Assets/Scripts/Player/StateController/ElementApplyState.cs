using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplyState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private float applyTimeout = 0.5f;
    private FrameTimeoutHandler timeoutHandler;

    private void Awake()
    {
        timeoutHandler = new FrameTimeoutHandler(applyTimeout);
    }

    public IControllerState NextState { get; set; }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateReset()
    {
        timeoutHandler.ResetTimeout();
        NextState = this;
    }

    public void StateUpdate()
    {
        timeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (timeoutHandler.HasTimeoutEnded())
        {
            NextState = GetComponent<ActionState>();
        }
    }
}
