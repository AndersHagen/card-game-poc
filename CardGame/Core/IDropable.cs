using Microsoft.Xna.Framework;

namespace CardGame.Core
{
    public interface IDropable
    {
        Rectangle Bound { get; }

        IDropable OnDrop(IDragable dropped, int x, int y, bool isFailedMove = false);
    }
}
