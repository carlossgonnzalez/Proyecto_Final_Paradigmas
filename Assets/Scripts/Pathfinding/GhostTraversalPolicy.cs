public class GhostTraversalPolicy : ITraversalPolicy
{
    public bool CanTraverse(MazeCellType cellType)
    {
        return cellType != MazeCellType.OuterWall;
    }

    public int GetCost(MazeCellType cellType)
    {
        switch (cellType)
        {
            case MazeCellType.Floor:
                return 1;

            case MazeCellType.Wall:
            case MazeCellType.ClosedDoor:
                return 3;

            default:
                return -1;
        }
    }
}