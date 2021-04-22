using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TrafficDemo.Classes
{
    [Serializable]
    public class Cell
    {
       // public enum Location { topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight, bottomLeft, bottomCenter, bottomRight }
        private float height;
        private float width;
        private PointF startingPoint;
        private Point location;
        //private Location locationOnGrid;
        private Road road;
        private bool taken;

        public float Height { get => height; set => height = value; }
        public float Width { get => width; set => width = value; }
       // internal Location LocationOnGrid { get => locationOnGrid; set => locationOnGrid = value; }
        internal Road Road { get => road; set => road = value; }
        public Point Location { get => this.location; set => this.location = value; }
        public PointF StartingPoint { get => startingPoint; set => startingPoint = value; }
        public bool Taken { get => this.taken;  set => this.taken = value; }

        public Cell(Point location)
        {
            this.location = location;
            this.taken = false;
        }

    }
}
