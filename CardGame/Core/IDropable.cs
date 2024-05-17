namespace CardGame.Core
{
    public interface IDropable
    {
        IDropable OnDrop(IDragable dropped, int x, int y, bool isFailedMove = false);
    }
}
