using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public class Card : GameObject
    {
        public Card(Texture2D texture, Vector2 position, Vector2 scale) : base(texture, position, scale)
        {
        }

        public override void IsActive()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
