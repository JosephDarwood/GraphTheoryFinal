using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.My_Graph_Notebook.Interface
{
    interface IGraphQueries
    {
        public string VertexEdgeInfo();
        public string VertexDegreeInfo();
        public string BridgesInfo();
        public string BipartiteInfo();
    }
}
