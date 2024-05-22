using CardGame.Core.GameElements;
using CardGame.Core.GameElements.GameCards;
using CardGame.Core.GameState.Processors;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.GameState
{
    public class PlayState
    {
        public InputHandler InputHandler;

        private DrawProcessor _drawProcessor;
        private DeploymentProcessor _deploymentProcessor;
        private SacrificeProcessor _sacrificeProcessor;

        private TurnManager _turnManager;

        private Player _player;

        public PlayState()
        {
            _turnManager = new TurnManager();
            InputHandler = new InputHandler();
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var deck = new Deck(new Point(1400, 800), TextureManager.CardBack, 30, 0.25f);

            for (var i = 0; i < 16; i++)
            {
                var id = (CardId)(i % 4);

                var card = CardFactory.CreateCard(id, TextureManager.CardBack);
                deck.AddCard(card);
            }

            deck.Shuffle();

            _player = new Player(deck);

            _sacrificeProcessor = new SacrificeProcessor(_player);
            _drawProcessor = new DrawProcessor();
            _deploymentProcessor = new DeploymentProcessor(_player);

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
                    if (_sacrificeProcessor.ProcessSacrifice(commands))
                    {
                        _turnManager.ProgressToNextState();
                    }
                    break;
                case TurnState.Deployment:
                    if (_deploymentProcessor.ProcessDeployment(_player, commands))
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
                    else
                    {
                        _drawProcessor.Update(gameTime);
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

            _player.Update(gameTime);

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

        private bool ProcessStartOfTurn()
        {
            return true;
        }

        private bool ProcessRefresh()
        {
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.BackgroundDarkFrost, Vector2.Zero, null, Color.White * 0.5f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            _player.Draw(spriteBatch);

            var activeCard = _sacrificeProcessor.HeldCard ?? _deploymentProcessor.HeldCard;

            if (activeCard != null)
            {
                spriteBatch.Draw(
                    activeCard.Texture,
                    activeCard.Position,
                    null,
                    Color.White,
                    0f,
                    activeCard.Center,
                    activeCard.Scale,
                    SpriteEffects.None,
                    0
                );
            }

            if (_drawProcessor.DrawnCard != null)
            {
                _drawProcessor.DrawnCard.Card.Draw(spriteBatch);
            }
        }
    }
}
