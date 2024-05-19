using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CardGame.Core.GameElements
{
    public class Card : GameObject, IDragable
    {
        private Rectangle _bound;
        private bool _held;
        private Texture2D _cardBack;
        private Texture2D _cardFront;

        private bool _isFaceUp;

        private Vector2 _velocity;

        public Card(Texture2D front, Texture2D back, Vector2 position, float scale) : base(front, position, scale)
        {
            _cardBack = back;
            _cardFront = front;
            SetBound();
            _held = false;
            Texture = front;
            _isFaceUp = true;
            _velocity = Vector2.Zero;
        }

        private void SetBound()
        {
            var top = (Position - Center * Scale).ToPoint();
            var width = Texture.Width * Scale;
            var height = Texture.Height * Scale;

            _bound = new Rectangle(top.X, top.Y, (int)width, (int)height);
        }

        public override bool IsActive()
        {
            return true;
        }

        public bool IsClicked(int x, int y)
        {
            return _bound.Contains(x, y);
        }

        public void Lift()
        {
            Scale += 0.02f;
        }

        public void Drop(CardStack target = null)
        {
            if (target != null)
            {
                Scale = target.Scale;
            }
            else
            {
                Scale -= 0.02f;
            }

            _held = false;
        }

        public override void Update(GameTime gameTime)
        {
            Texture = _isFaceUp ? _cardFront : _cardBack;

            Position += _velocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void OnClick(int x, int y)
        {
            Lift();
            _held = true;
        }

        public void OnRelease(int x, int y)
        {
            if (!_held) return;
            Drop();
            _held = false;
        }

        public void OnDrag(int x, int y)
        {
            SnapToPosition(new Vector2(x, y));
        }

        internal void SnapToPosition(Vector2 position)
        {
            Position = position;
            SetBound();
        }

        internal void SetScale(float scale)
        {
            Scale = scale;
        }

        internal void Flip(bool faceUp)
        {
            _isFaceUp = faceUp;
        }
    }
}
