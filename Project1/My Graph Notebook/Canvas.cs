using ColorHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Project1.My_Graph_Notebook
{
    public class Canvas
    {
        private Graph myGraph;
        Rectangle drawBox;
        Texture2D panelTexture;
        Texture2D NodeTexture;
        Texture2D edgeTexture;
        ContentManager contentManager;
        SpriteFont font;
        int Canvas_width;
        int panel_offset;
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        Vector2 center;
        public Canvas(ContentManager content, SpriteBatch sprite, GraphicsDeviceManager graphics)
        {
            contentManager = content;
            spriteBatch = sprite;
            graphicsDevice = graphics.GraphicsDevice;
            Canvas_width = (int)(Globals.windowWidth * .7);
            panel_offset = (int)(Globals.windowWidth * .1);
            drawBox = new Rectangle(panel_offset, 0, Canvas_width, Globals.windowHeight);
            center = new Vector2(panel_offset + (Canvas_width / 2), Globals.windowHeight / 2);
            myGraph = new Graph();
            init();
        }
        public void Update(MousePack mouse)
        {
            myGraph.Update(mouse);
        }
        private void init()
        {
            panelTexture = contentManager.Load<Texture2D>("canvasBackground");
            NodeTexture = contentManager.Load<Texture2D>("NodeTexture");
            edgeTexture = contentManager.Load<Texture2D>("panel texture");
            font = contentManager.Load<SpriteFont>("Menu");
        }

        internal void DrawNode(Node b)
        {
            RGB rgb = ColorHelper.ColorConverter.HexToRgb(new HEX(b.Color));
            spriteBatch.Draw(CreateCircleTexture(b.Radius, new Color(rgb.R, rgb.G, rgb.B)), b.Position, Color.White);
            Vector2 textpos = new Vector2(b.Position.X + (b.Radius / 2), b.Position.Y + (b.Radius / 2));
            spriteBatch.DrawString(font, b.UniqueLabel, textpos, Color.Black);


        }
        public void AddEdge()
        {
            myGraph.graphState=1;
        }
        internal void DrawEdge(Edge e)
        {

            DrawLine(e.Source.Center(), e.Destination.Center(), Color.White);
        }
        public void AddNode()
        {
            myGraph.AddNode(panel_offset);
        }
        public void RemoveEdge()
        {
            myGraph.RemoveEdge();
        }
        public void RemoveVertex()
        {
            myGraph.RemoveVertex();
        }
        public void Draw()
        {
            spriteBatch.Draw(panelTexture, drawBox, Color.White);
            myGraph.Draw(this);
        }
        Texture2D CreateCircleTexture(int radius, Color color)
        {
            int diameter = radius * 2;
            Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
            Color[] colorData = new Color[diameter * diameter];

            float radiussquared = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiussquared)
                        colorData[index] = color;
                    else
                        colorData[index] = Color.Transparent;
                }
            }

            texture.SetData(colorData);
            return texture;
        }
        void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            // Calculate the direction and distance between the start and end points
            Vector2 direction = end - start;
            float length = direction.Length();

            // Calculate the angle of rotation
            float angle = (float)Math.Atan2(direction.Y, direction.X);

            // Create a destination rectangle for drawing the line
            Rectangle destinationRect = new Rectangle((int)start.X, (int)start.Y, (int)length, 1);

            // Draw the line using the destination rectangle
            spriteBatch.Draw(edgeTexture, destinationRect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
        public string VertexEdgeInfo()
        {
            int nodes = myGraph.Nodes.Count;
            int edges = myGraph.Edges.Count;
            string retstr = $"Vertex: {nodes}, Edges{edges}";
            return retstr;
        }
        public string VertexDegreeInfo()
        {
            string retstr = "";
            Dictionary<Node, int> degreeMatrix = new Dictionary<Node, int>();
            foreach (Node n in myGraph.Nodes)
            {
                degreeMatrix[n] = 0;
            }
            foreach (Edge e in myGraph.Edges)
            {
                degreeMatrix[e.Source]++;
                degreeMatrix[e.Destination]++;
            }
            foreach (Node n in myGraph.Nodes)
            {
                retstr += $"ID: {n.InternalID}, Label: {n.UniqueLabel}, Degree: {degreeMatrix[n]}\n";
            }
            return retstr;
        }
        public string BridgesInfo()
        {
            string retstr = "";

            return retstr;
        }
        public string BipartiteInfo()
        {
            string retstr = "";
            if (IsBipartite())
            {
                retstr = "is bipartite";
            } else
            {
                retstr = "is not bipartite";
            }
            return retstr;
        }
        public bool IsBipartite()
        {
            Dictionary<Node, int> NodeGroups = new Dictionary<Node, int>();
            Queue<Node> q = new Queue<Node>();
            foreach (Node n in this.myGraph.Nodes) //for each node
            {
                if (!NodeGroups.ContainsKey(n)) //if unknown
                {
                    NodeGroups[n] = 0; //mark as 1st group (0st group?)
                    q.Enqueue(n); //put in list of nodes to work through
                    while (q.Count > 0) //work through current que 
                    {
                        Node current = q.Dequeue();
                        foreach (Edge e in this.myGraph.Edges) 
                        {
                            Node x = null;
                            if (e.Source == current)
                            {
                                x = e.Destination;
                            }
                            else if (e.Destination == current) 
                            { 
                                x = e.Source; 
                            }
                            if (x != null)
                            {
                                if (!NodeGroups.ContainsKey(x)) //If node not worked thorugh add it to the que as node of different group
                                {
                                    NodeGroups[x] = 1 - NodeGroups[current];
                                    q.Enqueue(x);
                                }
                                else if (NodeGroups[x] == NodeGroups[current]) //if it was worked through and in the smae group, you cant be bipartite. 
                                {
                                    return false;
                                }
                            }
                            
                        }
                    }
                }
            }
            return true;
        }

    }
}
