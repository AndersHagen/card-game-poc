using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CardGame.Core.GameState.Processors
{
    public class ActiveCard
    {
        public Card Card { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public IDropable Target { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Vector2 Center { get; set;}

        public float Scale { get; set; }
    }
}
