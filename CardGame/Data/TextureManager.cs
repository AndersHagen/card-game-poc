using CardGame.Core;
using CardGame.Core.GameElements.GameCards;
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

            BuildCardTextures(new List<CardData>(), contentManager, spriteBatch);
        }

        private static void BuildCardTextures(List<CardData> cards, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            using (var s = new StreamReader("Data/cards.json"))
            {
                var content = s.ReadToEnd();
                cards = JsonSerializer.Deserialize<List<CardData>>(content);
            }

            foreach (var card in cards)
            {
                var texture = contentManager.Load<Texture2D>($"gfx/cards/{card.Image}");
                CardImages.Add(card.CardId, CardFactory.BuildFrontTexture(spriteBatch, texture));
            }
        }
    }
}