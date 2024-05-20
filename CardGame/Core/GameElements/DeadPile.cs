using CardGame.Core.GameElements.GameCards;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CardGame.Core.GameElements
{
    public class DeadPile
    {
        private Stack<Card> _cards;

        public float Scale { get; private set; }
        private Texture2D _emptyTexture;

        public Card TopCard => _cards.Count > 0 ? _cards.Peek() : null;
        public bool IsEmpty => _cards.Count == 0;

        public Rectangle Bound { get; private set; }

        public Vector2 Position { get; private set; }
        
        public DeadPile(Point position, float scale) 
        {
            Position = position.ToVector2();
            Scale = scale;
            _cards = new Stack<Card>();
            _emptyTexture = TextureManager.DeadPileDefault;

            Bound = new Rectangle(position.X, position.Y, (int)(scale * Constants.CARD_WIDTH), (int)(scale * Constants.CARD_HEIGHT));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsEmpty) return;

            spriteBatch.Draw(
                _emptyTexture,
                Position,
                null,
                Color.DarkGray * 0.3f,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }

        public void AddCard(Card card)
        {
            _cards.Push(card);
        }
    }
}
