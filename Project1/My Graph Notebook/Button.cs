using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    public class Button
    {
        string text;
        Vector2 position;
        SpriteFont font;
        Rectangle rect;
        public Rectangle Rect => rect;
        Texture2D texture;
        public Button(string text, Vector2 position, int height, int width, SpriteFont font, Texture2D texture)
        {
            rect = new Rectangle((int)position.X, (int)position.Y, width, height);
            this.text = text;
            this.position = position + new Vector2(5,20);
            this.font = font;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
            spriteBatch.DrawString(font, text, position, Color.Black);

        }
        public bool IsClicked(MouseState mouseState)
        {
            return rect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
