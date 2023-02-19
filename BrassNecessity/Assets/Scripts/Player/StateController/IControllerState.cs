public interface IControllerState
{
    public IControllerState NextState { get; }
    public void StateUpdate();
    public IControllerState GetNextState();
}
