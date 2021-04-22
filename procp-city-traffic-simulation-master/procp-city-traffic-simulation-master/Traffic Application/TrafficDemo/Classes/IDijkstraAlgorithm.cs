using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace TrafficDemo.Classes
{
    interface IDijkstraAlgorithm
    {
        List<Node> GetShortesPathDijkstra();
        void BuildShortestPath(List<Node> list, Node node);
        void DijkstraSearch();
    }
}
