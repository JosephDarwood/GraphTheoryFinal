using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.My_Graph_Notebook.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    internal class InputPanel
    {
        Rectangle drawBox;
        Texture2D texture;
        ContentManager contentManager;
        int panel_width;
        int panel_offset;
        int vertical_offset;
        Button addVertex;
        Button addEdge;
        Button removeVertex;
        Button removeEdge;
        ButtonFunctions functions;
        public InputPanel(ContentManager content, ButtonFunctions buttonTarget) 
        {
            vertical_offset = 50;
            contentManager = content;
            panel_width = (int)(Globals.windowWidth * .1);
            panel_offset = 0;
            drawBox = new Rectangle(panel_offset, 0, panel_width, Globals.windowHeight);
            functions = buttonTarget;
            init();
        }
        private void init()
        {
            texture = contentManager.Load<Texture2D>("panel texture");
            Vector2 yoffset = new Vector2(0, 0);
            Vector2 tempVect = new Vector2((int)(panel_width * .1)+panel_offset, vertical_offset);
            addVertex = new Button("Add vertex", tempVect + yoffset, 50, (int)(panel_width * .8), contentManager.Load<SpriteFont>("Menu"), contentManager.Load<Texture2D>("myButton"));
            yoffset.Y += 50;
            addEdge = new Button("Add edge", tempVect + yoffset, 50, (int)(panel_width * .8), contentManager.Load<SpriteFont>("Menu"), contentManager.Load<Texture2D>("myButton"));
            yoffset.Y += 50;
            removeVertex = new Button("Remove vertex", tempVect + yoffset, 50, (int)(panel_width * .8), contentManager.Load<SpriteFont>("Menu"), contentManager.Load<Texture2D>("myButton"));
            yoffset.Y += 50;
            removeEdge = new Button("Remove edge", tempVect + yoffset, 50, (int)(panel_width * .8), contentManager.Load<SpriteFont>("Menu"), contentManager.Load<Texture2D>("myButton"));
        }
        public void Update(MousePack mouse)
        {

            if (addVertex.IsClicked(mouse.currentMouse) && (mouse.previousMouse.LeftButton == ButtonState.Released))
            {
                functions.AddVertex();
            }
            if (addEdge.IsClicked(mouse.currentMouse) && (mouse.previousMouse.LeftButton == ButtonState.Released))
            {
                functions.AddEdge();
            }
            if (removeVertex.IsClicked(mouse.currentMouse) && (mouse.previousMouse.LeftButton == ButtonState.Released))
            {
                functions.RemoveVertex();
            }
            if (removeEdge.IsClicked(mouse.currentMouse) && (mouse.previousMouse.LeftButton == ButtonState.Released))
            {
                functions.RemoveEdge();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawBox, Color.Wheat);
            addVertex.Draw(spriteBatch);
            addEdge.Draw(spriteBatch);
            removeVertex.Draw(spriteBatch);
            removeEdge.Draw(spriteBatch);
            //maButton.Draw(spriteBatch);
        }
    }
}
