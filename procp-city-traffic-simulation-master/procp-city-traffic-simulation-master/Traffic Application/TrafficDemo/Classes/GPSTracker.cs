using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace TrafficDemo.Classes
{
    public class GPSTracker : IDijkstraAlgorithm, IAStarAlgorithm
    {
        private Node end;
        private Node start;
        private List<Node> shortestPath;
        private List<Node> nodes;
        private Grid grid;
        private string algorithm;

        public Node End { get => end; set => end = value; }
        public Node Start { get => start; set => start = value; }

        public List<Node> ShortestPath { get => shortestPath; set => shortestPath = value; }
        public List<Node> Nodes { get => nodes; set => nodes = value; }

        public int NodeVisits { get; private set; }
        public double ShortestPathLength { get; set; }
        public double ShortestPathCost { get; private set; }
        public string Algorithm { get => algorithm; set => algorithm = value; }


        public GPSTracker(Grid Grid, Point startPoint, Point endPoint, Lane endpointLane, string algorithm)
        {
            shortestPath = new List<Node>();
            this.grid = Grid;
            nodes = new List<Node>();
            this.algorithm = algorithm;
            this.end = new Node(endPoint);
            this.end.parentLane = endpointLane;
            nodes.Add(this.end);

            this.start = new Node(startPoint);
            int distanceToStart = 0;
            foreach(KeyValuePair<Lane, List<Point>> kvp2 in this.grid.GetAllPointsOnGrid())
            {
                foreach(Point P in kvp2.Value)
                {
                    int distance = ((start.Location.X - P.X) * (start.Location.X - P.X) + (start.Location.Y - P.Y) * (start.Location.Y - P.Y));
                    if ((distanceToStart == 0) || (distance >= distanceToStart))
                    {
                        distanceToStart = distance;
                        start.parentLane = kvp2.Key; 

                    }

                    
                }
            }
            nodes.Add(this.start);

            foreach (KeyValuePair<Lane, List<Point>> KVP in this.grid.GetAllPointsOnGrid())
            {
                foreach (Point P in KVP.Value) {
                    Node newNode = new Node(P);
                    newNode.parentLane = KVP.Key;
                    // Find out what type of point it is so we can adjust its weight later 

                    if (P == KVP.Key.StartPointOnScreen)
                    {
                        newNode.Name = "StartPoint";
                    }
                    foreach(KeyValuePair<int, Point> KVP2 in KVP.Key.EndPointsOnScreen)
                    {
                       if (P == KVP2.Value)
                        {
                            newNode.Name = "EndPoint";
                        }

                    }
                    foreach(Point edge in KVP.Key.EdgesOnScreen)
                    {
                        if (P == edge)
                        {
                            newNode.Name = "Edge";
                        }
                    }
                    nodes.Add(newNode);
                }
            }

            foreach (Node N in this.nodes)
            {
                if (N.Location == startPoint)
                {
                    this.start = N;
                }

            }


            // remove duplicates 

            for (int g = 0; g < nodes.Count; g++)
            {
                Node N1 = this.nodes[g];

                for (int i = 0; i < nodes.Count; i++)
                {
                    Node N2 = this.nodes[i];
                    if ((N1 != N2 && N1.Location == N2.Location) && N2 != this.start)
                    {
                        this.nodes.Remove(N2);
                    }
                }

            }



            foreach (Node Final in this.nodes)
            {
                Final.ConnectClosestNodes(nodes, 5);
            }

            



        }

        public GPSTracker()
        {
            nodes = new List<Node>();
        }

        public List<Node> GetShortesPathDijkstra()
        {
            DijkstraSearch();
            var shortestPath = new List<Node>();
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();
            return shortestPath;
        }


        public void BuildShortestPath(List<Node> list, Node node)
        {

            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            ShortestPathLength += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Length;
            ShortestPathCost += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Cost;
            BuildShortestPath(list, node.NearestToStart);
        }

        public void DijkstraSearch()
        {
            NodeVisits = 0;
            Start.MinCostToStart = 0;
            var prioQueue = new List<Node>();
            prioQueue.Add(Start);
            do
            {
                NodeVisits++;
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart.Value).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == End)
                    return;
            } while (prioQueue.Any());
        }

        public List<Node> GetShortestPathAstar()
        {
            foreach (var node in this.nodes)
            node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(End);
            AstarSearch();
            var shortestPath = new List<Node>();
            shortestPath.Add(End);
            BuildShortestPath(shortestPath, End);
            shortestPath.Reverse();
            return shortestPath;
        }

        public void AstarSearch()
        {
            Start.MinCostToStart = 0;
            var prioQueue = new List<Node>();
            prioQueue.Add(Start);
            do
            {
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                NodeVisits++;
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == End)
                    return;
            } while (prioQueue.Any());
        }
    }
}
