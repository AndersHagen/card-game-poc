﻿using CardGame.Core;
using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CardGame.Data
{
    public static class TextureManager
    {
        public static Dictionary<CardId, Texture2D> CardImages = new Dictionary<CardId, Texture2D>();

        public static Texture2D CardBack;
        public static Texture2D CardFrame;
        public static Texture2D CardDecor;
        public static Texture2D CardDecor2;
        public static Texture2D CardHeartSymbol;
        public static Texture2D CardShieldSymbol;
        public static Texture2D CardSwordSymbol;
        public static Texture2D CardInfoBar;
        public static Texture2D CardStar;
        public static Texture2D CardStarframe;
        public static Texture2D BackgroundDarkFrost;
        public static Texture2D DeadPileDefault;

        public static SpriteFont SimpleFont;

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            CardBack = contentManager.Load<Texture2D>("gfx/cards/cardback1");
            CardFrame = contentManager.Load<Texture2D>("gfx/cards/frame");
            CardDecor = contentManager.Load<Texture2D>("gfx/cards/decor");
            CardDecor2 = contentManager.Load<Texture2D>("gfx/cards/decor2");
            CardHeartSymbol = contentManager.Load<Texture2D>("gfx/cards/heart");
            CardShieldSymbol = contentManager.Load<Texture2D>("gfx/cards/shield");
            CardSwordSymbol = contentManager.Load<Texture2D>("gfx/cards/sword");
            CardInfoBar = contentManager.Load<Texture2D>("gfx/cards/info_bar");
            CardStar = contentManager.Load<Texture2D>("gfx/cards/star");
            CardStarframe = contentManager.Load<Texture2D>("gfx/cards/star_frame");

            BackgroundDarkFrost = contentManager.Load<Texture2D>("gfx/scenery/background");
            DeadPileDefault = contentManager.Load<Texture2D>("gfx/cards/deadpile");

            // Fonts
            SimpleFont = contentManager.Load<SpriteFont>("simple_font");
        }

        public static void BuildCardTextures(List<CardData> cards, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            foreach (var card in cards)
            {
                var texture = contentManager.Load<Texture2D>($"gfx/cards/{card.Image}");
                CardImages.Add(card.CardId, BuildFrontTexture(spriteBatch, texture));
            }
        }

        private static Texture2D BuildFrontTexture(SpriteBatch spriteBatch, Texture2D image)
        {
            var gfx = spriteBatch.GraphicsDevice;

            var target = new RenderTarget2D(gfx, 750, 1050);

            gfx.SetRenderTarget(target);

            gfx.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(image, new Vector2(12, 32), Color.White);
            spriteBatch.Draw(CardDecor2, new Vector2(270, 760), Color.White);
            spriteBatch.Draw(CardDecor, new Vector2(63, 630), Color.White);

            spriteBatch.Draw(CardInfoBar, new Vector2(63, 830), Color.White);
            spriteBatch.Draw(CardSwordSymbol, new Vector2(290, 870), Color.White);
            spriteBatch.Draw(CardHeartSymbol, new Vector2(400, 870), Color.White);

            spriteBatch.Draw(CardFrame, new Vector2(0, 0), Color.White);

            spriteBatch.End();

            gfx.SetRenderTarget(null);

            return target;
        }
    }
}