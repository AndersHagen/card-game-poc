using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;

namespace CardGame.Core.GameState.Processors
{
    public class DrawnCard
    {
        public Card Card { get; set; }
        public IDropable Target { get; set; }
        public Vector2 TargetPosition { get; set;}
    }
}
