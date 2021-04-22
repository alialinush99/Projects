using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
    
    public class Simulation
    {
        private string name;
        private double time; 
        private bool isRunning;
        private Grid grid;

        public string Name { get => name; set => name = value; }
        public double Time { get => time; set => time = value; }
        public bool IsRunning { get => isRunning; set => isRunning = value; }
        internal Grid Grid { get => grid; set => grid = value; }

        public Simulation(string Name, double time)
        {
            this.name = Name;
            this.time = time;
            this.isRunning = false;
        }

        public void Start()
        {
            this.isRunning = true;
        }

        public void Pause()
        {
            this.isRunning = false;
        }

        public void Resume()
        {
            this.isRunning = true;
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        
    }
}
