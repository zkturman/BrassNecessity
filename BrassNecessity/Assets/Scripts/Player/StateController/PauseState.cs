using UnityEngine;

public class PauseState : MonoBehaviour, IControllerState
{
    private PlayerControllerInputs _input;
    public IControllerState NextState {get; private set;}

    private void Awake()
    {
        _input = GetComponent<PlayerControllerInputs>();
        NextState = this;
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateReset()
    {
        _input.pause = false;
        NextState = this;
        Debug.Log("Switched to pause state.");
    }

    public void StateUpdate()
    {
        if (_input.pause)
        {
            NextState = GetComponent<ActionState>();
            Debug.Log("Switched to action state.");
        }
    }
}
