using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook
{
    public class Edge
    {
        Node source;
        public Node Source => source;
        Node destination;
        public Node Destination => destination;
        int amount;
        public Edge(Node _1, Node _2) 
        {
            source = _1;
            destination = _2;
        }
    }
}
