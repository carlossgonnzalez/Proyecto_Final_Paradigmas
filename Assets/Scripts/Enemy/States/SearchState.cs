
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IGhostState
{
    private readonly GhostMovement movement;
    private readonly MazeGrid grid;
    private readonly float waitTime;

    private readonly List<Vector2Int> searchPoints =
        new List<Vector2Int>();

    private Vector2Int lastKnownPosition;

    private int currentPoint;
    private float waitTimer;

    public bool IsFinished { get; private set; }

    public SearchState(
        GhostMovement movement,
        MazeGrid grid,
        float waitTime)
    {
        this.movement = movement;
        this.grid = grid;
        this.waitTime = waitTime;
    }

    public void SetTarget(Vector2Int position)
    {
        lastKnownPosition = position;
    }

    public void Enter()
    {
        Debug.Log("Ghost State: Search");

        IsFinished = false;
        currentPoint = 0;
        waitTimer = waitTime;

        GenerateSearchPoints();

        if (searchPoints.Count == 0)
        {
            IsFinished = true;
            return;
        }

        movement.SetDestination(searchPoints[0]);
    }

    private void GenerateSearchPoints()
    {
        searchPoints.Clear();

        // Investigar primero la ultima posicion conocida
        if (grid.GetCell(lastKnownPosition) != MazeCellType.Floor)
            return;

        searchPoints.Add(lastKnownPosition);

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        // Buscar celdas libres alrededor
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbor =
                lastKnownPosition + direction;

            if (grid.GetCell(neighbor) == MazeCellType.Floor)
            {
                searchPoints.Add(neighbor);
            }
        }
    }

    public void Tick()
    {
        if (IsFinished || movement.HasPath)
            return;

        waitTimer -= Time.deltaTime;

        if (waitTimer > 0f)
            return;

        currentPoint++;

        if (currentPoint >= searchPoints.Count)
        {
            IsFinished = true;
            return;
        }

        movement.SetDestination(
            searchPoints[currentPoint]
        );

        waitTimer = waitTime;
    }

    public void Exit()
    {
        movement.StopMovement();
    }
}
