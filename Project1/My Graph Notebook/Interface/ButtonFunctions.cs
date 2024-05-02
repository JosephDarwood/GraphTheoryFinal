using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook.Interface
{
    interface ButtonFunctions
    {
        public void AddVertex();
        public void AddEdge();
        public void RemoveVertex();
        public void RemoveEdge();
        public void RenameVertex(string destination, string source);
    }
}
