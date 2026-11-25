
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    [SerializeField]
    private Vector2Int start =
        new Vector2Int(2, 3);

    [SerializeField]
    private Vector2Int goal =
        new Vector2Int(6, 3);

    [SerializeField] private bool showPlayerPath = true;
    [SerializeField] private bool showGhostPath = true;

    private void OnDrawGizmosSelected()
    {
        MazeGridPreview preview =
            GetComponent<MazeGridPreview>();

        if (preview == null)
            return;

        MazeGrid grid = preview.CreateTestGrid();

        AStarPathfinding pathfinding =
            new AStarPathfinding(grid);

        if (showPlayerPath)
        {
            List<Vector2Int> playerPath =
                pathfinding.FindPath(
                    start,
                    goal,
                    new PlayerTraversalPolicy(),
                    out _
                );

            DrawPath(grid, playerPath, Color.blue, 0.3f);
        }

        if (showGhostPath)
        {
            List<Vector2Int> ghostPath =
                pathfinding.FindPath(
                    start,
                    goal,
                    new GhostTraversalPolicy(),
                    out _
                );

            DrawPath(grid, ghostPath, Color.magenta, 0.6f);
        }

        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(
            grid.CellToWorld(start) + Vector3.up,
            0.35f
        );

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(
            grid.CellToWorld(goal) + Vector3.up,
            0.35f
        );
    }

    private void DrawPath(
        MazeGrid grid,
        List<Vector2Int> path,
        Color color,
        float height)
    {
        Gizmos.color = color;

        Vector3 offset = Vector3.up * height;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position =
                grid.CellToWorld(path[i]) + offset;

            Gizmos.DrawSphere(position, 0.18f);

            if (i < path.Count - 1)
            {
                Vector3 nextPosition =
                    grid.CellToWorld(path[i + 1]) + offset;

                Gizmos.DrawLine(position, nextPosition);
            }
        }
    }

    [ContextMenu("Test AStar")]
    private void TestAStar()
    {
        MazeGridPreview preview =
            GetComponent<MazeGridPreview>();

        if (preview == null)
        {
            Debug.LogError("Falta MazeGridPreview.");
            return;
        }

        AStarPathfinding pathfinding =
            new AStarPathfinding(preview.CreateTestGrid());

        List<Vector2Int> playerPath =
            pathfinding.FindPath(
                start,
                goal,
                new PlayerTraversalPolicy(),
                out int playerCost
            );

        List<Vector2Int> ghostPath =
            pathfinding.FindPath(
                start,
                goal,
                new GhostTraversalPolicy(),
                out int ghostCost
            );

        Debug.Log(
            $"Player: {playerPath.Count} nodos. " +
            $"Coste: {playerCost}"
        );

        Debug.Log(
            $"Ghost: {ghostPath.Count} nodos. " +
            $"Coste: {ghostCost}"
        );
    }
}
