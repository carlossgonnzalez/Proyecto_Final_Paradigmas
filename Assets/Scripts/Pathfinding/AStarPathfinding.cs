
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    private readonly MazeGrid grid;

    private static readonly Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public AStarPathfinding(MazeGrid grid)
    {
        this.grid = grid;
    }

    public List<Vector2Int> FindPath(
        Vector2Int start,
        Vector2Int goal,
        ITraversalPolicy policy,
        out int totalCost)
    {
        totalCost = -1;

        if (!grid.IsInside(start) ||
            !grid.IsInside(goal))
        {
            return new List<Vector2Int>();
        }

        if (!policy.CanTraverse(grid.GetCell(start)) ||
            !policy.CanTraverse(grid.GetCell(goal)))
        {
            return new List<Vector2Int>();
        }

        List<Vector2Int> openSet =
            new List<Vector2Int>();

        HashSet<Vector2Int> closedSet =
            new HashSet<Vector2Int>();

        Dictionary<Vector2Int, Vector2Int> cameFrom =
            new Dictionary<Vector2Int, Vector2Int>();

        Dictionary<Vector2Int, int> gScore =
            new Dictionary<Vector2Int, int>();

        openSet.Add(start);
        gScore[start] = 0;

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet[0];

            // Buscar el nodo con menor coste estimado
            foreach (Vector2Int node in openSet)
            {
                int currentF =
                    gScore[current] + Heuristic(current, goal);

                int nodeF =
                    gScore[node] + Heuristic(node, goal);

                if (nodeF < currentF)
                {
                    current = node;
                }
            }

            // Hemos encontrado el objetivo
            if (current == goal)
            {
                totalCost = gScore[current];

                return ReconstructPath(
                    cameFrom,
                    current
                );
            }

            openSet.Remove(current);
            closedSet.Add(current);

            // Explorar los vecinos
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbor = current + direction;

                if (!grid.IsInside(neighbor))
                    continue;

                if (closedSet.Contains(neighbor))
                    continue;

                MazeCellType cellType =
                    grid.GetCell(neighbor);

                if (!policy.CanTraverse(cellType))
                    continue;

                int tentativeG =
                    gScore[current] + policy.GetCost(cellType);

                if (!gScore.ContainsKey(neighbor) ||
                    tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // No existe una ruta valida
        return new List<Vector2Int>();
    }

    private int Heuristic(
        Vector2Int current,
        Vector2Int goal)
    {
        return Mathf.Abs(current.x - goal.x) +
               Mathf.Abs(current.y - goal.y);
    }

    private List<Vector2Int> ReconstructPath(
        Dictionary<Vector2Int, Vector2Int> cameFrom,
        Vector2Int current)
    {
        List<Vector2Int> path =
            new List<Vector2Int>();

        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();

        return path;
    }
}
