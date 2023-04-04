using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerInputs input;
    private MenuNavigationControls controlHandler;

    [SerializeField]
    private float keyboardInputTimeoutInSeconds = 0.2f;
    [SerializeField]
    private float gamepadInputTimeoutInSeconds = 0.4f;
    private float inputTimeoutInSeconds;
    private FrameTimeoutHandler keyboardTimeoutHandler;
    private FrameTimeoutHandler gamepadTimeoutHandler;
    private FrameTimeoutHandler inputTimeoutHandler;

    [SerializeField]
    private StartScreenUIBehaviour menuUI;
    // Start is called before the first frame update
    void Start()
    {
        controlHandler = new MenuNavigationControls(input);
        keyboardTimeoutHandler = new FrameTimeoutHandler(keyboardInputTimeoutInSeconds);
        gamepadTimeoutHandler = new FrameTimeoutHandler(gamepadInputTimeoutInSeconds);
        determineTimeout();
    }

    // Update is called once per frame
    void Update()
    {
        determineTimeout();
        if (inputTimeoutHandler.HasTimeoutEnded())
        {
            findMenuInput();
        }
        else
        {
            inputTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        }
        if (input.shoot)
        {
            menuUI.ExecuteCurrentButton();
        }
    }

    private void findMenuInput()
    {
        int navigationValue = controlHandler.GetVerticalMovement();
        if (navigationValue != 0){
            menuUI.NavigateToNextButton(navigationValue);
            inputTimeoutHandler.ResetTimeout();
        }
    }

    private void determineTimeout()
    {
        if (CurrentDevice.IsCurrentDeviceKeyboard())
        {
            inputTimeoutInSeconds = keyboardInputTimeoutInSeconds;
            inputTimeoutHandler = keyboardTimeoutHandler;
        }
        else
        {
            inputTimeoutInSeconds = gamepadInputTimeoutInSeconds;
            inputTimeoutHandler = gamepadTimeoutHandler;
        }
    }
}
