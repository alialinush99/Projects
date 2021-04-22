using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficDemo.Classes
{
    [Serializable]
   public class Grid
    {
        private List<Cell> cells;
        private List<Road> roads;
        [NonSerialized] private Timer car_timer;
        private long time;
        private int simSpeed;
        public Grid(int rows, int columns)
        {
            this.cells = new List<Cell>();
            this.roads = new List<Road>();
            this.AddCells(rows, columns);
            simSpeed = 1;
            InitializeCarTimer();
        }
        public int SimSpeed
        {
            get { return simSpeed; }
            set { _ = value <= 0 ? throw new ArgumentException("Invalid simulation speed value.") : simSpeed = value; } 
        }
        public List<Cell> Cells
        {
            get
            {
                return cells;
            }
        }

        public List<Road> Roads
        {
            get
            {
                return roads;
            }
        }

        public Dictionary<Lane, List<Point>> GetAllPointsOnGrid()
        {
            Dictionary<Lane, List<Point>> dictionary = new Dictionary<Lane, List<Point>>();
            List<Point> pointsOnGrid = new List<Point>();
            foreach (Cell C in this.cells)
            {
                if (C.Road != null)
                {

                    foreach (Lane L in C.Road.Lanes)
                    {
                        pointsOnGrid.Add(L.StartPointOnScreen);

                        if (L.EdgesOnScreen.Count > 0)
                        {
                            foreach (Point edge in L.EdgesOnScreen)
                            {
                                pointsOnGrid.Add(edge);
                            }
                        }

                        if (L.EndPointsOnScreen.Count > 0)
                        {
                            foreach (KeyValuePair<int, Point> combo in L.EndPointsOnScreen)
                            {
                                pointsOnGrid.Add(combo.Value);
                            }
                        }
                        dictionary.Add(L, pointsOnGrid);
                        pointsOnGrid = new List<Point>();
                    }
                }

            }



            return dictionary;
        }

        public void AddRoad(Road road,int cellIndex)
        {
            roads.Add(road);
            cells[cellIndex].Road = road;
        }

        public bool CheckSpawningLanes(Lane myLane)
        {

           foreach (Cell C in this.cells)
            {
                if (C.Road != null && C.Road != myLane.ParentRoad)
                {
                    foreach (string group in C.Road.lanesGroups.Keys)
                    {
                        foreach (Lane L in C.Road.lanesGroups[group])
                        {
                            foreach (Point endPoint in L.EndPointsOnScreen.Values)
                            {
                                if (myLane.StartPointOnScreen == endPoint)
                                {
                                    return false;
                                }
                            }

                            if (myLane.StartPointOnScreen == L.StartPointOnScreen)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        

        public void InitializeCarTimer()
        {
            car_timer = new Timer();
            car_timer.Interval = 90/simSpeed;
            car_timer.Tick += car_timer_Tick;
        }

        public void InitializeGrid()
        {
            car_timer = new Timer();
            car_timer.Interval = 90 / simSpeed;
            car_timer.Tick += car_timer_Tick;
            foreach (Road r in roads)
            {
                r.InitializeRoad();
            }
        }

        private void car_timer_Tick(object sender, EventArgs e)
        {
          
            car_timer.Interval = 90/simSpeed;
            foreach (Road r in roads)
            {
                if (r != null)
                {
                    r.Pb_Background.Refresh();
                    foreach (string g in r.lanesGroups.Keys)
                    {
                        foreach (Lane L in r.lanesGroups[g])
                        {
                            L.MoveCars();
                        }
                    }
                    foreach (string g in r.pedestriansLanesGroups.Keys)
                    {
                        foreach (PedestrianLane pl in r.pedestriansLanesGroups[g])
                        {
                            pl.MovePedastrians();
                        }
                    }

                    if (r.Mediator != null)
                    {
                        if (r.Mediator.EV != null && r.Mediator.EV.currentRoad == r)
                        {
                            if (r.Mediator.EV.Grid == null)
                            {
                                r.Mediator.EV.Grid = this;
                            }
                            
                            r.Mediator.EV.MoveToEmergency();
                        }
                    }
                }
                r.PlaceTrafficLightsOnCrossingPb();
                r.DrawLanesAndCars();
                r.DrawPedestriansLanesAndPedestrians();
            }
            time += (90 / simSpeed);
        }

        /// <summary>
        /// Starting the car timer and all the traffic light timers
        /// </summary>
        public bool StartTimers()
        {
            car_timer.Start();
            foreach (Road r in roads)
            {
                r.StartTLTimer();
            }
            return true;
        }
        /// <summary>
        /// Pausing the car timer and stopping all the traffic light timers
        /// </summary>
        public bool PauseTimers()
        {
            car_timer.Stop();
            foreach (Road r in roads)
            {
                r.StopTLTimer();
            }
            return true;
        }

        /// <summary>
        /// Stopping all the timers and removing the cars from all the lists of the lanes
        /// </summary>
        public bool StopSimulation()
        {
            car_timer.Stop();
            time = 0;
            foreach (Road r in roads)
            {
                //todo: remove the cars from the lanes
                r.StopTLTimer();
                foreach (Lane L in r.Lanes)
                {
                    L.RemoveAllCars();
                }
            }
            return true;
        }

        public void AddCells(int rows, int columns)
        {
            int cellWidth = 200;
            int cellHeight = 200;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.Cells.Add(new Cell(new Point(0 + j * cellWidth, 0 + i * cellHeight)));
                }
            }
        }
        public bool RemoveCrossing(int selectedCell)
        {
            Road selectedRoad = this.cells[selectedCell - 1].Road;
            if (selectedRoad != null)
            {
                selectedRoad.Pb_Background.Dispose();
                roads.Remove(selectedRoad);
                cells[selectedCell - 1].Taken = false;
                cells[selectedCell - 1].Road = null;

                return true;
            }
            return false;
        }
        public int GetCarFlow()
        {
            int carsInSim = 0;
            foreach (Road r in roads)
            {
                carsInSim += r.CarsOnRoad;
            }
            return carsInSim;
        }

    }
}
