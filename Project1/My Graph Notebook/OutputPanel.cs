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
using static System.Net.Mime.MediaTypeNames;

namespace Project1.My_Graph_Notebook
{
    internal class OutputPanel
    {
        Rectangle drawBox;
        Texture2D texture;
        ContentManager contentManager;
        int panel_width;
        int panel_offset;
        string outText = "";
        Button cycleOutput;
        int whichOutput;
        SpriteFont font;
        IGraphQueries mygraph;
        public OutputPanel(ContentManager content, IGraphQueries graph)
        {
            contentManager = content;
            panel_width = (int)(Globals.windowWidth * .2);
            panel_offset = (int)(Globals.windowWidth * .8);
            drawBox = new Rectangle(panel_offset, 0, panel_width, Globals.windowHeight);
            whichOutput = 0;
            mygraph = graph;
            init();
        }
        private void init()
        {
            texture = contentManager.Load<Texture2D>("panel texture");
            font = contentManager.Load<SpriteFont>("Menu");
            cycleOutput = new Button("Cycle Output", new Vector2(panel_offset + ((1 / 8) * (panel_width)), 1 / 8 * Globals.windowHeight), 50, (int)(panel_width * .8), contentManager.Load<SpriteFont>("Menu"), contentManager.Load<Texture2D>("myButton"));

        }
        public void Update(MousePack mouse)
        {
            if (cycleOutput.IsClicked(mouse.currentMouse) && (mouse.previousMouse.LeftButton == ButtonState.Released))
            {
                whichOutput++;
            }
            switch(whichOutput)
            {
                case 0:
                    this.outText = this.mygraph.VertexEdgeInfo();
                    break;
                case 1:
                    this.outText = this.mygraph.VertexDegreeInfo();
                    break;
                case 2:
                    this.outText = this.mygraph.BridgesInfo();
                    break;
                case 3:
                    this.outText = this.mygraph.BipartiteInfo();
                    break;
                default:
                    whichOutput = 0;
                    break;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawBox, Color.Wheat);
            cycleOutput.Draw(spriteBatch);
            spriteBatch.DrawString(font, WhatData(this.whichOutput), new Vector2(panel_offset+20, 0+50), Color.White);
            spriteBatch.DrawString(font, this.outText, new Vector2(panel_offset + 20, 0 + 100), Color.White);

            //maButton.Draw(spriteBatch);

        }
        public string WhatData(int i)
        {
            string retstr = "";
            switch (i)
            {
                case 0:
                    retstr = "Vertex and Edge Info";
                    break;
                case 1:
                    retstr = "Vertex Degree Info";
                    break;
                case 2:
                    retstr = "Bridge Info";
                    break;
                case 3:
                    retstr = "Bipartite Info";
                    break;
            }
            return retstr;
        }
    }
}
