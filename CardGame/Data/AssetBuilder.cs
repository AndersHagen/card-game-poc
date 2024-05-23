using CardGame.Core;
using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CardGame.Data
{
    public static class AssetBuilder
    {
        public static void BuildCardAssets(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var cardData = new List<CardData>();

            using (var s = new StreamReader("Data/cards.json"))
            {
                var content = s.ReadToEnd();
                cardData = JsonSerializer.Deserialize<List<CardData>>(content);
            }

            TextureManager.BuildCardTextures(cardData, contentManager, spriteBatch);

            foreach (var card in cardData)
            {
                var auras = ParseAuraString(card.Auras);
                CardFactory.RegisterCardInfoForId(new CardInfo(card, auras));
            }
        }

        private static List<Aura> ParseAuraString(string aurastring)
        {
            var auras = new List<Aura>();

            var defs = aurastring.Split(';');

            foreach (var def in defs)
            {
                var parts = def.Split(':');

                switch (parts[0])
                {
                    case "Atk":
                        auras.Add(new ModifyAttackAura(int.Parse(parts[1])));
                        break;
                    case "Hlt":
                        auras.Add(new ModifyHealthAura(int.Parse(parts[1])));
                        break;
                }
            }

            return auras;
        }
    }
}
