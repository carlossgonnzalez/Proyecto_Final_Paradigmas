public interface ITraversalPolicy
{
    bool CanTraverse(MazeCellType cellType);

    int GetCost(MazeCellType cellType);
}