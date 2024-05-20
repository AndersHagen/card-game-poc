using CardGame.Core.GameElements;
using CardGame.Core.GameElements.GameCards;
using CardGame.Core.GameState.Processors;
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

        private DrawProcessor _drawProcessor;

        private TurnManager _turnManager;

        private bool _dealingFromDeck;

        private Player _player;

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            _turnManager = new TurnManager();
            InputHandler = new InputHandler();
            _drawProcessor = new DrawProcessor();
            _mouseHeld = false;

            _dealingFromDeck = false;
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var deck = new Deck(new Point(1400, 800), TextureManager.CardBack, 30, 0.25f);

            for (var i = 0; i < 16; i++)
            {
                var id = (CardId)(i % 4);

                var card = CardFactory.CreateCard(GameObjectManager, id, TextureManager.CardBack);
                deck.AddCard(card);
            }

            deck.Shuffle();

            _player = new Player(deck);
        }

        public GameCommand Update(GameTime gameTime)
        {
            var commands = InputHandler.GetInput();

            if (commands.Any(c => c is ExitCommand))
            {
                return new ExitCommand();
            }

            switch (_turnManager.CurrentState)
            {
                case TurnState.Start:
                    if (ProcessStartOfTurn())
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Refresh:
                    if (ProcessRefresh())
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Sacrifice:
                    if (ProcessSacrifice(commands))
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Deployment:
                    if (ProcessDeployment(commands))
                    {
                        _turnManager?.ProgressToNextState();
                    }
                    break;
                case TurnState.Attack:
                    if (ProcessAttack())
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Draw:
                    if (_drawProcessor.ProcessDrawFromDeck(_player))
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Cleanup:
                    if (ProcessCleanup())
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.End:
                    if (ProcessEndOfTurn())
                    {
                        _turnManager?.ProgressToNextState();
                    }
                    break;
            }

            GameObjectManager.Update(gameTime);

            return new EmptyCommand();
        }

        private bool ProcessEndOfTurn()
        {
            return true;
        }

        private bool ProcessCleanup()
        {
            return true;
        }

        private bool ProcessAttack()
        {
            return true;
        }

        private bool ProcessDeployment(List<GameCommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is EndStepCommand)
                {
                    return true;
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
                        var dropTarget = _player.GameArea.OnDrop(HeldCard, cmd.X, cmd.Y) ?? _player.Hand.OnDrop(HeldCard, cmd.X, cmd.Y);

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
            return false;
        }

        private bool ProcessSacrifice(List<GameCommand> commands)
        {
            return true;
        }

        private bool ProcessStartOfTurn()
        {
            return true;
        }

        private bool ProcessRefresh()
        {
            return true;
        }

        private IClickable CheckIfStackClicked(MouseClickCommand click)
        {
            var playerStacks = new List<StackGroup> { _player.Hand, _player.GameArea };

            foreach (var stack in playerStacks)
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

            _player.Draw(spriteBatch);

            GameObjectManager.Draw(spriteBatch);
            if (HeldCard != null)
            {
                HeldCard.Draw(spriteBatch);
            }

            if (_drawProcessor.DrawnCard != null)
            {
                _drawProcessor.DrawnCard.Draw(spriteBatch);
            }
        }
    }
}
