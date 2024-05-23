using CardGame.Core.GameElements.GameCards;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CardGame.Core
{
    public static class CardFactory
    {
        public static Dictionary<CardId, CardInfo> _infoById = new Dictionary<CardId, CardInfo>();

        public static Card CreateCard(CardId id, Texture2D back)
        {
            var front = TextureManager.CardImages[id];

            Card card = null;
            var info = _infoById[id];

            switch (info.CardType)
            {
                case "Minion":
                    var minion = new Minion(front, back, Vector2.Zero, 1f, info.Attack, info.Health);
                    minion.AddAuras(info.Auras);
                    return minion;
            }

            return card;
        }

        public static void RegisterCardInfoForId(CardInfo info)
        {
            _infoById.Add(info.CardId, info);
        }
    }
}
