using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private float switchTimeout = 0.5f;
    private FrameTimeoutHandler timeoutHandler;
    public int SwitchValue { get; set; }

    public IControllerState NextState { get; private set; }
    private void Awake()
    {
        timeoutHandler = new FrameTimeoutHandler(switchTimeout);
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateReset()
    {
        NextState = this;
        timeoutHandler.ResetTimeout();
        Debug.Log(string.Format("Switching weapons {0}.", SwitchValue > 0 ? "right" : "left"));
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
