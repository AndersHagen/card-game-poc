using CardGame.Core.GameElements;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace CardGame.Core.GameState.Processors
{
    public class DrawProcessor
    {
        private bool _drawing;

        public ActiveCard DrawnCard { get; private set; }

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

            if (Math.Abs((DrawnCard.Card.Position - DrawnCard.TargetPosition).Length()) <= 20)
            {
                if (DrawnCard.Target is CardStack stack)
                {
                    stack.AddCard(DrawnCard.Card);
                    DrawnCard.Card.SetVelocity(Vector2.Zero);
                }
                else
                {
                    throw new Exception("Unsupported source or target for draw!");
                }

                DrawnCard = null;
                _drawing = DrawFromDeck(player);

                return _drawing == false;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            DrawnCard?.Card.Update(gameTime);
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

                DrawnCard = new ActiveCard
                {
                    Card = deckTopCard,
                    Target = stack,
                    TargetPosition = stack.Bound.Center.ToVector2()
                };

                DrawnCard.Card.SetVelocity((DrawnCard.TargetPosition- DrawnCard.Card.Position) / 50f);
                _drawing = true;

                return true;
            }

            return false;
        }
    }
}
