using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements
{
    public class DeploymentArea : IClickable, IDropable
    {
        // 200 x 275  (187,5 x 262,5)

        private Point _location;
        private Rectangle _areaBorder;
        private float _scale;
        private DeploySlot[] _deploySlots;

        private Texture2D _solid;

        public DeploymentArea(Point location, int slots, float scale, int padding = 10) 
        {
            _location = location;
            _scale = scale;

            var width = (int)(scale * (slots * (Constants.CARD_WIDTH + padding) + padding));
            var height = (int)(scale * (Constants.CARD_HEIGHT + padding + padding));

            _areaBorder = new Rectangle(_location, new Point(width, height));
            _deploySlots = new DeploySlot[slots];

            for (var i = 0; i < _deploySlots.Length; i++)
            {
                var offset = (int)(scale * ((Constants.CARD_WIDTH + padding) * i + padding));
                _deploySlots[i] = new DeploySlot(new Rectangle(_location.X + offset, _location.Y + (int)(scale * padding), (int)(scale * Constants.CARD_WIDTH), (int)(scale * Constants.CARD_HEIGHT)), scale);
            }
        }

        public bool IsClicked(int x, int y)
        {
            throw new NotImplementedException();
        }

        public IClickable OnClick(int x, int y)
        {
            foreach (var slot in _deploySlots)
            {
                var clicked = slot.OnClick(x, y);

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

            spriteBatch.Draw(_solid, _areaBorder, Color.DarkBlue * 0.3f);

            foreach (var slot in _deploySlots)
            {
                spriteBatch.Draw(_solid, slot.Bound, Color.DarkSlateGray * 0.3f);
            }
        }

        public bool OnDrop(IDragable dropped, int x, int y)
        {
            foreach (var slot in _deploySlots)
            {
                if (slot.OnDrop(dropped, x, y)) return true;
            }

            return false;
        }

        public void AssignCardToSlot(Card card, int slot)
        {
            _deploySlots[slot].SetCard(card);
        }
    }
}
