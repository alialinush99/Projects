using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficDemo.Classes
{
    [Serializable]
    public class Mediator
    {
        private List<Car> clients;
        private List<Road> controlledRoads;
        private bool surpriseElements;
        private Dictionary<Lane, Point> crashLocations;
        private EmergencyVehicle eV;
        private int crashesCount = 0;
        internal List<Car> Clients { get => clients; set => clients = value; }
        internal List<Road> ControlledRoads { get => controlledRoads; set => controlledRoads = value; }
        public bool SurpriseElements { get => surpriseElements; set => surpriseElements = value; }
        internal Dictionary<Lane, Point> CrashLocations { get => crashLocations; set => crashLocations = value; }
        internal EmergencyVehicle EV { get => eV; set => eV = value; }
        public int CrashesCount { get => crashesCount; set => crashesCount = value; }

        public delegate void Notication(string message);
        [field: NonSerialized] public event Notication CarsCrashed;
        [field: NonSerialized] public event Notication CarReachedEnd;
        

        public Mediator()
        {
            clients = new List<Car>(); // Creates an empty list of cars 
            controlledRoads = new List<Road>(); // Creates an empty list of lanes 
            crashLocations = new Dictionary<Lane, Point>();
        }


        public void SpawnEmergencyVehicle(Point start,  Point crash ,List<Node> pathFromStartToCrash, List<Node> pathFromCrashToEnd, Point end)
        {

            this.EV = new EmergencyVehicle( start.X, start.Y, 15, 15, this, pathFromStartToCrash, crash, pathFromCrashToEnd ,end);

        }

        public void EmergencyVehicleArrived (EmergencyVehicle ev)
        {
            this.EV = null;
            this.crashLocations.Remove(this.crashLocations.First().Key);
        }



 

        public void AddRoad(Road r)
        {
            this.controlledRoads.Add(r);
            r.AssignMediator(this);

           /* foreach (string g in r.lanesGroups.Keys)
            {
                foreach (Lane L in r.lanesGroups[g])
                {
                    foreach (Car C in L.Cars)
                    {
                        clients.Add(C); // Adds every car in the lane to the list of cars we have
                    }
                }
            }*/
        }

        public void RemoveRoad(Road r)
        {

            this.controlledRoads.Remove(r);
            //     L.Mediator = null; // Keeps a reference of the mediator in the lane
            foreach (string g in r.lanesGroups.Keys)
            {
                foreach (Lane L in r.lanesGroups[g])
                {
                    foreach (Car C in L.Cars)
                    {
                        clients.Remove(C); // Removes cars from that road
                    }
                }
            }
        }

        public string LiveFeed(string Action)
        {
            string Report = "";
            if (Action == "crash")
            {
                Report = "Cars have crashed into each other!";
            }

            if (Action == "EndLane")
            {
                Report = "Car has reached the end of the lane";
            }
            return Report;
        }


        public string SendMessage(string Message, Car Sender, PointF target)
        {
            // Response to whether the car should keep moving or not (The default being yes):
            string Response = "Yes";
            // Data that we need to decide if the car should keep moving or not: 

            Road currentRoad = Sender.CurrentLane.ParentRoad;  // 4- Which road is the car on right now
            PointF stopHere = Sender.CurrentLane.StoppingPoint; // 2- Stopping point of the lane that the car is on
            TrafficLight trafficLight = Sender.CurrentLane.TLight; // 3- Traffic light of the lane that the car is on  
            List<Car> otherCars = clients.FindAll(c => c.CurrentLane.ParentRoad == currentRoad && c != Sender); // 1- Cars on the same roadpiece 

            //  We have all the data we need, the following code is the logic of "Should the car stop moving?":
            if (target.X == Sender.Location.X)
            {
                if (target.Y < Sender.Location.Y)  // Checks if the car is moving North
                {
                    foreach (Car otherCar in otherCars)
                    {
                        if (otherCar != Sender)
                        {
                            if (otherCar.Location.X == Sender.Location.X && otherCar.Location.Y < Sender.Location.Y && Sender.Location.Y - otherCar.Location.Y < 10)
                            {
                                return "No";
                            }
                        }
                    }
                }

                if (target.Y > Sender.Location.Y)  // Checks if the car is moving South
                {
                    foreach (Car otherCar in otherCars)
                    {
                        if (otherCar != Sender)
                        {
                            if (otherCar.Location.X == Sender.Location.X && otherCar.Location.Y > Sender.Location.Y && otherCar.Location.Y - Sender.Location.Y < 10)
                            {
                                return "No";
                            }
                        }
                    }
                }
            }

            // Stop here

            if (target.Y == Sender.Location.Y) // Checks if the car should be moving on the X axis
            {
                if (target.X > Sender.Location.X)  // Checks if the car is moving east
                {
                    foreach (Car otherCar in otherCars)
                    {
                        if (otherCar != Sender)
                        {
                            if (otherCar.Location.Y == Sender.Location.Y && otherCar.Location.X > Sender.Location.X && otherCar.Location.X - Sender.Location.X < 10) // Checks if there's a car too close infront of it 
                            {
                                return "No";
                            }
                        }
                    }
                }

                if (target.X < Sender.Location.X)  // Checks if the car is moving west
                {
                    foreach (Car otherCar in otherCars)
                    {
                        if (otherCar != Sender)
                        {
                            if (otherCar.Location.Y == Sender.Location.Y && otherCar.Location.X < Sender.Location.X && Sender.Location.X - otherCar.Location.X < 10) // Checks if there's a car too close infront of it 
                            {
                                return "No";
                            }
                        }
                    }
                }
            }
            if (trafficLight != null) {
                if (Sender.Location == stopHere && trafficLight.IsGreen == false)
                {
                    return "No";
                }
            }


            foreach (Car C1 in otherCars)
            {
                if (C1 != Sender)
                {
                    // Math mothefucker
                    double distanceBetweenCars = Math.Sqrt(Math.Pow((C1.Location.X - Sender.Location.X), 2) + Math.Pow((C1.Location.Y - Sender.Location.Y), 2));
                    if (distanceBetweenCars < 6)
                    {
                        CarsCrashed("Cars have crashed into each other!");
                        // TODO change how this work so we can track more crashes on the same lane 
                        if (crashLocations.ContainsKey(Sender.CurrentLane) == false)
                        {
                            crashLocations.Add(Sender.CurrentLane, new Point(Convert.ToInt32(Sender.Location.X) + Sender.CurrentLane.ParentRoad.Pb_Background.Location.X, Convert.ToInt32(Sender.Location.Y) + Sender.CurrentLane.ParentRoad.Pb_Background.Location.Y));
                            crashesCount += 1;

                        }
                    }
                }
            }
            PointF EndPointLocationOnScreen;
            EndPointLocationOnScreen = Sender.CurrentLane.EndPointsOnScreen[1];
            if (Sender.Location == Sender.CurrentLane.EndPoint)
            {
                   
                Sender.CurrentLane.RemoveSpecificCar(Sender);
                CarReachedEnd("A car has reached the end of the lane.");
                clients.Remove(Sender);
                var roadsToCheck = controlledRoads.Where(c => c != Sender.CurrentLane.ParentRoad);
                foreach (Road R in roadsToCheck)
                {
                    foreach (string group in R.lanesGroups.Keys)
                    {
                        foreach (Lane L in R.lanesGroups[group])
                        {
                            if (L.StartPointOnScreen == EndPointLocationOnScreen)
                            {
                                Lane newLane = R.lanesGroups[group][new Random().Next(0, R.lanesGroups[group].Count)];
                                R.AddCars(newLane,1);
                               
                                return Response;
                            }
                        }
                    }
                }
            }

            return Response;

        }

    }
}

