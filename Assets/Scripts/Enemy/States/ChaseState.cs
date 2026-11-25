
using UnityEngine;

public class ChaseState : IGhostState
{
    private readonly GhostMovement movement;

    private readonly float repathInterval;

    private Vector2Int targetCell;
    private Vector2Int previousTarget;

    private bool hasRequestedPath;

    private float repathTimer;

    public ChaseState(
        GhostMovement movement,
        float repathInterval)
    {
        this.movement = movement;
        this.repathInterval = repathInterval;
    }

    public void SetTarget(Vector2Int target)
    {
        targetCell = target;
    }

    public void Enter()
    {
        Debug.Log("Ghost State: Chase");

        hasRequestedPath = false;
        repathTimer = 0f;
    }

    public void Tick()
    {
        repathTimer -= Time.deltaTime;

        if (repathTimer > 0f)
            return;

        if (hasRequestedPath &&
            previousTarget == targetCell)
            return;

        movement.SetDestination(targetCell);

        previousTarget = targetCell;
        hasRequestedPath = true;

        repathTimer = repathInterval;
    }

    public void Exit()
    {
        movement.StopMovement();
    }
}
