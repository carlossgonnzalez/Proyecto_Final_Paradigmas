
using UnityEngine;

public class PatrolState : IGhostState
{
    private readonly GhostMovement movement;
    private readonly Vector2Int[] patrolPoints;
    private readonly float waitTime;

    private int currentPoint;
    private float waitTimer;

    public PatrolState(
        GhostMovement movement,
        Vector2Int[] patrolPoints,
        float waitTime)
    {
        this.movement = movement;
        this.patrolPoints = patrolPoints;
        this.waitTime = waitTime;
    }

    public void Enter()
    {
        Debug.Log("Ghost State: Patrol");

        currentPoint = 0;
        waitTimer = waitTime;

        movement.SetDestination(
            patrolPoints[currentPoint]
        );
    }

    public void Tick()
    {
        if (movement.HasPath)
            return;

        waitTimer -= Time.deltaTime;

        if (waitTimer > 0f)
            return;

        currentPoint =
            (currentPoint + 1) % patrolPoints.Length;

        movement.SetDestination(
            patrolPoints[currentPoint]
        );

        waitTimer = waitTime;
    }

    public void Exit()
    {
        movement.StopMovement();
    }
}
