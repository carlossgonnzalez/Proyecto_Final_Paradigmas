
public class GhostStateMachine
{
    public IGhostState CurrentState { get; private set; }

    public void ChangeState(IGhostState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState?.Enter();
    }

    public void Tick()
    {
        CurrentState?.Tick();
    }
}
