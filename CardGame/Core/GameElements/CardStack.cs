using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.GameElements
{
    public class CardStack : IClickable, IDropable
    {
        public Rectangle Bound { get; private set; }

        private float _scale;

        public Card TopCard => _cards.Count > 0 ? _cards.Peek() : null;

        public bool IsFull => _cards.Count == _maxStackSize;
        public bool IsEmpty => _cards.Count == 0;

        private StackType _stackType;

        private Stack<Card> _cards;
        private int _maxStackSize;
        private bool _faceUp;

        private bool NoDrop => _stackType == StackType.PickupOnly || _stackType == StackType.NotInteractive;

        public CardStack(Rectangle bound, float scale, StackType slotType, int stackSize = 1, bool faceUp = true)
        {
            Bound = bound;
            _scale = scale;
            _stackType = slotType;
            _cards = new Stack<Card>();
            _maxStackSize = stackSize;
            _faceUp = faceUp;
        }   

        public bool IsClicked(int x, int y)
        {
            return Bound.Contains(x, y);
        }

        public IClickable OnClick(int x, int y)
        {
            if (!IsClicked(x, y)) return null;

            return this;
        }

        public void PopCard()
        {
            _cards.Pop();
        }

        public IDropable OnDrop(IDragable dropped, int x, int y, bool isFailedMove = false)
        {
            if ((NoDrop && !isFailedMove) || !Bound.Contains(x, y)) return null;

            if (dropped is Card card && !IsFull)
            {
                AddCard(card);
                return this;
            }

            return null;
        }

        public Card PeekTopCard(bool force = false)
        {
            if (force || _stackType == StackType.Regular || _stackType == StackType.PickupOnly)
            {
                return TopCard;
            }

            return null;
        }

        public bool AddCard(Card card)
        {
            if (IsFull) return false;

            _cards.Push(card);

            card.Flip(_faceUp);

            card.SnapToPosition(Bound.Center.ToVector2());
            card.SetScale(_scale);
            return true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var card in _cards)
            {
                card.Update(gameTime);
            }
        }

        public bool Available()
        {
            return !IsFull;
        }
    }
}
