using CardGame.Core.GameElements;
using CardGame.Core.GameElements.GameCards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameState.Processors
{
    public class DrawProcessor
    {
        private bool _drawing;

        public Card DrawnCard { get; private set; }
        private Vector2? DrawnCardPosition;
        private CardStack DrawnCardTarget;
        private Vector2? DrawnCardTargetPosition;

        public DrawProcessor()
        {
            _drawing = false;
        }

        public bool ProcessDrawFromDeck(Player player)
        {
            if (!_drawing)
            {
                return DrawFromDeck(player) == false;
            }

            if (Math.Abs((DrawnCard.Position - DrawnCardTargetPosition.Value).Length()) <= 20)
            {
                DrawnCardTarget.AddCard(DrawnCard);
                DrawnCard.SetVelocity(Vector2.Zero);
                DrawnCard = null;
                DrawnCardPosition = null;
                DrawnCardTarget = null;
                DrawnCardTargetPosition = null;
                _drawing = DrawFromDeck(player);

                return _drawing == false;
            }

            return false;
        }

        private bool DrawFromDeck(Player player)
        {
            var target = player.Hand;
            var deck = player.Deck;

            var emptyStacks = target.GetEmptyStacks();

            if (emptyStacks.Count > 0 && deck.TopCard != null)
            {
                var stack = emptyStacks.First();
                var deckTopCard = deck.GetTopCard();

                DrawnCard = deckTopCard;
                DrawnCardPosition = deckTopCard.Position;
                DrawnCardTarget = stack;
                DrawnCardTargetPosition = stack.Bound.Center.ToVector2();

                DrawnCard.SetVelocity((DrawnCardTargetPosition.Value - DrawnCardPosition.Value) / 50f);
                _drawing = true;

                return true;
            }

            return false;
        }
    }
}
