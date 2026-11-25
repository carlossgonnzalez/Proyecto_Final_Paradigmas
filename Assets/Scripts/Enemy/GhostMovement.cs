
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private MazeGrid grid;
    private AStarPathfinding pathfinding;

    private readonly ITraversalPolicy policy =
        new GhostTraversalPolicy();

    private List<Vector2Int> path =
        new List<Vector2Int>();

    private int currentPathIndex;

    public bool HasPath =>
        currentPathIndex < path.Count;

    public void Initialize(MazeGrid mazeGrid)
    {
        grid = mazeGrid;
        pathfinding = new AStarPathfinding(grid);

        StopMovement();
    }

    public void SetDestination(Vector2Int destination)
    {
        if (pathfinding == null)
            return;

        Vector2Int start =
            grid.WorldToCell(transform.position);

        path = pathfinding.FindPath(
            start,
            destination,
            policy,
            out int totalCost
        );

        currentPathIndex = 0;

        Debug.Log(
            $"Ghost path: {path.Count} nodes. " +
            $"Cost: {totalCost}"
        );
    }

    private void Update()
    {
        if (!HasPath)
            return;

        Vector3 target = grid.CellToWorld(
            path[currentPathIndex]
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(
            transform.position, target) < 0.01f)
        {
            transform.position = target;
            currentPathIndex++;
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Max(0f, newSpeed);
    }
    public void StopMovement()
    {
        path.Clear();
        currentPathIndex = 0;
    }
}
