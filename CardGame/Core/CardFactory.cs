using CardGame.Core.GameElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CardGame.Core
{
    public static class CardFactory
    {
        public static Card CreateCard(GameObjectManager graphicsManager, Texture2D front, Texture2D back, Vector2 position)
        {
            var card = new Card(front, back, position, 0.25f);
            graphicsManager.RegisterObject(card);
            return card;
        }

        public static Texture2D BuildFrontTexture(SpriteBatch spriteBatch, Texture2D image)
        {
            var gfx = spriteBatch.GraphicsDevice;
            
            var target = new RenderTarget2D(gfx, 750, 1050);

            gfx.SetRenderTarget(target);

            gfx.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(image, new Vector2(12, 32), Color.White);
            spriteBatch.Draw(TextureManager.CardDecor2, new Vector2(270, 760), Color.White);
            spriteBatch.Draw(TextureManager.CardDecor, new Vector2(63, 630), Color.White);

            spriteBatch.Draw(TextureManager.CardInfoBar, new Vector2(63, 830), Color.White);
            spriteBatch.Draw(TextureManager.CardSwordSymbol, new Vector2(290, 870), Color.White);
            spriteBatch.Draw(TextureManager.CardHeartSymbol, new Vector2(400, 870), Color.White);

            spriteBatch.Draw(TextureManager.CardFrame, new Vector2(0, 0), Color.White);

            spriteBatch.End();

            gfx.SetRenderTarget(null);
            
            return target;
        }
    }
}
