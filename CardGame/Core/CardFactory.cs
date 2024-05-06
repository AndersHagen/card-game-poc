using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public static class CardFactory
    {
        public static Card CreateCard(GameObjectManager graphicsManager, Texture2D texture, Vector2 position)
        {
            var card = new Card(texture, position, new Vector2(0.5f, 0.5f));
            graphicsManager.RegisterObject(card);
            return card;
        }
    }
}
