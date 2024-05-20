using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements
{
    public class Deck
    {
        private Stack<Card> _cards;

        private int _maxSize;
        public float Scale { get; private set; }
        private Texture2D _emptyTexture;

        public Card TopCard => _cards.Count > 0 ? _cards.Peek() : null;
        public bool IsFull => _cards.Count == _maxSize;
        public bool IsEmpty => _cards.Count == 0;

        public Rectangle Bound {  get; private set; }

        public Vector2 Position { get; private set; }

        public Deck(Point position, Texture2D placeHolderTexture, int maxSize, float scale) 
        {
            _cards = new Stack<Card>(); 
            Position = position.ToVector2();
            _maxSize = maxSize;
            Scale = scale;
            _emptyTexture = placeHolderTexture;

            Bound = new Rectangle(position.X, position.Y, (int)(scale * Constants.CARD_WIDTH), (int)(scale * Constants.CARD_HEIGHT));
        }

        public Card GetTopCard()
        {
            var card = TopCard;

            if (card != null)
            {
                _cards.Pop();
            }

            return card;
        }

        public void Shuffle()
        {
            if (_cards.Count <= 1) return;

            var rnd = new Random((int)(DateTime.Now.Ticks % Int32.MaxValue));

            var cards = _cards.ToList();

            _cards.Clear();

            while (cards.Count > 0)
            {
                var idx = rnd.Next(0, cards.Count);

                _cards.Push(cards[idx]);
                cards.RemoveAt(idx);
            }
        }

        public bool AddCard(Card card)
        {
            if (IsFull) return false;

            _cards.Push(card);

            card.Flip(false);

            card.SnapToPosition(Bound.Center.ToVector2());
            card.SetScale(Scale);
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (TopCard != null) return;

            spriteBatch.Draw(
                _emptyTexture,
                Position,
                null,
                Color.White * 0.3f,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                1
            );
        }
    }
}
