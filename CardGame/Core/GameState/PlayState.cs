using CardGame.Core.GameElements;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGame.Core.GameState
{
    public class PlayState
    {
        public GameObjectManager GameObjectManager;
        public InputHandler InputHandler;
        private bool _mouseHeld;

        private Card HeldCard;
        private Vector2? HeldCardPosition;
        private CardStack _lastStack;

        private StackGroup _playerArea;
        private StackGroup _playerHand;
        private StackGroup _playerDeck;

        private List<StackGroup> _playerStacks;

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            InputHandler = new InputHandler();
            _mouseHeld = false;
            _playerArea = new StackGroup(new Point(350, 560), 5, 0.2f, 10, StackType.DropOnly);
            _playerHand = new StackGroup(new Point(170, 800), 6, 0.25f);
            _playerDeck = new StackGroup(new Point(1400, 800), 1, 0.25f, 10, StackType.PickupOnly, 10, false);

            _playerStacks = new List<StackGroup>() 
            {
                _playerArea,
                _playerHand,
                _playerDeck,
            };
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var front1 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardTestImage);
            var front2 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardSkeleMage);
            var front3 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardBoneGolem);
            var front4 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardSkeletalWarlord);
            var front5 = CardFactory.BuildFrontTexture(spriteBatch, TextureManager.CardSwampZombie);

            var c1 = CardFactory.CreateCard(GameObjectManager, front1, TextureManager.CardBack, new Vector2(400, 400));
            var c2 = CardFactory.CreateCard(GameObjectManager, front2, TextureManager.CardBack, new Vector2(800, 400));
            var c3 = CardFactory.CreateCard(GameObjectManager, front3, TextureManager.CardBack, new Vector2(400, 400));
            var c4 = CardFactory.CreateCard(GameObjectManager, front4, TextureManager.CardBack, new Vector2(800, 400));
            var c5 = CardFactory.CreateCard(GameObjectManager, front5, TextureManager.CardBack, new Vector2(400, 400));

            var c6 = CardFactory.CreateCard(GameObjectManager, front1, TextureManager.CardBack, new Vector2(400, 400));
            var c7 = CardFactory.CreateCard(GameObjectManager, front2, TextureManager.CardBack, new Vector2(800, 400));
            var c8 = CardFactory.CreateCard(GameObjectManager, front3, TextureManager.CardBack, new Vector2(400, 400));
            var c9 = CardFactory.CreateCard(GameObjectManager, front4, TextureManager.CardBack, new Vector2(800, 400));
            var c10 = CardFactory.CreateCard(GameObjectManager, front5, TextureManager.CardBack, new Vector2(400, 400));

            _playerDeck.AssignCardToSlot(c1, 0);
            _playerDeck.AssignCardToSlot(c2, 0);
            _playerDeck.AssignCardToSlot(c3, 0);
            _playerDeck.AssignCardToSlot(c4, 0);
            _playerDeck.AssignCardToSlot(c5, 0);
            _playerDeck.AssignCardToSlot(c6, 0);
            _playerDeck.AssignCardToSlot(c7, 0);
            _playerDeck.AssignCardToSlot(c8, 0);
            _playerDeck.AssignCardToSlot(c9, 0);
            _playerDeck.AssignCardToSlot(c10, 0);

            _playerDeck.Shuffle(0);
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

                    var clickedStack = CheckIfStackClicked(click);

                    if (clickedStack is CardStack stack)
                    {
                        var topCard = stack.GetTopCard();

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

            GameObjectManager.Update(gameTime);

            return new EmptyCommand();
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
        }
    }
}
