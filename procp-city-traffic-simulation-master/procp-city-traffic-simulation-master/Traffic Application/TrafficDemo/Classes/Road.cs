using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TrafficDemo.Classes
{
    [Serializable]
   public class Road
    {

        private Point location;
        private List<Lane> lanes;
        public Dictionary<string, List<Lane>> lanesGroups { get; set; }
        private List<PedestrianLane> pedestriansLanes;
        public Dictionary<string, List<PedestrianLane>> pedestriansLanesGroups { get; set; }
        [NonSerialized] private PictureBox pb_back;
        private TrafficLightsGroups groups;
        private Mediator mediator;
        internal string type;
        private int carsOnRoad;
        [NonSerialized] public Graphics gr;
        [NonSerialized] Timer ligthsTimer;

        public Road(Point location, PictureBox pb_background)
        {
            this.lanes = new List<Lane>();
            this.lanesGroups = new Dictionary<string, List<Lane>>(4);
            this.lanesGroups.Add("east", new List<Lane>());
            this.lanesGroups.Add("west", new List<Lane>());
            this.lanesGroups.Add("north", new List<Lane>());
            this.lanesGroups.Add("south", new List<Lane>());
            this.pedestriansLanes = new List<PedestrianLane>();
            this.pedestriansLanesGroups = new Dictionary<string, List<PedestrianLane>>(4);
            this.pedestriansLanesGroups.Add("east", new List<PedestrianLane>());
            this.pedestriansLanesGroups.Add("west", new List<PedestrianLane>());
            this.pedestriansLanesGroups.Add("north", new List<PedestrianLane>());
            this.pedestriansLanesGroups.Add("south", new List<PedestrianLane>());
            this.carsOnRoad = 0;
            this.location = location;
            this.pb_back = pb_background;
            groups = new TrafficLightsGroups();
            gr = pb_back.CreateGraphics();
            ligthsTimer = new Timer();
            ligthsTimer.Interval = 1000;
            ligthsTimer.Tick += ligthsTimer_tick;
        }
        public List<Lane> Lanes { get => lanes; set => lanes = value; }
        public Point Location { get => location; set => this.location = value; }
        public PictureBox Pb_Background { get => this.pb_back; set => this.pb_back = value; }
        public TrafficLightsGroups trafficGroups { get => this.groups; set => this.groups = value; }
        internal Mediator Mediator { get => mediator; set => mediator = value; }
        public Timer LigthsTimer { get => ligthsTimer; set => ligthsTimer = value; }
        public int CarsOnRoad { get => carsOnRoad; set => carsOnRoad = value; }

        public void StartTLTimer()
        {
            ligthsTimer.Start();
        }
        public void TrSpeed(int speed)
        {
            ligthsTimer.Interval = 4000 / speed;
        }
        /// <summary>
        /// Stops the traffic light timer.
        /// </summary>
        public void StopTLTimer()
        {
            ligthsTimer.Stop();
        }

        /// <summary>
        /// Initializes the non-serialized fields of the crossing.
        /// </summary>
        public void InitializeRoad()
        {
            gr = pb_back.CreateGraphics();
            ligthsTimer = new Timer();
            ligthsTimer.Interval = 1000;
            ligthsTimer.Tick += ligthsTimer_tick;
        }
        public void AssignMediator(Mediator M)
        {
            this.mediator = M;
            foreach (string g in this.lanesGroups.Keys)
            {
                foreach (Lane L in this.lanesGroups[g])
                {
                    L.MyMediator = M;
                    foreach (Car C in L.Cars)
                    {
                        C.Mediator = M;
                    }
                }
            }
        }

        public void AddCars(Lane lane, int amount)
        {
            CarsOnRoad += amount;
            foreach (string group in this.lanesGroups.Keys)
            {
                if (this.lanesGroups[group].Contains(lane))
                {
                    float distanceBetweenCars = 0;
                    for (int i = 0; i < amount; i++)
                    {
                        int dice = new Random().Next(0, this.lanesGroups[group].Count);
                        Lane l = this.lanesGroups[group][dice];
                        Car c = new Car(Color.White, 0, 0, 0, 0, l);
                        if (amount == 1)
                        {
                            foreach (Lane laneInGroup in this.lanesGroups[group])
                            {
                                if (laneInGroup.Cars.Count > 0)
                                {
                                    if (laneInGroup.LaneDirectionFrom == Direction.North)
                                    {
                                        float distance = laneInGroup.Cars.Last().Location.Y - laneInGroup.StartPoint.Y;
                                        if (distance < 10) distanceBetweenCars += Math.Abs(15 - distance);
                                    }
                                    else if (laneInGroup.LaneDirectionFrom == Direction.South)
                                    {
                                        float distance = laneInGroup.StartPoint.Y - laneInGroup.Cars.Last().Location.Y;
                                        if (distance < 10) distanceBetweenCars += Math.Abs(15 - distance);
                                    }
                                    else if (laneInGroup.LaneDirectionFrom == Direction.West)
                                    {
                                        float distance = laneInGroup.Cars.Last().Location.X - laneInGroup.StartPoint.X;
                                        if (distance < 10) distanceBetweenCars += Math.Abs(15 - distance);
                                    }
                                    else if (laneInGroup.LaneDirectionFrom == Direction.East)
                                    {
                                        float distance = laneInGroup.StartPoint.X - laneInGroup.Cars.Last().Location.X;
                                        if (distance < 10) distanceBetweenCars += Math.Abs(15 - distance);
                                    }
                                }
                            }
                        }
                        else
                        {
                            distanceBetweenCars += 15;
                        }
                        if (l.LaneDirectionFrom == Direction.North)
                        {
                            c = new Car(Color.Red, l.StartPoint.X, l.StartPoint.Y - distanceBetweenCars, 7, 7, l);
                            l.Cars.Add(c); // Create the next car 10 pixels behind 
                        }
                        else if (l.LaneDirectionFrom == Direction.South)
                        {
                            c = new Car(Color.Red, l.StartPoint.X, l.StartPoint.Y + distanceBetweenCars, 7, 7, l);
                            l.Cars.Add(c); // Create the next car 10 pixels behind 
                        }
                        else if (l.LaneDirectionFrom == Direction.West)
                        {
                            c = new Car(Color.Red, l.StartPoint.X - distanceBetweenCars, l.StartPoint.Y, 7, 7, l);
                            l.Cars.Add(c); // Create the next car 10 pixels behind 
                        }
                        else if (l.LaneDirectionFrom == Direction.East)
                        {
                            c = new Car(Color.Red, l.StartPoint.X + distanceBetweenCars, l.StartPoint.Y, 7, 7, l);
                            l.Cars.Add(c); // Create the next car 10 pixels behind 
                        }
                        if (mediator != null) mediator.Clients.Add(c);
                    }
                    break;
                }
            }
        }

        public void AddLane(Direction from, Direction to)
        {
            PointF start, end, stop, edge1, edge2;
            List<PointF> edges;
            Lane mylane;
            //         switch (roadType) {
            //            case "Crossing":
            switch (from)
            {
                case Direction.North:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(80, 0);
                            end = new PointF(200, 120);
                            edges = new List<PointF>();
                            stop = new PointF(80, 50);
                            switch (this.type)
                            {
                                case "1":
                                    edges.Add(new PointF(80, 80));
                                    edges.Add(new PointF(80, 120));
                                    break;

                                case "2":
                                    edges.Add(new PointF(105, 15));
                                    edges.Add(new PointF(105, 120));
                                    stop.X = 105;
                                    break;
                            }
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.East, this);
                            switch (this.type)
                            {
                                case "1":
                                    mylane.TLight = groups.GetGroup3()[1];
                                    break;

                                case "2":
                                    mylane.TLight = groups.GetGroup4()[1];
                                    break;
                            }
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y - 15);
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.West:
                            start = new PointF(80, 0);
                            end = new PointF(0, 80);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 80));
                            stop = new PointF(80, 50);
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.West, this);
                            mylane.TLight = groups.GetGroup3()[1];
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y - 15);
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.South:
                            start = new PointF(80, 0);
                            end = new PointF(80, 200);
                            stop = new PointF(start.X, start.Y + 50);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 80));
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.South, this);
                            mylane.TLight = groups.GetGroup3()[1];
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y - 15);
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.South:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(120, 200);
                            end = new PointF(200, 120);
                            stop = new PointF(start.X, start.Y - 65);
                            edges = new List<PointF>();
                            edge1 = new PointF(120, 120);
                            edges.Add(edge1);
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.East, this);
                            mylane.TLight = groups.GetGroup3()[0];
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y + 15);
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.West:
                            start = new PointF(120, 200);
                            end = new PointF(0, 80);
                            stop = new PointF(start.X, start.Y - 65);
                            edges = new List<PointF>();
                            switch (this.type)
                            {

                                case "1":
                                    edges.Add(new PointF(120, 120));
                                    edges.Add(new PointF(120, 80));
                                    break;

                                case "2":
                                    edges.Add(new PointF(95, 180));
                                    edges.Add(new PointF(95, 80));
                                    stop.X = 95;
                                    break;

                            }
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.West, this);
                            switch (this.type)
                            {
                                case "1":
                                    mylane.TLight = groups.GetGroup3()[0];
                                    break;
                                case "2":
                                    mylane.TLight = groups.GetGroup4()[0];
                                    break;
                            }
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y + 15);
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);

                            break;
                        case Direction.North:

                            start = new PointF(120, 200);
                            end = new PointF(120, 0);
                            stop = new PointF(start.X, start.Y - 65);
                            edges = new List<PointF>();
                            edge1 = new PointF(120, 120);
                            edges.Add(edge1);
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.North, this);
                            mylane.TLight = groups.GetGroup3()[0];
                            mylane.StoppingPoint = new PointF(mylane.StoppingPoint.X, mylane.TLight.Location.Y + 15);
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.West:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(0, 120);
                            end = new PointF(200, start.Y);
                            stop = new PointF(55, 120);
                            edges = new List<PointF>();
                            edge1 = new PointF(80, 120);
                            edges.Add(edge1);
                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.East, this);
                            mylane.TLight = groups.GetGroup2()[0];
                            mylane.StoppingPoint = new PointF(mylane.TLight.Location.X - 15, mylane.StoppingPoint.Y);
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.North:
                            start = new PointF(0, 120);
                            end = new PointF(120, 0);

                            stop = new PointF(55, 95);

                            edges = new List<PointF>();
                            edge1 = new PointF(25, 95);
                            edge2 = new PointF(120, 95);
                            edges.Add(edge1);
                            edges.Add(edge2);

                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.North, this);
                            mylane.TLight = groups.GetGroup1()[0];
                            mylane.StoppingPoint = new PointF(mylane.TLight.Location.X - 15, mylane.StoppingPoint.Y);
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;

                        case Direction.South:

                            start = new PointF(0, 120);
                            end = new PointF(80, 200);

                            stop = new PointF(start.X + 55, start.Y);
                            edges = new List<PointF>();
                            edge1 = new PointF(80, 120);
                            edges.Add(edge1);

                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.South, this);
                            mylane.TLight = groups.GetGroup2()[0];
                            mylane.StoppingPoint = new PointF(mylane.TLight.Location.X - 15, mylane.StoppingPoint.Y);
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.East:
                    switch (to)
                    {
                        case Direction.North:
                            start = new PointF(200, 80);
                            end = new PointF(120, 0);

                            stop = new PointF(start.X - 55, start.Y);
                            edges = new List<PointF>();
                            edge1 = new PointF(120, 80);
                            edges.Add(edge1);
                            mylane = new Lane(start, end, stop, edges, Direction.East, Direction.North, this);
                            mylane.TLight = groups.GetGroup2()[1];
                            mylane.StoppingPoint = new PointF(mylane.TLight.Location.X + 15, mylane.StoppingPoint.Y);
                            this.lanesGroups["east"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;

                        case Direction.West:
                            start = new PointF(200, 80);
                            end = new PointF(0, 80);

                            stop = new PointF(start.X - 55, start.Y);
                            edges = new List<PointF>();
                            edge1 = new PointF(120, 80);
                            edges.Add(edge1);
                            mylane = new Lane(start, end, stop, edges, Direction.East, Direction.West, this);
                            mylane.TLight = groups.GetGroup2()[1];
                            mylane.StoppingPoint = new PointF(mylane.TLight.Location.X + 15, mylane.StoppingPoint.Y);
                            this.lanesGroups["east"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;

                        case Direction.South:
                            {
                                start = new PointF(200, 80);
                                end = new PointF(80, 200);
                                stop = new PointF(start.X - 55, 105);
                                edges = new List<PointF>();
                                edge1 = new PointF(175, 105);
                                edge2 = new PointF(80, 105);
                                edges.Add(edge1);
                                edges.Add(edge2);
                                mylane = new Lane(start, end, stop, edges, Direction.East, Direction.South, this);
                                mylane.TLight = groups.GetGroup1()[1];
                                mylane.StoppingPoint = new PointF(mylane.TLight.Location.X + 15, mylane.StoppingPoint.Y);
                                this.lanesGroups["east"].Add(mylane);
                                this.lanes.Add(mylane);
                                break;
                            }
                    }


                    break;
            }
            //break;
            //      }
        }
        public void AddRoundaboutLanes(Direction from, Direction to)
        {
            PointF start, end, stop;
            List<PointF> edges;
            Lane mylane;
            switch (from)
            {
                case Direction.North:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(80, 0);
                            end = new PointF(200, 120);
                            stop = new PointF(start.X, start.Y + 50);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(68, 90));
                            edges.Add(new PointF(68, 110));
                            edges.Add(new PointF(78, 120));
                            edges.Add(new PointF(90, 130));
                            edges.Add(new PointF(100, 130));
                            edges.Add(new PointF(110, 130));
                            edges.Add(new PointF(120, 120));
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.East, this);
                            mylane.TLight = null;
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.West:
                            start = new PointF(80, 0);
                            end = new PointF(0, 80);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 80));
                            stop = new PointF(80, 50);
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.West, this);
                            mylane.TLight = null;
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.South:
                            start = new PointF(80, 0);
                            end = new PointF(80, 200);
                            stop = new PointF(start.X, start.Y + 50);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(68, 90));
                            edges.Add(new PointF(68, 110));
                            edges.Add(new PointF(80, 130));
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.South, this);
                            mylane.TLight = null;
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.North:
                            start = new PointF(80, 0);
                            end = new PointF(120, 0);
                            stop = new PointF(start.X, start.Y + 50);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(68, 90));
                            edges.Add(new PointF(68, 110));
                            edges.Add(new PointF(78, 120));
                            edges.Add(new PointF(90, 130));
                            edges.Add(new PointF(100, 130));
                            edges.Add(new PointF(110, 130));
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 70));
                            mylane = new Lane(start, end, stop, edges, Direction.North, Direction.North, this);
                            mylane.TLight = null;
                            this.lanesGroups["north"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.South:
                    switch (to)
                    {
                        case Direction.West:
                            start = new PointF(120, 200);
                            end = new PointF(0, 80);
                            stop = new PointF(120, 120);
                            edges = new List<PointF>();
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 74));
                            edges.Add(new PointF(110, 68));
                            edges.Add(new PointF(90, 68));
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(70, 80));
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.West, this);
                            mylane.TLight = null;
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.East:
                            start = new PointF(120, 200);
                            end = new PointF(200, 120);
                            edges = new List<PointF>();
                            edges.Add(new PointF(120, 120));
                            stop = new PointF(120, 120);
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.East, this);
                            mylane.TLight = null;
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.South:
                            start = new PointF(120, 200);
                            end = new PointF(80, 200);
                            stop = new PointF(120, 120);
                            edges = new List<PointF>();
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 74));
                            edges.Add(new PointF(110, 68));
                            edges.Add(new PointF(90, 68));
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(70, 80));
                            edges.Add(new PointF(70, 90));
                            edges.Add(new PointF(70, 110));
                            edges.Add(new PointF(80, 130));
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.South, this);
                            mylane.TLight = null;
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                        case Direction.North:
                            start = new PointF(120, 200);
                            end = new PointF(120, 0);
                            stop = new PointF(120, 120);
                            edges = new List<PointF>();
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(120, 70));
                            mylane = new Lane(start, end, stop, edges, Direction.South, Direction.North, this);
                            mylane.TLight = null;
                            this.lanesGroups["south"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.West:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(0, 120);
                            end = new PointF(200, 120);

                            stop = new PointF(55, 95);

                            edges = new List<PointF>();
                            edges.Add(new PointF(25, 95));
                            edges.Add(new PointF(60, 95));
                            edges.Add(new PointF(70, 110));
                            edges.Add(new PointF(80, 120));
                            edges.Add(new PointF(90, 128));
                            edges.Add(new PointF(100, 128));
                            edges.Add(new PointF(110, 128));
                            edges.Add(new PointF(120, 120));
                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.East, this);
                            mylane.TLight = null;
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            // this.lanes.Add(mylane);
                            break;
                        case Direction.North:
                            start = new PointF(0, 120);
                            end = new PointF(120, 0);

                            stop = new PointF(55, 95);

                            edges = new List<PointF>();
                            edges.Add(new PointF(25, 95));
                            edges.Add(new PointF(60, 95));
                            edges.Add(new PointF(70, 110));
                            edges.Add(new PointF(80, 120));
                            edges.Add(new PointF(90, 128));
                            edges.Add(new PointF(100, 128));
                            edges.Add(new PointF(110, 128));
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(120, 70));
                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.North, this);
                            mylane.TLight = null;
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            //this.lanes.Add(mylane);
                            break;

                        case Direction.South:

                            start = new PointF(0, 120);
                            end = new PointF(80, 200);

                            stop = new PointF(start.X + 55, start.Y);
                            edges = new List<PointF>();
                            edges.Add(new PointF(80, 120));

                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.South, this);
                            mylane.TLight = null;
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            //this.lanes.Add(mylane);

                            break;
                        case Direction.West:
                            start = new PointF(0, 120);
                            end = new PointF(0, 80);

                            stop = new PointF(55, 95);

                            edges = new List<PointF>();
                            edges.Add(new PointF(25, 95));
                            edges.Add(new PointF(60, 95));
                            edges.Add(new PointF(70, 110));
                            edges.Add(new PointF(80, 120));
                            edges.Add(new PointF(90, 128));
                            edges.Add(new PointF(100, 128));
                            edges.Add(new PointF(110, 128));
                            edges.Add(new PointF(120, 120));
                            edges.Add(new PointF(130, 110));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 74));
                            edges.Add(new PointF(110, 68));
                            edges.Add(new PointF(90, 68));
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(70, 80));
                            mylane = new Lane(start, end, stop, edges, Direction.West, Direction.West, this);
                            mylane.TLight = null;
                            this.lanesGroups["west"].Add(mylane);
                            this.lanes.Add(mylane);
                            //this.lanes.Add(mylane);
                            break;
                    }
                    break;

                case Direction.East:
                    switch (to)
                    {
                        case Direction.North:
                            start = new PointF(200, 80);
                            end = new PointF(120, 0);

                            stop = new PointF(start.X - 55, start.Y);
                            edges = new List<PointF>();
                            edges.Add(new PointF(120, 80));
                            mylane = new Lane(start, end, stop, edges, Direction.East, Direction.North, this);
                            mylane.TLight = null;
                            this.lanesGroups["east"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;

                        case Direction.West:
                            start = new PointF(200, 80);
                            end = new PointF(0, 80);

                            stop = new PointF(start.X - 55, start.Y);
                            edges = new List<PointF>();
                            edges.Add(new PointF(175, 105));
                            edges.Add(new PointF(135, 105));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 74));
                            edges.Add(new PointF(110, 68));
                            edges.Add(new PointF(90, 68));
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(70, 80));
                            mylane = new Lane(start, end, stop, edges, Direction.East, Direction.West, this);
                            mylane.TLight = null;
                            this.lanesGroups["east"].Add(mylane);
                            this.lanes.Add(mylane);

                            break;

                        case Direction.South:
                            {
                                start = new PointF(200, 80);
                                end = new PointF(80, 200);
                                stop = new PointF(start.X - 55, start.Y);
                                edges = new List<PointF>();
                                edges.Add(new PointF(175, 105));
                                edges.Add(new PointF(135, 105));
                                edges.Add(new PointF(130, 90));
                                edges.Add(new PointF(125, 80));
                                edges.Add(new PointF(120, 74));
                                edges.Add(new PointF(110, 68));
                                edges.Add(new PointF(90, 68));
                                edges.Add(new PointF(80, 70));
                                edges.Add(new PointF(70, 80));
                                edges.Add(new PointF(70, 110));
                                edges.Add(new PointF(80, 130));
                                mylane = new Lane(start, end, stop, edges, Direction.East, Direction.South, this);
                                mylane.TLight = null;
                                this.lanesGroups["east"].Add(mylane);
                                this.lanes.Add(mylane);

                                break;
                            }
                        case Direction.East:
                            start = new PointF(200, 80);
                            end = new PointF(200, 120);
                            stop = new PointF(start.X - 55, start.Y);
                            edges = new List<PointF>();
                            edges.Add(new PointF(175, 105));
                            edges.Add(new PointF(135, 105));
                            edges.Add(new PointF(130, 90));
                            edges.Add(new PointF(125, 80));
                            edges.Add(new PointF(120, 74));
                            edges.Add(new PointF(110, 68));
                            edges.Add(new PointF(90, 68));
                            edges.Add(new PointF(80, 70));
                            edges.Add(new PointF(70, 80));
                            edges.Add(new PointF(70, 110));
                            edges.Add(new PointF(80, 120));
                            edges.Add(new PointF(90, 128));
                            edges.Add(new PointF(100, 128));
                            edges.Add(new PointF(110, 128));
                            edges.Add(new PointF(120, 120));
                            mylane = new Lane(start, end, stop, edges, Direction.East, Direction.East, this);
                            mylane.TLight = null;
                            this.lanesGroups["east"].Add(mylane);
                            this.lanes.Add(mylane);
                            break;
                    }


                    break;
            }
            //break;
            //      }
        }

        private void AddTwoWayLane(Direction from)
        {
            PointF start, end;
            Lane mylane;
            switch (from)
            {
                case Direction.East:
                    start = new PointF(200, 80);
                    end = new PointF(0, 80);
                    mylane = new Lane(start, end, end, null, Direction.East, Direction.West, this);
                    mylane.TLight = null;
                    this.lanesGroups["east"].Add(mylane);
                    this.lanes.Add(mylane);
                    break;
                case Direction.West:
                    start = new PointF(0, 120);
                    end = new PointF(200, 120);
                    mylane = new Lane(start, end, end, null, Direction.West, Direction.East, this);
                    mylane.TLight = null;
                    this.lanesGroups["west"].Add(mylane);
                    this.lanes.Add(mylane);
                    break;
                case Direction.North:
                    start = new PointF(80, 0);
                    end = new PointF(80, 200);
                    mylane = new Lane(start, end, end, null, Direction.North, Direction.South, this);
                    mylane.TLight = null;
                    this.lanesGroups["north"].Add(mylane);
                    this.lanes.Add(mylane);
                    break;
                case Direction.South:
                    start = new PointF(120, 200);
                    end = new PointF(120, 0);
                    mylane = new Lane(start, end, end, null, Direction.South, Direction.North, this);
                    mylane.TLight = null;
                    this.lanesGroups["south"].Add(mylane);
                    this.lanes.Add(mylane);
                    break;
            }
        }

        private void ligthsTimer_tick(object sender, EventArgs e)
        {

            if (groups.Count())
            {
                if (groups.Phase == "1")
                {

                }
                else if (groups.Phase == "2")
                {

                }
                else if (groups.Phase == "3")
                {

                }
                else if (groups.Phase == "4")
                {

                }
            }
        }

        public void DrawLanesAndCars()
        {
            foreach (string g in this.lanesGroups.Keys)
            {
                foreach (Lane l in this.lanesGroups[g])
                {
                    l.DrawCars(gr);
                }
            }

            if (this.mediator != null)
            {
                if (this.mediator.EV != null && this.mediator.EV.currentRoad == this)
                {
                    this.mediator.EV.Draw(gr);
                }
            }
        }
        public void PlaceTrafficLightsOnCrossingPb()
        {
            foreach (TrafficLight tl in groups.GetGroup1())
            {
                tl.Draw(gr);

            }
            foreach (TrafficLight tl in groups.GetGroup2())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in groups.GetGroup3())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in groups.GetGroup4())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in groups.GetPedestrianTlG1())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in groups.GetPedestrianTlG2())
            {
                tl.Draw(gr);
            }
        }

        public void AddRoad()
        {
            if (this.type == "two-way-h")
            {
                AddTwoWayLane(Direction.East);
                AddTwoWayLane(Direction.West);
            }
            else if (this.type == "two-way-v")
            {
                AddTwoWayLane(Direction.North);
                AddTwoWayLane(Direction.South);
            }
            else if (this.type == "roundabout")
            {
                AddRoundaboutLanes(Direction.East, Direction.West);
                AddRoundaboutLanes(Direction.East, Direction.East);
                AddRoundaboutLanes(Direction.East, Direction.South);
                AddRoundaboutLanes(Direction.East, Direction.North);

                AddRoundaboutLanes(Direction.West, Direction.North);
                AddRoundaboutLanes(Direction.West, Direction.East);
                AddRoundaboutLanes(Direction.West, Direction.South);
                AddRoundaboutLanes(Direction.West, Direction.West);

                AddRoundaboutLanes(Direction.South, Direction.East);
                AddRoundaboutLanes(Direction.South, Direction.West);
                AddRoundaboutLanes(Direction.South, Direction.North);
                AddRoundaboutLanes(Direction.South, Direction.South);

                AddRoundaboutLanes(Direction.North, Direction.North);
                AddRoundaboutLanes(Direction.North, Direction.South);
                AddRoundaboutLanes(Direction.North, Direction.East);
                AddRoundaboutLanes(Direction.North, Direction.West);

                //pedestrians
                AddSidewalkLane(Direction.North, Direction.North);
                AddSidewalkLane(Direction.East, Direction.West);
                AddSidewalkLane(Direction.West, Direction.East);
                AddSidewalkLane(Direction.South, Direction.South);
            }
            else
            {
                if (this.type == "1")
                {
                    AddSidewalkLane(Direction.North, Direction.North);
                    AddSidewalkLane(Direction.East, Direction.West);
                    AddSidewalkLane(Direction.West, Direction.East);
                    AddSidewalkLane(Direction.South, Direction.South);
                }

                AddLane(Direction.North, Direction.South);
                AddLane(Direction.North, Direction.West);
                AddLane(Direction.North, Direction.East);
                AddLane(Direction.West, Direction.North);
                AddLane(Direction.West, Direction.South);
                AddLane(Direction.West, Direction.East);


                AddLane(Direction.South, Direction.North);
                AddLane(Direction.South, Direction.West);
                AddLane(Direction.South, Direction.East);

                AddLane(Direction.East, Direction.South);
                AddLane(Direction.East, Direction.North);
                AddLane(Direction.East, Direction.West);
            }
        }
        public void DrawPedestriansLanesAndPedestrians()
        {
            foreach (string g in this.pedestriansLanesGroups.Keys)
            {
                foreach (PedestrianLane pl in this.pedestriansLanesGroups[g])
                {
                    pl.DrawPedastrians(gr);
                }
            }
        }
        private void AddSidewalkLane(Direction from, Direction to)
        {
            PointF start, end, stop, edge1, edge2;
            List<PointF> edges;
            PedestrianLane mylane;
            switch (from)
            {
                case Direction.North:
                    switch (to)
                    {
                        case Direction.North:
                            start = new PointF(56, 0);
                            end = new PointF(135, 0);
                            stop = new PointF(56, 56);
                            edges = new List<PointF>();
                            edges.Add(new PointF(56, 56));
                            edges.Add(new PointF(135, 56));
                            mylane = new PedestrianLane(start, end, stop, edges, Direction.North, Direction.West, this);
                            //mylane.IsSidewalkLane = true;
                            mylane.TLight = groups.GetPedestrianTlG1()[0];
                            mylane.StoppingPoint = new PointF(56, 56);
                            this.pedestriansLanesGroups["north"].Add(mylane);
                            break;
                    }
                    break;

                case Direction.West:
                    switch (to)
                    {
                        case Direction.East:
                            start = new PointF(0, 135);
                            end = new PointF(200, 135);
                            stop = new PointF(56, 135);
                            edges = new List<PointF>();
                            edges.Add(new PointF(56, 135));
                            edges.Add(new PointF(200, 135));
                            mylane = new PedestrianLane(start, end, stop, edges, Direction.West, Direction.East, this);
                            //mylane.IsSidewalkLane = true;
                            mylane.TLight = groups.GetPedestrianTlG2()[0];
                            mylane.StoppingPoint = new PointF(56, 135);
                            this.pedestriansLanesGroups["west"].Add(mylane);
                            break;
                    }
                    break;


                case Direction.East:
                    switch (to)
                    {
                        case Direction.West:
                            start = new PointF(200, 50);
                            end = new PointF(0, 50);
                            stop = new PointF(140, 50);
                            edges = new List<PointF>();
                            edges.Add(new PointF(140, 50));
                            edges.Add(new PointF(0, 50));
                            mylane = new PedestrianLane(start, end, stop, edges, Direction.East, Direction.West, this);
                            //mylane.IsSidewalkLane = true;
                            mylane.TLight = groups.GetPedestrianTlG1()[1];
                            mylane.StoppingPoint = new PointF(133, 50);
                            this.pedestriansLanesGroups["east"].Add(mylane);
                            break;

                    }
                    break;

                case Direction.South:
                    switch (to)
                    {
                        case Direction.South:
                            start = new Point(138, 200);
                            end = new PointF(55, 200);
                            stop = new PointF(138, 145);
                            edges = new List<PointF>();
                            edges.Add(new PointF(138, 145));
                            edges.Add(new PointF(55, 145));
                            mylane = new PedestrianLane(start, end, stop, edges, Direction.South, Direction.West, this);
                            //mylane.IsSidewalkLane = true;
                            mylane.TLight = groups.GetPedestrianTlG2()[1];
                            mylane.StoppingPoint = new PointF(138, 145);
                            this.pedestriansLanesGroups["south"].Add(mylane);
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        public void AddPedestrians(PedestrianLane lane, int amount)
        {
            foreach (string group in this.pedestriansLanesGroups.Keys)
            {
                if (this.pedestriansLanesGroups[group].Contains(lane))
                {
                    float distanceBetweenPedestrian = 0;
                    for (int i = 0; i < amount; i++)
                    {
                        int dice = new Random().Next(0, this.pedestriansLanesGroups[group].Count);
                        PedestrianLane pl = this.pedestriansLanesGroups[group][dice];
                        Pedestrian p = new Pedestrian(Color.Yellow, 0, 0, 0, 0, pl);

                        if (amount == 1)
                        {
                            foreach (PedestrianLane pedestrianlaneInGroup in this.pedestriansLanesGroups[group])
                            {
                                if (pedestrianlaneInGroup.Pedestrians.Count > 0)
                                {
                                    if (pedestrianlaneInGroup.LaneDirectionFrom == Direction.North)
                                    {
                                        float distance = pedestrianlaneInGroup.Pedestrians.Last().Location.Y - pedestrianlaneInGroup.StartPoint.Y;
                                        if (distance < 10) distanceBetweenPedestrian += Math.Abs(15 - distance);
                                    }
                                    else if (pedestrianlaneInGroup.LaneDirectionFrom == Direction.South)
                                    {
                                        float distance = pedestrianlaneInGroup.StartPoint.Y - pedestrianlaneInGroup.Pedestrians.Last().Location.Y;
                                        if (distance < 10) distanceBetweenPedestrian += Math.Abs(15 - distance);
                                    }
                                    else if (pedestrianlaneInGroup.LaneDirectionFrom == Direction.West)
                                    {
                                        float distance = pedestrianlaneInGroup.Pedestrians.Last().Location.X - pedestrianlaneInGroup.StartPoint.X;
                                        if (distance < 10) distanceBetweenPedestrian += Math.Abs(15 - distance);
                                    }
                                    else if (pedestrianlaneInGroup.LaneDirectionFrom == Direction.East)
                                    {
                                        float distance = pedestrianlaneInGroup.StartPoint.X - pedestrianlaneInGroup.Pedestrians.Last().Location.X;
                                        if (distance < 10) distanceBetweenPedestrian += Math.Abs(15 - distance);
                                    }
                                }
                            }
                        }
                        else
                        {
                            distanceBetweenPedestrian += 15;
                        }
                        if (pl.LaneDirectionFrom == Direction.North)
                        {
                            p = new Pedestrian(Color.Black, pl.StartPoint.X, pl.StartPoint.Y - distanceBetweenPedestrian, 6, 6, pl);
                            pl.Pedestrians.Add(p); // Create the next car 10 pixels behind 
                        }
                        else if (pl.LaneDirectionFrom == Direction.South)
                        {
                            p = new Pedestrian(Color.Black, pl.StartPoint.X, pl.StartPoint.Y + distanceBetweenPedestrian, 6, 6, pl);
                            pl.Pedestrians.Add(p); // Create the next car 10 pixels behind 
                        }
                        else if (pl.LaneDirectionFrom == Direction.West)
                        {
                            p = new Pedestrian(Color.Black, pl.StartPoint.X - distanceBetweenPedestrian, pl.StartPoint.Y, 6, 6, pl);
                            pl.Pedestrians.Add(p); // Create the next car 10 pixels behind 
                        }
                        else if (pl.LaneDirectionFrom == Direction.East)
                        {
                            p = new Pedestrian(Color.Black, pl.StartPoint.X + distanceBetweenPedestrian, pl.StartPoint.Y, 6, 6, pl);
                            pl.Pedestrians.Add(p); // Create the next car 10 pixels behind 
                        }
                        //if (mediator != null) mediator.Clients.Add(p);
                    }
                    break;
                }
            }
        }

    }
} 
