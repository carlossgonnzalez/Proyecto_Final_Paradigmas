public class PlayerTraversalPolicy : ITraversalPolicy
{
    public bool CanTraverse(MazeCellType cellType)
    {
        return cellType == MazeCellType.Floor;
    }

    public int GetCost(MazeCellType cellType)
    {
        return CanTraverse(cellType) ? 1 : -1;
    }
}