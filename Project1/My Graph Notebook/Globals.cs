using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    public class Globals 
    {
        public static readonly int windowHeight = 720;
        public static readonly int windowWidth = 1280;
        public static Vector2 Center()
        {
            return new Vector2(windowHeight / 2, windowWidth / 2);
        }
    }
}
