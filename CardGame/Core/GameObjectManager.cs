using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public class GameObjectManager
    {
        private List<Card> _cards;

        public GameObjectManager()
        {
            _cards = new List<Card>();
        }
        
        public void RegisterObject(Card card)
        {
            _cards.Add(card);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var card in _cards)
            {
                card.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var card in _cards)
            {
                card.Draw(spriteBatch);
            }
        }
    }
}
