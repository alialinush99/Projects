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
    public class RoadType1 : Road
    {
        public RoadType1(Point location, PictureBox pb_background)
           : base(location, pb_background)
        {
            this.type = "1";
            //group 1
            TrafficLight leftUpper = new TrafficLight(new Point(66, 96));
            TrafficLight rightUpper = new TrafficLight(new Point(126, 74));

            //group 2
            TrafficLight leftDown = new TrafficLight(new Point(66, 118));
            TrafficLight rightDown = new TrafficLight(new Point(126, 96));

            //group 3
            TrafficLight downRight = new TrafficLight(new Point(112, 123));
            TrafficLight topLeft = new TrafficLight(new Point(81, 68));

            //TrafficLight For Pedestrian
            TrafficLight pdLeftTop = new TrafficLight(new Point(55, 63));
            TrafficLight pdRightTop = new TrafficLight(new Point(133, 37));
            TrafficLight pdLeftDown = new TrafficLight(new Point(62, 154));
            TrafficLight pdRightDown = new TrafficLight(new Point(138, 130));

            //adding groups
            trafficGroups.AddToGroup1(leftUpper);
            trafficGroups.AddToGroup1(rightDown);
            trafficGroups.AddToGroup2(leftDown);
            trafficGroups.AddToGroup2(rightUpper);
            trafficGroups.AddToGroup3(downRight);
            trafficGroups.AddToGroup3(topLeft);

            trafficGroups.AddToPedestrianTLG1(pdLeftTop);
            trafficGroups.AddToPedestrianTLG1(pdRightTop);
            trafficGroups.AddToPedestrianTLG2(pdLeftDown);
            trafficGroups.AddToPedestrianTLG2(pdRightDown);

            trafficGroups.SetLight(trafficGroups.GetGroup1(), true);
            base.AddRoad();
        }
    }
}
