
using System;
using UnityEngine;

public class MazeGrid
{
    private readonly MazeCellType[,] cells;

    public int Width { get; }
    public int Depth { get; }
    public float CellSize { get; }
    public Vector3 Origin { get; }

    public MazeGrid(
        int width,
        int depth,
        float cellSize,
        Vector3 origin)
    {
        if (width < 3 || depth < 3 || cellSize <= 0f)
        {
            throw new ArgumentException(
                "Dimensiones del laberinto no validas."
            );
        }

        Width = width;
        Depth = depth;
        CellSize = cellSize;
        Origin = origin;

        cells = new MazeCellType[width, depth];

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Depth; z++)
            {
                bool isBorder =
                    x == 0 || z == 0 ||
                    x == Width - 1 ||
                    z == Depth - 1;

                cells[x, z] = isBorder
                    ? MazeCellType.OuterWall
                    : MazeCellType.Wall;
            }
        }
    }

    public bool IsInside(Vector2Int position)
    {
        return position.x >= 0 &&
               position.y >= 0 &&
               position.x < Width &&
               position.y < Depth;
    }

    public MazeCellType GetCell(Vector2Int position)
    {
        if (!IsInside(position))
            return MazeCellType.OuterWall;

        return cells[position.x, position.y];
    }

    public void SetCell(
        Vector2Int position,
        MazeCellType type)
    {
        if (!IsInside(position))
            return;

        if (GetCell(position) == MazeCellType.OuterWall)
            return;

        cells[position.x, position.y] = type;
    }

    public Vector3 CellToWorld(Vector2Int position)
    {
        return Origin + new Vector3(
            position.x * CellSize,
            0f,
            position.y * CellSize
        );
    }

    public Vector2Int WorldToCell(Vector3 position)
    {
        Vector3 localPosition = position - Origin;

        int x = Mathf.FloorToInt(
            localPosition.x / CellSize + 0.5f
        );

        int z = Mathf.FloorToInt(
            localPosition.z / CellSize + 0.5f
        );

        return new Vector2Int(x, z);
    }
}
