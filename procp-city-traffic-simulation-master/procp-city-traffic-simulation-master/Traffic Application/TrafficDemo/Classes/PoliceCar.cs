using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
    class PoliceCar : Car
    {
        public PoliceCar(Color c, float X, float Y, float Height, float Width, Lane cLane) : base(c, X, Y, Height, Width, cLane)
        {
        }
    }
}
