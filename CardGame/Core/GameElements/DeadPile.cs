using CardGame.Core.GameElements.GameCards;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CardGame.Core.GameElements
{
    public class DeadPile : IDropable
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

        public void Update(GameTime gameTime)
        {
            foreach (var card in _cards)
            {
                card.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var texture = TopCard?.Texture ?? _emptyTexture;
            var tint = TopCard == null ? 0.5f : 0.3f;

            spriteBatch.Draw(
                texture,
                Position,
                null,
                Color.DarkGray * tint,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }

        public bool AddCard(Card card)
        {
            _cards.Push(card);
            
            return true;
        }

        public IDropable OnDrop(IDragable dropped, int x, int y, bool isFailedMove = false)
        {
            throw new System.NotImplementedException();
        }

        public bool Available()
        {
            return true;
        }
    }
}
