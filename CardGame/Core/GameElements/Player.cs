using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements
{
    public class Player
    {
        public Deck Deck { get; set; }
        public StackGroup Hand { get; set; }
        public DeadPile DeadPile { get; set; }
        public StackGroup GameArea { get; set; }

        public int Mana { get; set; }
        public int Health { get; set; }

        public Player(Deck deck) 
        {
            GameArea = new StackGroup(new Point(350, 560), 5, 0.2f, 10, StackType.DropOnly);
            Hand = new StackGroup(new Point(170, 800), 6, 0.25f);

            Deck = deck;

            DeadPile = new DeadPile(new Point(1161, 560), 0.2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameArea.Draw(spriteBatch);
            Hand.Draw(spriteBatch);
            DeadPile.Draw(spriteBatch);
            Deck.Draw(spriteBatch);
        }
    }
}
