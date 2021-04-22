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
    class Roundabout : Road
    {
        public Roundabout(Point location, PictureBox pb_background) : base(location, pb_background)
        {
            this.type = "roundabout";
            //TrafficLight For Pedestrian
            TrafficLight pdLeftTop = new TrafficLight(new Point(55, 63));
            TrafficLight pdRightTop = new TrafficLight(new Point(133, 37));
            TrafficLight pdLeftDown = new TrafficLight(new Point(62, 154));
            TrafficLight pdRightDown = new TrafficLight(new Point(138, 130));
            trafficGroups.AddToPedestrianTLG1(pdLeftTop);
            trafficGroups.AddToPedestrianTLG1(pdRightTop);
            trafficGroups.AddToPedestrianTLG2(pdLeftDown);
            trafficGroups.AddToPedestrianTLG2(pdRightDown);
            trafficGroups.SetLight(trafficGroups.GetPedestrianTlG1(), false);
            trafficGroups.SetLight(trafficGroups.GetPedestrianTlG2(), false);
            AddRoad();
        }
    }
}
