using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CardGame.Core.GameState.Processors
{
    public class HeldCard
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Center { get; set;}

        public float Scale { get; set; }
    }
}
