using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CardGame.Core.GameElements.GameCards
{
    public class Card : IDragable
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Center;

        private bool _held;
        private Texture2D _cardBack;
        private Texture2D _cardFront;

        private bool _isFaceUp;

        private Vector2 _velocity;
        public float Scale;

        public Card(Texture2D front, Texture2D back, Vector2 position, float scale)
        {
            Position = position;
            Texture = front;
            Center = new Vector2(Texture.Width / 2, Texture.Height / 2);
            _cardBack = back;
            _cardFront = front;
            Scale = scale;
            _held = false;
            _isFaceUp = true;
            _velocity = Vector2.Zero;
        }

        public bool IsClicked(int x, int y)
        {
            return false;
        }

        public Texture2D Lift()
        {
            _held = true;
            return Texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            Texture = _isFaceUp ? _cardFront : _cardBack;

            Position += _velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var tone = _held ? Color.Gray : Color.White;

            spriteBatch.Draw(
                Texture,
                Position,
                null,
                tone,
                0f,
                Center,
                Scale,
                SpriteEffects.None,
                0f
                );
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void OnClick(int x, int y)
        {
        }

        public void OnRelease(int x, int y)
        {
        }

        public void OnDrag(int x, int y)
        {
            SnapToPosition(new Vector2(x, y));
        }

        internal void SnapToPosition(Vector2 position)
        {
            Position = position;
        }

        internal void SetScale(float scale)
        {
            Scale = scale;
        }

        internal void Flip(bool faceUp)
        {
            _isFaceUp = faceUp;
        }

        internal void Release()
        {
            _held = false;
        }
    }
}
