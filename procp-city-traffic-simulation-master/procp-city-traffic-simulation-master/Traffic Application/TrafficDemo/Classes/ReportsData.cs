using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
   public class ReportsData
    {
        public Dictionary<string, List<Report>> Reports { get; set; }

        public ReportsData()
        {
            this.Reports = new Dictionary<string, List<Report>>();
        }
        public Report GetReortByName(string name)
        {
            foreach (var values in Reports.Values)
            {
                foreach (var report in values)
                {
                    if (report.Name.Equals(name))
                    {
                        return report;
                    }
                }
                return null;
            }
            return null;
        }
        public int GetSmallGridSimulationsCount()
        {
            int count = 0;
            foreach (var key in Reports.Keys)
            {
                count += Reports[key].FindAll(r => r.GridSize == "Small").Count();
            }
            return count;
        }
        public int GetMediumGridSimulationsCount()
        {
            int count = 0;
            foreach (var key in Reports.Keys)
            {
                count += Reports[key].FindAll(r => r.GridSize == "Medium").Count();
            }
            return count;
        }
        public int GetLargeGridSimulationsCount()
        {
            int count = 0;
            foreach (var key in Reports.Keys)
            {
                count += Reports[key].FindAll(r => r.GridSize == "Large").Count();
            }
            return count;
        }

        public double GetCarsEntered()
        {
            double TotalCarsEntered = 0;
            Reports.Values.ToList().ForEach(v => v.ForEach(r => TotalCarsEntered += r.CarsEntered));
            return TotalCarsEntered;
        }

        public double GetCarsLeft()
        {
            double TotalCarsLeft = 0;
            Reports.Values.ToList().ForEach(v => v.ForEach(r => TotalCarsLeft += r.CarsLeft));
            return TotalCarsLeft;
        }
        public double GetSimulationsCount()
        {
            double simulationsCount = 0;
            Reports.Values.ToList().ForEach(v => simulationsCount += v.Count);
            return simulationsCount;
        }
        public double GetSimulationsWithAccidentsCount()
        {
            double totalSimulationsWithAccidents = 0;
            Reports.Values.ToList().ForEach(v => v.ForEach(r => {
                if (r.Accidents > 0)
                {
                    totalSimulationsWithAccidents++;
                }
            }));
            return totalSimulationsWithAccidents;
        }

        public TimeSpan[] GetAlgorithmsTiming()
        {
            TimeSpan dijkstraTiming = new TimeSpan();
            TimeSpan aStarTiming = new TimeSpan();
            Reports.Values.ToList().ForEach(v => v.ForEach(r => {
                if (r.SPA == "Dijkstra")
                {
                    dijkstraTiming += r.EADrivingTime;
                }
                else
                {
                    aStarTiming += r.EADrivingTime;
                }
            }));
            return new TimeSpan[] { dijkstraTiming, aStarTiming };
        }
        public TimeSpan GetActualTiming()
        {
            TimeSpan actualTime = new TimeSpan();
            Reports.Values.ToList().ForEach(v => v.ForEach(r => actualTime += r.ActualTIme));
            return actualTime;
        }
        public TimeSpan GetPlannedTiming()
        {
            TimeSpan plannedTime = new TimeSpan();
            Reports.Values.ToList().ForEach(v => v.ForEach(r => plannedTime += r.PlannedTime));
            return plannedTime;
        }
    }
}
