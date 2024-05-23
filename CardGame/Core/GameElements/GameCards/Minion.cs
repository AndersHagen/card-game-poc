using CardGame.Data;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGame.Core.GameElements.GameCards
{
    public class Minion : Card
    {
        public int Attack { get; private set; }
        public int Health { get; private set; }

        private List<Aura> _auras;

        private CardText _attackText;
        private CardText _healthText;

        public Minion(Texture2D front, Texture2D back, Vector2 position, float scale, int attack, int health) : base(front, back, position, scale)
        {
            Attack = attack;
            Health = health;

            _auras = new List<Aura>();
            _attackText = new CardText(TextureManager.SimpleFont, Attack.ToString(), new Vector2(-220, 335));
            _healthText = new CardText(TextureManager.SimpleFont, Health.ToString(), new Vector2(170, 335));
        }

        public void AddAuras(List<Aura> auras) 
        {
            _auras.AddRange(auras);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsFaceUp) 
            { 
                _attackText.Draw(spriteBatch, Position, Scale);
                _healthText.Draw(spriteBatch, Position, Scale);
            }
        }

        private void ApplyAuras()
        {
            Attack = 0;
            Health = 0;

            foreach (var aura in _auras)
            {
                if (aura is ModifyAttackAura modAtkAura)
                {
                    Attack += modAtkAura.Modifier;
                }

                if (aura is ModifyHealthAura modHltAura)
                {
                    Health += modHltAura.Modifier;
                }
            }
        }
    }
}
