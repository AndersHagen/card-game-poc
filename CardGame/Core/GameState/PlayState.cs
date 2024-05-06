using CardGame.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameState
{
    public class PlayState
    {
        public GameObjectManager GameObjectManager;
        public InputHandler InputHandler;

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            InputHandler = new InputHandler();
        }

        public void LoadContent(ContentManager contentManager)
        {
            var cardback = contentManager.Load<Texture2D>("gfx/cards/cardback1");

            CardFactory.CreateCard(GameObjectManager, cardback, new Vector2(400, 400));
            CardFactory.CreateCard(GameObjectManager, cardback, new Vector2(800, 400));
        }

        public void Update(GameTime gameTime)
        {
            GameObjectManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameObjectManager.Draw(spriteBatch);
        }
    }
}
