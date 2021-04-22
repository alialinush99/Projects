using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficDemo.Classes
{
    [Serializable]
    class TwoWayVertical : Road
    {
        public TwoWayVertical(Point location, PictureBox pb_background) : base(location, pb_background)
        {
            this.type = "two-way-v";
            AddRoad();
        }
    }
}
