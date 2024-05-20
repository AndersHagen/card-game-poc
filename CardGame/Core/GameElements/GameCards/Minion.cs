using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CardGame.Core.GameElements.GameCards
{
    public class Minion : Card
    {
        public int Attack { get; private set; }
        public int Health { get; private set; }

        private List<Aura> _auras;

        public Minion(Texture2D front, Texture2D back, Vector2 position, float scale) : base(front, back, position, scale)
        {
            Attack = 0;
            Health = 0;
            _auras = new List<Aura>();
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
