using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    public class Graph
    {
        private List<Node> nodes;
        List<Edge> edges;
        public List<Node> Nodes => nodes;
        public List<Edge> Edges => edges;
        int idTracker;
        int labelDisplacement;
        bool NodeDrag;
        Node dragged;
        public int graphState;
        Node e1;
        Node e2;
        bool ott = true;
        public Graph()
        {
            nodes= new List<Node>();
            edges= new List<Edge>();
            idTracker= 0;
            labelDisplacement= 0;
            NodeDrag = false;
            graphState = 0;
            e1 = null;
            e2 = null;
        }
        public void RemoveEdge()
        {
            graphState = 3; e1 = null; e2= null;
        }
        public void RemoveVertex()
        {
            graphState = 2; e1 = null;
        }
        public void Update(MousePack mouse)
        {

            switch (graphState) {
                case 0: //default grpah
                    if (!NodeDrag)
                    {
                        if (mouse.previousMouse.LeftButton == ButtonState.Released && mouse.currentMouse.LeftButton == ButtonState.Pressed)
                        {
                            foreach (Node n in nodes)
                            {
                                if (n.ContainsPoint(mouse.currentMouse.Position.ToVector2()))
                                {
                                    dragged = n;
                                    NodeDrag = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mouse.currentMouse.LeftButton == ButtonState.Released)
                        {
                            NodeDrag = false;
                            dragged = null;
                        }
                        else
                        {
                            dragged.Move(mouse.currentMouse.Position.ToVector2()-new Vector2(dragged.Radius, dragged.Radius));
                        }
                    }
                    break;
                case 1: //connect edge
                    if(e1 != null && e2 != null)
                    {
                        this.MakeEdge(e1, e2);
                        e1 = null;
                        e2 = null;
                        graphState = 0;
                    }
                    else if (mouse.previousMouse.LeftButton == ButtonState.Released && mouse.currentMouse.LeftButton == ButtonState.Pressed)
                    {
                        foreach (Node n in nodes)
                        {
                            if (n.ContainsPoint(mouse.currentMouse.Position.ToVector2()))
                            {
                                if(e1 == null)
                                {
                                    e1 = n;
                                } else
                                {
                                    e2 = n;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 2:
                    if(e1 != null)
                    {
                        Expunge(e1);
                        e1 = null;
                        graphState = 0;
                    }
                    else if (mouse.previousMouse.LeftButton == ButtonState.Released && mouse.currentMouse.LeftButton == ButtonState.Pressed)
                    {
                        foreach (Node n in nodes)
                        {
                            if (n.ContainsPoint(mouse.currentMouse.Position.ToVector2()))
                            {
                                e1 = n;
                                break;
                            }
                        }
                    }
                    break;
                case 3:
                    if (e1 != null && e2 != null)
                    {
                        this.Expunge(e1, e2);
                        e1 = null;
                        e2 = null;
                        graphState = 0;
                    }
                    else if (mouse.previousMouse.LeftButton == ButtonState.Released && mouse.currentMouse.LeftButton == ButtonState.Pressed)
                    {
                        foreach (Node n in nodes)
                        {
                            if (n.ContainsPoint(mouse.currentMouse.Position.ToVector2()))
                            {
                                if (e1 == null)
                                {
                                    e1 = n;
                                }
                                else
                                {
                                    e2 = n;
                                }
                                break;
                            }
                        }
                    }
                    break;
            }
        }
        public void Draw(Canvas c)
        {
            foreach (Edge e in edges)
            {
                c.DrawEdge(e);
            }
            foreach (Node n in nodes)
            {
                c.DrawNode(n);
            }
        }
        public void AddNode(int XOffset)
        {
            idTracker++;
            string labelAttempt = "V" + idTracker.ToString();
            while(!verifyLabel(labelAttempt))
            {
                labelDisplacement++;
                labelAttempt = "V" + (idTracker+labelDisplacement).ToString();
            }
            Node b = new Node(idTracker, labelAttempt);
            b.Move(new Vector2 (XOffset, 0));
            nodes.Add(b);
        }
        public bool verifyLabel(string label)
        {
            foreach(Node n in nodes)
            {
                if (label == n.UniqueLabel)
                {
                    return false;
                }
            }
            return true; 
        }
        public void MakeEdge(Node e1, Node e2)
        {
            Edge e = new Edge(e1, e2);
            edges.Add(e);
        }
        public void Expunge(Node e1)
        {
            List<Edge> edgesToRemove = new List<Edge>();

            foreach (Edge e in Edges)
            {
                if ((e.Source.InternalID == e1.InternalID) || (e.Destination.InternalID == e1.InternalID))
                {
                    edgesToRemove.Add(e);
                }
            }
            foreach (Edge e in edgesToRemove)
            {
                Expunge(e);
            }
            nodes.Remove(e1);
        }
        public void Expunge(Edge e1)
        {
            edges.Remove(e1);
        }
        public void Expunge(Node e1, Node e2)
        {
            List<Edge> edgesToRemove = new List<Edge>();
            foreach (Edge e in Edges)
            {
                if (((e.Source.InternalID == e1.InternalID) &&(e.Destination.InternalID == e2.InternalID)) || ((e.Source.InternalID == e2.InternalID) && (e.Destination.InternalID == e1.InternalID)))
                {
                    edgesToRemove.Add(e);
                }
            }
            foreach (Edge e in edgesToRemove)
            {
                Expunge(e);
            }
        }

    }
}
