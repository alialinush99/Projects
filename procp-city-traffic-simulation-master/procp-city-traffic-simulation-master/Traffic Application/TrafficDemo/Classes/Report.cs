using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation { get; set; }
        public string GridSize { get; set; }
        public TimeSpan PlannedTime { get; set; }
        public TimeSpan ActualTIme { get; set; }
        public int NumberOfRoads { get; set; }
        public int CarsEntered { get; set; }
        public int CarsLeft { get; set; }
        public TimeSpan EADrivingTime { get; set; }
        public int PedestriansEntered { get; set; }
        public int PedestriansLeft { get; set; }
        public string SPA { get; set; }
        public string SurpriseElements { get; set; }
        public int Accidents { get; set; }
        public Report(string name, DateTime creation, string gridSize, string ptime, string atime, int nrOfRoads, int carsEnt, int carsLeft,
            string EAD, int pedestriansE, int pedestriansL, string SPA, string surpriseEl, int accidents)
        {
            this.Name = name;
            this.Creation = creation;
            this.GridSize = gridSize;
            this.PlannedTime = TimeSpan.Parse(ptime);
            this.ActualTIme = TimeSpan.Parse(atime);
            this.NumberOfRoads = nrOfRoads;
            this.CarsEntered = carsEnt;
            this.CarsLeft = carsLeft;
            this.EADrivingTime = TimeSpan.Parse(EAD);
            this.PedestriansEntered = pedestriansE;
            this.PedestriansLeft = pedestriansL;
            this.SPA = SPA;
            this.SurpriseElements = surpriseEl;
            this.Accidents = accidents;
        }
    }
}
