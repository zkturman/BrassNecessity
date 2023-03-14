using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplyState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private float applyTimeout = 0.5f;
    [SerializeField]
    private GameObject playerLaser;
    private ElementComponent laserElement;
    private PlayerControllerInputs input;

    private FrameTimeoutHandler timeoutHandler;
    private Queue<ElementComponent> availableElements;

    private void Awake()
    {
        timeoutHandler = new FrameTimeoutHandler(applyTimeout);
        laserElement = playerLaser.GetComponent<ElementComponent>();
        availableElements = new Queue<ElementComponent>();
        input = GetComponent<PlayerControllerInputs>();
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
            Debug.Log("Applying element " + newElement.ElementInfo.Primary);
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
