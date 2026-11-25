
using UnityEngine;

public class StunnedState : IGhostState
{
    private readonly GhostMovement movement;

    private float remainingTime;

    public bool IsFinished => remainingTime <= 0f;

    public StunnedState(GhostMovement movement)
    {
        this.movement = movement;
    }

    public void SetDuration(float duration)
    {
        remainingTime = Mathf.Max(
            remainingTime,
            duration
        );
    }

    public void Enter()
    {
        Debug.Log("Ghost State: Stunned");

        movement.StopMovement();
    }

    public void Tick()
    {
        remainingTime -= Time.deltaTime;
    }

    public void Exit()
    {
        remainingTime = 0f;

        Debug.Log("Ghost recovered from stun");
    }
}
