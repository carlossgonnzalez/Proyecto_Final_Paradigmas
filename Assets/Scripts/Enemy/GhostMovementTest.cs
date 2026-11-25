
using UnityEngine;

public class GhostMovementTest : MonoBehaviour
{
    [SerializeField] private GhostMovement ghost;

    [SerializeField]
    private Vector2Int start =
        new Vector2Int(2, 3);

    [SerializeField]
    private Vector2Int destination =
        new Vector2Int(6, 3);

    private void Start()
    {
        MazeGridPreview preview =
            GetComponent<MazeGridPreview>();

        if (preview == null || ghost == null)
        {
            Debug.LogError(
                "Missing Ghost or MazeGridPreview."
            );

            return;
        }

        MazeGrid grid = preview.CreateTestGrid();

        ghost.transform.position =
            grid.CellToWorld(start);

        ghost.Initialize(grid);
        ghost.SetDestination(destination);
    }
}