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
   public class RoadType2 : Road
    {
        public RoadType2(Point location, PictureBox pb_background)
           : base(location, pb_background)
        {
            this.type = "2";

            TrafficLight leftUpper = new TrafficLight(new Point(64, 96));
            TrafficLight rightDown = new TrafficLight(new Point(128, 96));
            TrafficLight topLeft = new TrafficLight(new Point(75, 65));
            TrafficLight downRight = new TrafficLight(new Point(119, 128));

            //group 2
            TrafficLight leftDown = new TrafficLight(new Point(64, 118));
            TrafficLight rightUpper = new TrafficLight(new Point(128, 75));
            TrafficLight topRight = new TrafficLight(new Point(97, 65));
            TrafficLight downLeft = new TrafficLight(new Point(97, 128));

            //adding traffic light groups

            trafficGroups.AddToGroup1(leftUpper);
            trafficGroups.AddToGroup1(rightDown);
            trafficGroups.AddToGroup2(leftDown);
            trafficGroups.AddToGroup2(rightUpper);
            trafficGroups.AddToGroup3(downRight);
            trafficGroups.AddToGroup3(topLeft);
            trafficGroups.AddToGroup4(downLeft);
            trafficGroups.AddToGroup4(topRight);

            trafficGroups.SetLight(trafficGroups.GetGroup1(), true);
            base.AddRoad();
        }
    }
}
