public interface IControllerState
{
    public IControllerState NextState { get; }

    public void StateReset();
    public void StateUpdate();
    public IControllerState GetNextState();
}
