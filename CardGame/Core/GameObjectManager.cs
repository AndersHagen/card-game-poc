using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public class GameObjectManager
    {
        private List<GameObject> _gameObjects;

        public GameObjectManager()
        {
            _gameObjects = new List<GameObject>();
        }
        
        public void RegisterObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
