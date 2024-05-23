using CardGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements.GameCards
{
    public class CardText
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }

        public Vector2 Offset { get; set; }
        public Vector2 Center { get; set; }


        public CardText(SpriteFont font, string text, Vector2 offset)
        {
            Text = text;
            Font = font;
            SetCenter();
            Offset = offset;
        }

        private void SetCenter()
        {
            var dims = Font.MeasureString(Text);
            Center = dims / 2f;
        }

        public void SetText(string text)
        {
            Text = text;
            SetCenter();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 parentPosition, float parentScale)
        {
            spriteBatch.DrawString(
                Font,
                Text,
                parentPosition + Offset * parentScale,
                Color.LightGoldenrodYellow,
                0f,
                Center * parentScale,
                parentScale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
