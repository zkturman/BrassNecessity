public interface IControllerState
{
    public IControllerState NextState { get; }

    public void StateEnter();
    public void StateUpdate();
    public IControllerState GetNextState();
}
