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
    public class Lane
    {
        // ---------------------------------------Fields--------------------------------------------

        private Direction laneDirectionTo;
        private Direction laneDirectionFrom;
        private PointF startPoint;
        private Point startPointOnScreen;
        private Dictionary<int, Point> endPointsOnScreen;
        private PointF endPoint;
        private PointF stoppingPoint;
        private List<PointF> edges;
        private List<Point> edgesOnScreen;
        private List<Car> cars;
        [NonSerialized] private GraphicsPath myPath;
        private TrafficLight tLight;
        private Road parentRoad;
        private Mediator mediator;
        private bool isInitialLane;
        //   PointF[] pathPoints;

        // -----------------------------------------------------------------------------------



        // ---------------------------------------Constructors--------------------------------------------

        public Lane(PointF startPoint, PointF endPoint, PointF stoppingPoint, List<PointF> edgePoints, Direction LDF, Direction LDT, Road parentRoad)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.stoppingPoint = stoppingPoint;
            this.edges = edgePoints;
            this.parentRoad = parentRoad;
            this.endPointsOnScreen = new Dictionary<int, Point>();
            this.edgesOnScreen = new List<Point>();
            if(parentRoad.type == "two-way-h")
            {
                if(LDF == Direction.West)
                {
                    this.endPointsOnScreen.Add(2, new Point(120 + parentRoad.Pb_Background.Location.X, parentRoad.Pb_Background.Location.Y));
                }
                else
                {
                    this.endPointsOnScreen.Add(2, new Point(80 + parentRoad.Pb_Background.Location.X, 200 + parentRoad.Pb_Background.Location.Y));

                }
            }
            else if(parentRoad.type == "two-way-v")
            {
                if (LDF == Direction.North)
                {
                    this.endPointsOnScreen.Add(2, new Point(200 + parentRoad.Pb_Background.Location.X,120+ parentRoad.Pb_Background.Location.Y));

                }
                else
                {
                    this.endPointsOnScreen.Add(2, new Point(parentRoad.Pb_Background.Location.X, 80 + parentRoad.Pb_Background.Location.Y));

                }
            }
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

            cars = new List<Car>();
            this.laneDirectionTo = LDT;
            this.laneDirectionFrom = LDF;
            isInitialLane = true;
        }


        // ---------------------------------------Properties--------------------------------------------
        public GraphicsPath MyPath { get => myPath; set => myPath = value; }
        public List<Car> Cars { get => cars; set => cars = value; }
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
        // ---------------------------------------Methods--------------------------------------------

        public void RemoveSpecificCar(Car c)
        {
            cars.Remove(c);
            parentRoad.CarsOnRoad -= 1;
        } 

        public void RemoveAllCars()
        {
            this.cars = new List<Car>();
            parentRoad.CarsOnRoad = 0;
        }

        public void MoveCars()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                Car C = this.cars[i];
                C.Accelerate();
            }
        }
        public void DrawCars(Graphics gr)
        {
            this.Cars.ForEach(car => car.Draw(gr));
        }
        public void Clear()
        {
            myPath.Reset();
        }


        public void Redraw()
        {
            if (this.edges != null)
            {
                if (this.edges.Count == 1)
                {
                    myPath.AddLine(startPoint, edges.Last());
                    myPath.AddLine(edges.Last(), endPoint);
                }

                else if (this.edges.Count > 1)
                {
                    myPath.AddLine(startPoint, edges.First());
                    for (int i = 0; i < edges.Count; i++)
                    {
                        PointF Edge = edges[i];
                        myPath.AddLine(Edge, edges.ElementAt(edges.IndexOf(Edge) + 1));


                    }

                    myPath.AddLine(edges.Last(), endPoint);
                }

                else
                {
                    MyPath.AddLine(startPoint, endPoint);
                }
            }
            else
            {
                MyPath.AddLine(startPoint, endPoint);
            }
        }
    }
}
