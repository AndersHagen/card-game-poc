using Microsoft.Xna.Framework;
using System;

namespace CardGame.Core.GameElements
{
    public class DeploySlot : IClickable, IDropable
    {
        public Rectangle Bound { get; private set; }

        private float _scale;

        public Card DeployedCard { get; private set; }

        public DeploySlot(Rectangle bound, float scale)
        {
            Bound = bound;
            _scale = scale;
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

        public void OnDrag(int x, int y)
        {
            return;
        }

        public void PopCard()
        {
            DeployedCard = null;
        }

        public bool OnDrop(IDragable dropped, int x, int y)
        {
            if (!Bound.Contains(x, y)) return false;

            if (dropped is Card card && DeployedCard == null)
            {
                DeployedCard = card;
                card.SnapToPosition(Bound.Center.ToVector2());
                return true;
            }

            return false;
        }

        internal void SetCard(Card card)
        {
            DeployedCard = card;
            card.SnapToPosition(Bound.Center.ToVector2());
            card.SetScale(_scale);
        }
    }
}
