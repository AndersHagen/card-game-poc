﻿using CardGame.Core;
using CardGame.Core.GameState;
using CardGame.Core.Input.Commands;
using CardGame.Data;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CardGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PlayState _currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            _graphics.IsFullScreen = true;

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.Init(Content, _spriteBatch);

            AssetBuilder.BuildCardAssets(Content, _spriteBatch);

            _currentState = new PlayState();

            _currentState.LoadContent(Content, _spriteBatch) ;
        }

        protected override void Update(GameTime gameTime)
        {
            var cmd = _currentState.Update(gameTime);

            if (cmd is ExitCommand)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            _spriteBatch.Begin();

            _currentState.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
