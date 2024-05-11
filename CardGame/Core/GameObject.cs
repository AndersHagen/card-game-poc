using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public abstract class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        protected float Scale;
        protected Vector2 Center;

        public GameObject(Texture2D texture, Vector2 position, float scale) 
        {
            Texture = texture;
            Position = position;
            Scale = scale;
            Center = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                Center,
                Scale,
                SpriteEffects.None,
                0f
                );
        }
        public abstract bool IsActive();
    }
}
