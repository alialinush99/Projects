using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
    public interface IAStarAlgorithm
    {
         List<Node> GetShortestPathAstar();
         void AstarSearch();
    }
}
