using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public PlayState(GameObjectManager gameObjectManager)
        {
            GameObjectManager = gameObjectManager;
            InputHandler = new InputHandler();
            _mouseHeld = false;
        }

        public void LoadContent(ContentManager contentManager)
        {
            var cardback = contentManager.Load<Texture2D>("gfx/cards/cardback1");

            CardFactory.CreateCard(GameObjectManager, cardback, new Vector2(400, 400));
            CardFactory.CreateCard(GameObjectManager, cardback, new Vector2(800, 400));
        }

        public GameCommand Update(GameTime gameTime)
        {
            var commands = InputHandler.GetInput();

            foreach (var command in commands)
            {
                if (command is ExitCommand) return command;

                if (command is MouseClickCommand)
                {
                    Debug.WriteLine(command);
                }
                if (command is MouseDraggedCommand && !_mouseHeld)
                {
                    Debug.WriteLine(command);
                    _mouseHeld = true;
                }
                if (command is MouseReleaseCommand)
                {
                    Debug.WriteLine(command);
                    _mouseHeld = false;
                }
            }


            GameObjectManager.Update(gameTime);

            return new EmptyCommand();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameObjectManager.Draw(spriteBatch);
        }
    }
}
