using CardGame.Core.GameElements;
using CardGame.Core.GameElements.GameCards;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CardGame.Core.GameState
{
    public class PlayState
    {
        public GameObjectManager GameObjectManager;
        public InputHandler InputHandler;
        private bool _mouseHeld;

        private Card HeldCard;
        private Card DealtCard;
        private Vector2? HeldCardPosition;
        private Vector2? DealtCardPosition;
        private CardStack DealtCardTarget;
        private Vector2? DealtCardTargetPosition;
        private CardStack _lastStack;

        private StackGroup _playerArea;
        private StackGroup _playerHand;

        private Deck _deck;

        private List<StackGroup> _playerStacks;

        private bool _dealingFromDeck;

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            InputHandler = new InputHandler();
            _mouseHeld = false;
            _playerArea = new StackGroup(new Point(350, 560), 5, 0.2f, 10, StackType.DropOnly);
            _playerHand = new StackGroup(new Point(170, 800), 6, 0.25f);

            _deck = new Deck(new Point(1400, 700), TextureManager.CardBack, 30, 0.25f);

            _playerStacks = new List<StackGroup>() 
            {
                _playerArea,
                _playerHand,
            };

            _dealingFromDeck = false;
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            for (var i = 0; i < 16; i++)
            {
                var id = (CardId)(i % 4);

                var card = CardFactory.CreateCard(GameObjectManager, id, TextureManager.CardBack);
                _deck.AddCard(card);
            }

            _deck.Shuffle();
        }

        public GameCommand Update(GameTime gameTime)
        {
            var commands = InputHandler.GetInput();

            if (commands.Any(c => c is ExitCommand))
            {
                return new ExitCommand();
            }

            if (_dealingFromDeck)
            {
                if (Math.Abs((DealtCard.Position - DealtCardTargetPosition.Value).Length()) <= 20)
                {
                    DealtCardTarget.AddCard(DealtCard);
                    DealtCard.SetVelocity(Vector2.Zero);
                    DealtCard = null;
                    DealtCardPosition = null;
                    DealtCardTarget = null;
                    DealtCardTargetPosition = null;
                    _dealingFromDeck = DrawFromDeck(_playerHand, _deck);
                }
            }
            else
            {
                foreach (var command in commands)
                {
                    if (command is DrawCommand)
                    {
                        DrawFromDeck(_playerHand, _deck);
                    }

                    if (command is MouseClickCommand click)
                    {
                        Debug.WriteLine(command);

                        var clickedStack = CheckIfStackClicked(click);

                        if (clickedStack is CardStack stack)
                        {
                            var topCard = stack.PeekTopCard();

                            if (topCard != null && HeldCard == null)
                            {
                                HeldCard = topCard;
                                HeldCard.Lift();
                                HeldCardPosition = topCard.Position;
                                _lastStack = stack;
                                stack.PopCard();
                            }
                        }
                    }
                    if (command is MouseDraggedCommand)
                    {
                        if (!_mouseHeld)
                        {
                            Debug.WriteLine(command);
                            _mouseHeld = true;
                        }

                        if (HeldCard != null)
                        {
                            HeldCard.OnDrag((command as MouseDraggedCommand).X, (command as MouseDraggedCommand).Y);
                        }

                    }
                    if (command is MouseReleaseCommand cmd)
                    {
                        Debug.WriteLine(cmd);
                        _mouseHeld = false;

                        if (HeldCard != null)
                        {
                            var dropTarget = _playerArea.OnDrop(HeldCard, cmd.X, cmd.Y) ?? _playerHand.OnDrop(HeldCard, cmd.X, cmd.Y);

                            if (dropTarget is CardStack slot)
                            {
                                HeldCard.Drop(slot);
                            }
                            else
                            {
                                _lastStack.OnDrop(HeldCard, (int)HeldCardPosition?.X, (int)HeldCardPosition?.Y, true);
                                HeldCard.Drop(_lastStack);
                            }

                            HeldCard = null;
                            HeldCardPosition = null;
                            _lastStack = null;
                        }
                    }

                }
            }

            GameObjectManager.Update(gameTime);

            return new EmptyCommand();
        }

        private bool DrawFromDeck(StackGroup target, Deck deck)
        {
            var emptyStacks = target.GetEmptyStacks();

            if (emptyStacks.Count > 0 && deck.TopCard != null)
            {
                var stack = emptyStacks.First();
                var deckTopCard = deck.GetTopCard();

                DealtCard = deckTopCard;
                DealtCardPosition = deckTopCard.Position;
                DealtCardTarget = stack;
                DealtCardTargetPosition = stack.Bound.Center.ToVector2();

                DealtCard.SetVelocity((DealtCardTargetPosition.Value - DealtCardPosition.Value) / 50f);
                _dealingFromDeck = true;

                return true;
            }

            return false;
        }

        private IClickable CheckIfStackClicked(MouseClickCommand click)
        {
            foreach (var stack in _playerStacks)
            {
                var stackClicked = stack.OnClick(click.X, click.Y);

                if (stackClicked != null)
                {
                    return stackClicked;
                }
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.BackgroundDarkFrost, Vector2.Zero, null, Color.White * 0.5f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            foreach (var stack in _playerStacks)
            {
                stack.Draw(spriteBatch);
            }

            GameObjectManager.Draw(spriteBatch);
            if (HeldCard != null)
            {
                HeldCard.Draw(spriteBatch);
            }

            if (DealtCard != null)
            {
                DealtCard.Draw(spriteBatch);
            }

            _deck.Draw(spriteBatch);
        }
    }
}
