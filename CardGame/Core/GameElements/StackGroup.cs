using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CardGame.Core.GameElements
{
    public class StackGroup : IClickable, IDropable
    {
        private Point _location;
        private Rectangle _boundary;
        private float _scale;
        private CardStack[] _stacks;

        private Texture2D _solid;

        private StackType _slotType;
        
        public float Scale => _scale;

        public StackGroup(Point location, int stacks, float scale, int padding = 10, StackType slotType = StackType.Regular, int stackSize = 1, bool isFaceUp = true) 
        {
            _location = location;
            _scale = scale;
            _slotType = slotType;

            var width = (int)(scale * (stacks * (Constants.CARD_WIDTH + padding) + padding));
            var height = (int)(scale * (Constants.CARD_HEIGHT + padding + padding));

            _boundary = new Rectangle(_location, new Point(width, height));
            _stacks = new CardStack[stacks];

            for (var i = 0; i < _stacks.Length; i++)
            {
                var offset = (int)(scale * ((Constants.CARD_WIDTH + padding) * i + padding));
                var bound = new Rectangle(_location.X + offset, _location.Y + (int)(scale * padding), (int)(scale * Constants.CARD_WIDTH), (int)(scale * Constants.CARD_HEIGHT));
                _stacks[i] = new CardStack(bound, scale, _slotType, stackSize, isFaceUp);
            }
        }

        public bool IsClicked(int x, int y)
        {
            throw new NotImplementedException();
        }

        public IClickable OnClick(int x, int y)
        {
            foreach (var stack in _stacks)
            {
                var clicked = stack.OnClick(x, y);

                if (clicked != null) return clicked;
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_solid == null)
            {
                _solid = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _solid.SetData(new Color[1] { Color.White });
            }

            spriteBatch.Draw(_solid, _boundary, Color.DarkBlue * 0.3f);

            foreach (var slot in _stacks)
            {
                spriteBatch.Draw(_solid, slot.Bound, Color.DarkSlateGray * 0.3f);
            }
        }

        public IDropable OnDrop(IDragable dropped, int x, int y, bool isFailedMove = false)
        {
            foreach (var slot in _stacks)
            {
                var dropTarget = slot.OnDrop(dropped, x, y, isFailedMove);

                if (dropTarget != null) return dropTarget;
            }

            return null;
        }

        public void AssignCardToSlot(Card card, int stackNumber)
        {
            _stacks[stackNumber].AddCard(card);
        }

        internal void Shuffle(int stackNumber)
        {
            _stacks[stackNumber].Shuffle();
        }
    }
}
