
using UnityEngine;

public class MazeGridPreview : MonoBehaviour
{
    public MazeGrid CreateTestGrid()
    {
        MazeGrid grid = new MazeGrid(
            9,
            9,
            2f,
            transform.position
        );

        // Crear pasillos interiores
        for (int x = 1; x < grid.Width - 1; x++)
        {
            for (int z = 1; z < grid.Depth - 1; z++)
            {
                grid.SetCell(
                    new Vector2Int(x, z),
                    MazeCellType.Floor
                );
            }
        }

        // Crear una pared interior
        for (int z = 1; z <= 6; z++)
        {
            grid.SetCell(
                new Vector2Int(4, z),
                MazeCellType.Wall
            );
        }

        return grid;
    }

    private void OnDrawGizmosSelected()
    {
        MazeGrid grid = CreateTestGrid();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int z = 0; z < grid.Depth; z++)
            {
                Vector2Int cell = new Vector2Int(x, z);

                MazeCellType type = grid.GetCell(cell);

                switch (type)
                {
                    case MazeCellType.Floor:
                        Gizmos.color = Color.green;
                        break;

                    case MazeCellType.Wall:
                        Gizmos.color = Color.red;
                        break;

                    case MazeCellType.ClosedDoor:
                        Gizmos.color = Color.yellow;
                        break;

                    default:
                        Gizmos.color = Color.gray;
                        break;
                }

                Vector3 position = grid.CellToWorld(cell);

                Gizmos.DrawCube(
                    position,
                    new Vector3(
                        grid.CellSize * 0.9f,
                        0.1f,
                        grid.CellSize * 0.9f
                    )
                );
            }
        }
    }
}
