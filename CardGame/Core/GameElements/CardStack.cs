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

        public float Scale => _scale;
        private StackType _slotType;

        private Stack<Card> _cards;
        private int _maxStackSize;
        private bool _faceUp;

        public CardStack(Rectangle bound, float scale, StackType slotType, int stackSize = 1, bool faceUp = true)
        {
            Bound = bound;
            _scale = scale;
            _slotType = slotType;
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
            if ((_slotType == StackType.PickupOnly && !isFailedMove) || !Bound.Contains(x, y)) return null;

            if (dropped is Card card && !IsFull)
            {
                AddCard(card);
                return this;
            }

            return null;
        }

        public Card GetTopCard()
        {
            if (_slotType == StackType.Regular || _slotType == StackType.PickupOnly)
            {
                return TopCard;
            }

            return null;
        }

        internal bool AddCard(Card card)
        {
            if (IsFull) return false;

            _cards.Push(card);

            card.Flip(_faceUp);

            card.SnapToPosition(Bound.Center.ToVector2());
            card.SetScale(_scale);
            return true;
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
    }
}
