public class FrameTimeoutHandler
{

    private float secondsOfTimeout;
    private float secondsPassed;

    public FrameTimeoutHandler(float secondsOfTimeout)
    {
        this.secondsOfTimeout = secondsOfTimeout;
    }

    public bool HasTimeoutEnded()
    {
        bool timeoutEnded = false;
        if (secondsPassed > secondsOfTimeout)
        {
            timeoutEnded = true;
        }
        return timeoutEnded;
    }

    public void ResetTimeout()
    {  
        secondsPassed = 0f;
    }

    public void ResetTimeout(float newTimeoutInSeconds)
    {
        secondsOfTimeout = newTimeoutInSeconds;
        secondsPassed = 0f;
    }

    public void UpdateTimePassed(float secondsPassedSinceLastFrame)
    {
        secondsPassed += secondsPassedSinceLastFrame;
    }
}
