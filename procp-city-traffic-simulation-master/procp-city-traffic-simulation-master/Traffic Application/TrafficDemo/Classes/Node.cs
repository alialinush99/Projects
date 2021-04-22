using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficDemo.Classes
{
    public class Node
    {
        private Point location;
        public bool Visited { get; set; }

        public Node NearestToStart { get; set; }
        public List<Edge> Connections { get; set; } = new List<Edge>();

        public double? MinCostToStart { get; set; }
        public double StraightLineDistanceToEnd { get; set; }
        public Guid Id { get; set; }
        public Lane parentLane { get; set; }

        public string Name { get; set; } = "";
        public Point Location { get => location; set => location = value; }

        public Node(Point locationOnGrid)
        {
            this.Location = locationOnGrid;
            Id = Guid.NewGuid();

        }

        public double StraightLineDistanceTo(Node end)
        {
            return Math.Sqrt(Math.Pow(Location.X - end.Location.X, 2) + Math.Pow(Location.Y - end.Location.Y, 2));
        }

        public void ConnectClosestNodes(List<Node> nodes, int branching)
        {
            var connections = new List<Edge>();
            foreach (var node in nodes)
            {
                if (node.Id == this.Id)
                    continue;

                var dist = Math.Sqrt(Math.Pow(Location.X - node.Location.X, 2) + Math.Pow(Location.Y - node.Location.Y, 2));
                int weightedCost = 0;
                switch(this.Name){
                    case "StartPoint":
                        if (node.Name == "StartPoint")
                        {
                            weightedCost += 100;
                        }
                        if (node.Name == "EndPoint")
                        {
                            weightedCost += 100;
                        }
                        break;
                    case "EndPoint":
                        if (node.Name == "StartPoint")
                        {
                            weightedCost += 100;
                        }
                        if (node.Name == "EndPoint")
                        {
                            weightedCost += 100;
                        }
                        break;
                    case "Edge":
                        if (node.Name == "StartPoint")
                        {
                            weightedCost += 100;
                        }
                        if (node.Name == "EndPoint")
                        {
                            weightedCost += 100;
                        }
                        break;
                }
                connections.Add(new Edge
                {
                    ConnectedNode = node,
                    Length = dist,
                    Cost = dist + weightedCost,
                });
            }
            connections = connections.OrderBy(x => x.Length).ToList();
            var count = 0;
            foreach (var cnn in connections)
            {
                //Connect three closes nodes that are not connected.
                if (!Connections.Any(c => c.ConnectedNode == cnn.ConnectedNode))
                    Connections.Add(cnn);
                count++;

                //Make it a two way connection if not already connected
                if (!cnn.ConnectedNode.Connections.Any(cc => cc.ConnectedNode == this))
                {
                    var backConnection = new Edge { ConnectedNode = this, Length = cnn.Length };
                    cnn.ConnectedNode.Connections.Add(backConnection);
                }
                if (count == branching)
                    return;
            }
        }


 



    }
}
