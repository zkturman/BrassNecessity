public interface IControllerState
{
    public void StateUpdate();
    public IControllerState GetNextState();
    public void LateStateUpdate();
}
