using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
     public class Edge
    {
        public double Length { get; set; }
        public double Cost { get; set; }
        public Node ConnectedNode { get; set; }

    }
}