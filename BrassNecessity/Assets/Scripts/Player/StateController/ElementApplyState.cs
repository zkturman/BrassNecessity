using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplyState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private float applyTimeout = 0.5f;
    [SerializeField]
    private GameObject playerLaser;
    [SerializeField]
    private ControllerMoveData moveData;
    [SerializeField]
    private ControllerJumpFallData jumpData;
    [SerializeField]
    private ControllerAnimationManager animationManager;

    private ElementComponent laserElement;
    private PlayerControllerInputs input;

    private FrameTimeoutHandler timeoutHandler;
    private Queue<ElementComponent> availableElements;
    private InputAgnosticMover mover;

    private void Awake()
    {
        timeoutHandler = new FrameTimeoutHandler(applyTimeout);
        laserElement = playerLaser.GetComponent<ElementComponent>();
        availableElements = new Queue<ElementComponent>();
        input = GetComponentInParent<PlayerControllerInputs>();
        mover = new InputAgnosticMover(moveData, jumpData);
        mover.AddAnimationManager(animationManager);
    }

    public IControllerState NextState { get; set; }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateEnter()
    {
        timeoutHandler.ResetTimeout();
        if (availableElements.Count > 0)
        {
            ElementComponent newElement = availableElements.Dequeue();
            laserElement.SwitchType(newElement.ElementInfo.Primary);
        }
        NextState = this;
    }

    public void StateUpdate()
    {
        timeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (timeoutHandler.HasTimeoutEnded())
        {
            NextState = GetComponent<ActionState>();
            input.applyElement = false;
        }
        else
        {
            mover.MovePlayer(input);
        }
    }

    public void AddElement(ElementComponent nextElement)
    {
        availableElements.Enqueue(nextElement);
        Debug.Log("Added element " + nextElement.ElementInfo.Primary);
    }

    public bool HasElements()
    {
        return availableElements.Count > 0;
    }
}
