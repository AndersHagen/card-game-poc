using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements
{
    public class CardDeck : StackGroup
    {
        private CardStack Stack => _stacks[0];

        public Card TopCard => Stack.TopCard;

        public CardDeck(
            Point location, 
            int stacks, 
            float scale, 
            int padding = 10, 
            StackType slotType = StackType.Regular, 
            int stackSize = 1, 
            bool isFaceUp = true
        ) : base(location, stacks, scale, padding, slotType, stackSize, isFaceUp)
        {
        }

        public void Shuffle()
        {
            Stack.Shuffle();
        }

        public Card GetTopCard()
        {
            var card = Stack.GetTopCard();

            if (card != null)
            {
                Stack.PopCard();
            }

            return card;
        }
    }
}
