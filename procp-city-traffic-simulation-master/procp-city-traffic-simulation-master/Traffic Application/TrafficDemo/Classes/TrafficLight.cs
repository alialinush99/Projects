using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;
using System.Drawing.Drawing2D;

namespace TrafficDemo.Classes
{
    [Serializable]
   public class TrafficLight
    {
        private int duration;
        private bool isGreen;
        private Color color; // not sure if we need it because we have the bool isGreen
        private Point location;
     //   private Road road; // maybe we will need this later

        // if you dont need the color property delete this constructor and use the isGreen in the mediator maybe
        public TrafficLight(Color color , Point location)
        {
            this.color = color;
            this.duration = 5;
            this.isGreen = false;
            this.location = location;
        }
        public TrafficLight(Point location)
        {
            this.duration = 5;
            this.isGreen = false;
            this.location = location;
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public bool IsGreen
        {
            get { return isGreen; }
            set { isGreen = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Draws the traffic lights in the crossing
        /// </summary>
        /// <param name="gr">The graphics where the trafficlight will be drawn</param>
        public void Draw(Graphics gr)
        {
            if (isGreen)
            {
                SolidBrush myBrush = new SolidBrush(Color.Green);
                gr.FillEllipse(myBrush, location.X, location.Y, 7, 7);
            }
            else
            {
                SolidBrush myBrush = new SolidBrush(Color.Red);
                gr.FillEllipse(myBrush, location.X, location.Y, 7, 7);
            }
        }
    }
}
