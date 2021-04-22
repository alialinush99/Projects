using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficDemo.Classes
{
    [Serializable]
    public class EmergencyVehicle
    {
        private PointF location;
        private float width;
        private float height;
        private Mediator mediator;
        private List<Node> nodesFromStartToCrash;
        private List<Node> nodesFromCrashToEnd; 
        private Queue<Point> pathPoints;
        private Grid grid;
        private Random rgb;
        private Point end; 
        private Point crash;
 

        public Road currentRoad { get; set; }
        public PointF Location { get => location; set => location = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
        internal Mediator Mediator { get => mediator; set => mediator = value; }
        internal  List<Node> NodesFromStartToCrash { get => nodesFromStartToCrash; set => nodesFromStartToCrash = value; }
        public Queue<Point> PathPoints { get => pathPoints; set => pathPoints = value; }
        public Point Crash { get => crash; set => crash = value; }
        internal Grid Grid { get => grid; set => grid = value; }
        public List<Node> NodesFromCrashToEnd { get => nodesFromCrashToEnd; set => nodesFromCrashToEnd = value; }

        public EmergencyVehicle(float X, float Y, float Height, float Width, Mediator med,  List<Node> nodesFromStartToCrash, Point Crash, List<Node> nodesFromEndToCrash , Point end )
        {
            pathPoints = new Queue<Point>();
            rgb = new Random();
            this.Location = new PointF(X, Y);
            this.Height = Height;
            this.Width = Width;
            this.mediator = med;
            this.nodesFromStartToCrash = nodesFromStartToCrash;
            nodesFromEndToCrash.Reverse();
            this.NodesFromCrashToEnd = nodesFromEndToCrash;
            foreach(Node N in this.nodesFromStartToCrash)
            {
                pathPoints.Enqueue(N.Location);
            }
            foreach(Node N in this.nodesFromCrashToEnd)
            {
                pathPoints.Enqueue(N.Location);
            }
            this.crash = Crash;
            this.end = end;
            foreach (Road R in this.mediator.ControlledRoads)
            {
                if ((this.location.X >= R.Location.X) && (this.location.X <= R.Location.X + 200))
                {
                    if (this.location.Y >= R.Location.Y && this.location.Y <= R.Location.Y + 200)
                    {

                        this.currentRoad = R;
                    }

                }
            }


        }

        public void MoveToEmergency()
        {
            foreach (Cell C in this.grid.Cells)
            {
                if (C.Road != null) { 
                    if (((this.location.X > C.Road.Location.X) && (this.location.X < C.Road.Location.X + 200)) && (this.location.Y > C.Road.Location.Y && this.location.Y < C.Road.Location.Y + 200))
                    {

                        this.currentRoad = C.Road;
                   
     
                        break;

                    }
                    if (this.location.X == C.Road.Location.X || this.location.Y == C.Road.Location.Y)
                    {
                        this.currentRoad = C.Road;
                    }
                }

            }


            if (pathPoints.Count > 0 && this.location == pathPoints.Peek())
            {
                pathPoints.Dequeue();
                if (pathPoints.Count > 0) this.crash = pathPoints.Peek();
            }
            if (pathPoints.Count > 0)
            {
                if (pathPoints.Peek().X > this.location.X)
                {
                    this.location.X += 1;
                }
                if (pathPoints.Peek().X < this.location.X)
                {
                    this.location.X -= 1;
                }
                if (pathPoints.Peek().Y > this.location.Y)
                {
                    this.location.Y += 1;
                }
                if (pathPoints.Peek().Y < this.location.Y)
                {
                    this.location.Y -= 1;
                }
            }
            
            if (this.pathPoints.Count == 0 || this.location == end)
            {
                mediator.EmergencyVehicleArrived(this);
            }
 

        }

        public void Draw(Graphics gr)
        {
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(rgb.Next(0, 255), rgb.Next(0, 255), rgb.Next(0, 255)));

            gr.FillEllipse(myBrush, this.location.X - this.currentRoad.Location.X, this.location.Y - this.currentRoad.Location.Y, this.width, this.height);
        }

    }
}


