using CardGame.Core.GameElements;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameState
{
    public class PlayState
    {
        public GameObjectManager GameObjectManager;
        public InputHandler InputHandler;
        private bool _mouseHeld;

        private Card HeldCard;
        private Vector2? HeldCardPosition;

        private DeploymentArea _playerArea;

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            InputHandler = new InputHandler();
            _mouseHeld = false;
            _playerArea = new DeploymentArea();
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var front1 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardTestImage);
            var front2 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardSkeleMage);

            var c1 = CardFactory.CreateCard(GameObjectManager, front1, TextureManager.CardBack, new Vector2(400, 400));
            var c2 = CardFactory.CreateCard(GameObjectManager, front2, TextureManager.CardBack, new Vector2(800, 400));

            _playerArea.AssignCardToSlot(c1, 1);
            _playerArea.AssignCardToSlot(c2, 3);
        }

        public GameCommand Update(GameTime gameTime)
        {
            var commands = InputHandler.GetInput();

            foreach (var command in commands)
            {
                if (command is ExitCommand) return command;

                if (command is MouseClickCommand click)
                {
                    Debug.WriteLine(command);

                    var area = _playerArea.OnClick(click.X, click.Y);

                    if (area != null)
                    {
                        if (area is DeploySlot slot)
                        {
                            if (slot.DeployedCard != null && HeldCard == null)
                            {
                                HeldCard = slot.DeployedCard;
                                HeldCard.Lift();
                                HeldCardPosition = slot.DeployedCard.Position;
                                slot.PopCard();
                            }
                        }
                    }

                    foreach (var o in GameObjectManager.GetClickableGameObjects())
                    {
                        if (o.IsClicked(click.X, click.Y))
                        {
                            if (HeldCard == null && o is Card)
                            {
                                o.OnClick(click.X, click.Y);
                                var card = o as Card;
                                HeldCard = card;
                                HeldCardPosition = card.Position;
                            }
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
                        if (_playerArea.OnDrop(HeldCard, cmd.X, cmd.Y))
                        {
                            Debug.WriteLine("Card dropped in slot!");
                            HeldCard.Drop();
                        }
                        else
                        {
                            _playerArea.OnDrop(HeldCard, (int)HeldCardPosition?.X, (int)HeldCardPosition?.Y);
                            HeldCard.Drop();
                        }
                        
                        HeldCard = null;
                        HeldCardPosition = null;
                    }
                }
            }

            return new EmptyCommand();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.BackgroundDarkFrost, Vector2.Zero, null, Color.White * 0.5f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            _playerArea.Draw(spriteBatch);
            GameObjectManager.Draw(spriteBatch);
        }
    }
}
