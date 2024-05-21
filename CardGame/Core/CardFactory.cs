using CardGame.Core.GameElements.GameCards;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CardGame.Core
{
    public static class CardFactory
    {
        public static Dictionary<CardId, List<Aura>> _startingAuras = new Dictionary<CardId, List<Aura>>();
        public static Dictionary<CardId, string> _typeById = new Dictionary<CardId, string>();

        public static Card CreateCard(GameObjectManager graphicsManager, CardId id, Texture2D back)
        {
            var front = TextureManager.CardImages[id];

            Card card = null;

            switch (_typeById[id])
            {
                case "Minion":
                    var minion = new Minion(front, back, Vector2.Zero, 1f);
                    minion.AddAuras(_startingAuras[id]);
                    return minion;
            }

            return card;
        }

        public static void RegisterAurasForId(CardId id, List<Aura> auras) 
        {
            _startingAuras.Add(id, auras);
        }

        public static void RegisterTypeForId(CardId id, string type)
        {
            _typeById.Add(id, type);
        }
    }
}
