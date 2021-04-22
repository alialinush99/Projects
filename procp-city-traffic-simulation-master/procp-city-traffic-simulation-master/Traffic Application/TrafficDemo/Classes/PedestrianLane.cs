using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace TrafficDemo.Classes
{
    [Serializable]
    public class PedestrianLane
    {

        private Direction laneDirectionTo;
        private Direction laneDirectionFrom;
        private PointF startPoint;
        private Point startPointOnScreen;
        private Dictionary<int, Point> endPointsOnScreen;
        private PointF endPoint;
        private PointF stoppingPoint;
        private List<PointF> edges;
        private List<Point> edgesOnScreen;
        [NonSerialized] private GraphicsPath myPath;
        private TrafficLight tLight;
        private Road parentRoad;
        private Mediator mediator;
        private bool isInitialLane;
        private List<Pedestrian> pedestrians;

        public PedestrianLane(PointF startPoint, PointF endPoint, PointF stoppingPoint, List<PointF> edgePoints, Direction LDF, Direction LDT, Road parentRoad)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.stoppingPoint = stoppingPoint;
            this.edges = edgePoints;
            this.parentRoad = parentRoad;
            this.endPointsOnScreen = new Dictionary<int, Point>();
            this.edgesOnScreen = new List<Point>();
            myPath = new GraphicsPath();
            if (edges == null)
            {
                myPath.AddLine(startPoint, endPoint);
            }
            else
            {

                if (this.edges.Count == 1)
                {
                    myPath.AddLine(startPoint, edges.Last());
                    myPath.AddLine(edges.Last(), endPoint);

                }
                else if (this.edges.Count > 1)
                {
                    myPath.AddLine(startPoint, edges.First());
                    int counter = 0;
                    foreach (PointF Edge in edges)
                    {
                        if (counter + 1 < edges.Count)
                        {
                            myPath.AddLine(Edge, edges.ElementAt(edges.IndexOf(Edge) + 1));
                        }

                        counter++;

                    }

                    myPath.AddLine(edges.Last(), endPoint);
                }
                else
                {
                    MyPath.AddLine(startPoint, endPoint);
                }

            }
            Pedestrians = new List<Pedestrian>();
            this.laneDirectionTo = LDT;
            this.laneDirectionFrom = LDF;
            isInitialLane = true;
        }


        // ---------------------------------------Properties--------------------------------------------
        public GraphicsPath MyPath { get => myPath; set => myPath = value; }
        public List<Pedestrian> Pedestrians { get => pedestrians; set => pedestrians = value; }
        public PointF StartPoint { get => startPoint; set => startPoint = value; }
        public PointF StoppingPoint { get => stoppingPoint; set => stoppingPoint = value; }
        internal TrafficLight TLight { get => tLight; set => tLight = value; }
        internal Direction LaneDirectionTo { get => laneDirectionTo; set => laneDirectionTo = value; }
        internal Direction LaneDirectionFrom { get => laneDirectionFrom; set => laneDirectionFrom = value; }
        public List<PointF> Edges { get => edges; set => edges = value; }
        public PointF EndPoint { get => endPoint; set => endPoint = value; }
        internal Road ParentRoad { get => parentRoad; set => parentRoad = value; }
        public Point StartPointOnScreen { get => startPointOnScreen; set => startPointOnScreen = value; }
        public Dictionary<int, Point> EndPointsOnScreen { get => endPointsOnScreen; set => endPointsOnScreen = value; }
        internal Mediator MyMediator { get => mediator; set => mediator = value; }
        public bool IsInitialLane { get => isInitialLane; set => isInitialLane = value; }
        public List<Point> EdgesOnScreen { get => edgesOnScreen; set => edgesOnScreen = value; }


        public void RemoveSpecificPedestrian(Pedestrian p)
        {
            pedestrians.Remove(p);
        }

        public void RemoveAllPedestrians()
        {
            this.pedestrians = new List<Pedestrian>();
        }

        public void MovePedastrians()
        {
            Pedestrian frontPedestrian = null;

            foreach (Pedestrian pedestrian in pedestrians.ToList())
            {
                var shouldStop = (frontPedestrian != null || IsAtStoppingPoint(pedestrian)) && IsRedLight(pedestrian);
                if (shouldStop)
                {
                    if (frontPedestrian == null || IsNotCloseWithEachOther(pedestrian, frontPedestrian))
                    {
                        pedestrian.Stop();
                        frontPedestrian = pedestrian;
                    }
                }
                else
                {
                    pedestrian.Walk();
                }

                if (pedestrian.Location == pedestrian.CurrentLane.EndPoint)
                {
                    pedestrians.Remove(pedestrian);
                }
            }
        }

        private static bool IsRedLight(Pedestrian pedestrian)
        {
            return pedestrian.CurrentLane.TLight.IsGreen == false;
        }

        private static bool IsAtStoppingPoint(Pedestrian p)
        {
            return p.Location == p.CurrentLane.StoppingPoint;
        }

        private static bool IsNotCloseWithEachOther(Pedestrian pedestrian, Pedestrian frontPedestrian)
        {
            return (
                pedestrian.Location.X == frontPedestrian.Location.X && pedestrian.Location.Y < frontPedestrian.Location.Y && frontPedestrian.Location.Y - pedestrian.Location.Y < 10)
                || (pedestrian.Location.X == frontPedestrian.Location.X && pedestrian.Location.Y > frontPedestrian.Location.Y && pedestrian.Location.Y - frontPedestrian.Location.Y < 10)
                || (pedestrian.Location.Y == frontPedestrian.Location.Y && pedestrian.Location.X > frontPedestrian.Location.X && pedestrian.Location.X - frontPedestrian.Location.X < 10)
                || (pedestrian.Location.Y == frontPedestrian.Location.Y && pedestrian.Location.X < frontPedestrian.Location.X && frontPedestrian.Location.X - pedestrian.Location.X < 10);
        }

        public void DrawPedastrians(Graphics gr)
        {
            this.Pedestrians.ForEach(p => p.Draw(gr));
        }
    }
}
