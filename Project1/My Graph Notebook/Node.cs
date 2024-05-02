using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    public class Node
    {
        private int internalid;
        private string uniquelabel; 
        private int radius;
        private string color;
        private Vector2 position;
        public Vector2 Position => position;
        public string Color => color;
        public int Radius => radius;
        public int InternalID => internalid;
        public string UniqueLabel => uniquelabel;
        public Node(int uid, string uniquelabel) 
        {
            this.uniquelabel = uniquelabel;
            internalid = uid;
            init();
            this.color = "#FFC0CB";
        }
        private void init()
        {
            position = new Vector2(0, 0);
            radius = 20;


        }
        public void Move(Vector2 newpos)
        {
            position = newpos;
        }
        public bool ContainsPoint(Vector2 point)
        {

            return Vector2.Distance(Center(), point) <= Radius;
        }
        public Vector2 Center()
        {
            return new Vector2(position.X+this.radius, position.Y+ this.radius);
        }
    }
}
