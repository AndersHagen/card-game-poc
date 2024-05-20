using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.GameElements
{
    public class StackGroup : IClickable //, IDropable
    {
        protected Point _location;
        public Rectangle Bound { get; protected set; }
        protected float _scale;
        protected CardStack[] _stacks;

        protected Texture2D _solid;

        protected StackType _slotType;
        
        public float Scale => _scale;

        public List<CardStack> Stacks => _stacks.ToList();

        public StackGroup(Point location, int stacks, float scale, int padding = 10, StackType slotType = StackType.Regular, int stackSize = 1, bool isFaceUp = true) 
        {
            _location = location;
            _scale = scale;
            _slotType = slotType;

            var width = (int)(scale * (stacks * (Constants.CARD_WIDTH + padding) + padding));
            var height = (int)(scale * (Constants.CARD_HEIGHT + padding + padding));

            Bound = new Rectangle(_location, new Point(width, height));
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

            spriteBatch.Draw(_solid, Bound, Color.DarkBlue * 0.3f);

            foreach (var slot in _stacks)
            {
                spriteBatch.Draw(_solid, slot.Bound, Color.DarkSlateGray * 0.3f);
            }
        }

        public List<CardStack> GetEmptyStacks()
        {
            return _stacks.Where(s => s.IsEmpty).ToList();
        }
    }
}
