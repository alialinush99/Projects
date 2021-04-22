using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDemo.Classes
{
    [Serializable]
    public class TrafficLightsGroups
    {
        private List<TrafficLight> group1;
        private List<TrafficLight> group2;
        private List<TrafficLight> group3;
        private List<TrafficLight> group4;
        private List<TrafficLight> pedestriantlg1;
        private List<TrafficLight> pedestriantlg2;
        private int durationGrp1 = 5000;
        private int durationGrp2 = 5000;
        private int durationGrp3 = 5000;
        private int durationGrp4 = 5000;
        private int durationpedestriantlg1 = 5000;
        private int durationpedestriantlg2 = 5000;
        //wait time between changing phases (between green and red light of the current grp and the next grp)
        private int delay = 3000;

        private int counter = 0;
        private string phase = "1";

        public TrafficLightsGroups()
        {
            group1 = new List<TrafficLight>();
            group2 = new List<TrafficLight>();
            group3 = new List<TrafficLight>();
            group4 = new List<TrafficLight>();
            pedestriantlg1 = new List<TrafficLight>();
            pedestriantlg2 = new List<TrafficLight>();
        }

        public int DurationGroup1
        {
            get { return durationGrp1; }
            set { durationGrp1 = value; }
        }

        public int DurationGroup2
        {
            get { return durationGrp2; }
            set { durationGrp2 = value; }
        }

        public int DurationGroup3
        {
            get { return durationGrp3; }
            set { durationGrp3 = value; }
        }

        public int DurationGroup4
        {
            get { return durationGrp4; }
            set { durationGrp4 = value; }
        }
        public int DurationPedestrianTlG1
        {
            get { return durationpedestriantlg1; }
            set { durationpedestriantlg1 = value; }
        }

        public int DurationPedestrianTlG2
        {
            get { return durationpedestriantlg2; }
            set { durationpedestriantlg2 = value; }
        }

        public string Phase
        {
            get { return phase; }
        }

        public List<TrafficLight> GetGroup1()
        {
            return group1;
        }

        public List<TrafficLight> GetGroup2()
        {
            return group2;
        }

        public List<TrafficLight> GetGroup3()
        {
            return group3;
        }

        public List<TrafficLight> GetGroup4()
        {
            return group4;
        }

        public List<TrafficLight> GetPedestrianTlG1()
        {
            return pedestriantlg1;
        }

        public List<TrafficLight> GetPedestrianTlG2()
        {
            return pedestriantlg2;
        }


        /// <summary>
        /// Changes the phase if the counter reaches the duration of a particular grp
        /// </summary>
        /// <returns></returns>
        public bool Count()
        {
            //On every 1 sec
            counter += 1000;
            if (phase == "1")
            {
                if (counter == durationGrp1)
                {
                    ChangePhaseRed();
                    return false;
                }
                else if (counter == durationGrp1 + delay)
                {
                    ChangePhaseGreen();
                    return true;
                }
            }
            else if (phase == "2")
            {
                if (counter == durationGrp2)
                {
                    ChangePhaseRed();
                    return false;
                }
                else if (counter == durationGrp2 + delay)
                {
                    ChangePhaseGreen();
                    return true;
                }
            }
            else if (phase == "3")
            {
                if (counter == durationGrp3)
                {
                    ChangePhaseRed();
                    return false;
                }
                else if (counter == durationGrp3 + delay)
                {
                    ChangePhaseGreen();
                    return true;
                }
            }
            else if (phase == "4")
            {
                if (counter == durationGrp4)
                {
                    ChangePhaseRed();
                    return false;
                }
                else if (counter == durationGrp4 + delay)
                {
                    ChangePhaseGreen();
                    return true;
                }
            }
            else if (phase == "0")
            {
                if (counter == DurationPedestrianTlG1)
                {
                    ChangePhaseRed();
                    ChangePhaseGreen();
                    return false;
                }
                else if (counter == durationGrp4 + delay)
                {
                    ChangePhaseGreen();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Switches between the different traffic light groups to green
        /// </summary>
        public void ChangePhaseGreen()
        {
            counter = 0;
            if (phase == "1")
            {
                phase = "2";
                this.SetLight(GetGroup2(), true);
            }
            else if (phase == "2")
            {
                phase = "3";
                this.SetLight(GetGroup3(), true);

            }
            else if (phase == "3")
            {
                if (group4.Count == 0)
                {
                    if (this.pedestriantlg1.Count() != 0 || this.pedestriantlg2.Count() != 0)
                    {
                        this.SetCarTrafficLightsToRed();
                        this.SetLight(GetPedestrianTlG1(), true);
                        this.SetLight(GetPedestrianTlG2(), true);
                        this.phase = "0";
                    }
                    else
                    {
                        phase = "1";
                        this.SetLight(GetGroup1(), true);
                    }
                }
                else
                {
                    phase = "4";
                    this.SetLight(GetGroup4(), true);
                }
            }
            else if (phase == "4")
            {
                if (this.pedestriantlg1.Count() != 0 || this.pedestriantlg2.Count() != 0)
                {
                    this.SetCarTrafficLightsToRed();
                    this.SetLight(GetPedestrianTlG1(), true);
                    this.SetLight(GetPedestrianTlG2(), true);
                    this.phase = "0";
                }
                else
                {
                    phase = "1";
                    this.SetLight(GetGroup1(), true);
                }
            }
            else if (phase == "0")
            {
                phase = "1";
                this.SetLight(GetGroup1(), true);
            }
        }


        /// <summary>
        /// Switches the traffic lights of a particular grp to red
        /// </summary>
        public void ChangePhaseRed()
        {
            if (phase == "1")
            {
                this.SetLight(GetGroup1(), false);
            }
            else if (phase == "2")
            {
                this.SetLight(GetGroup2(), false);
            }
            else if (phase == "3")
            {
                this.SetLight(GetGroup3(), false);
            }
            else if (phase == "4")
            {
                this.SetLight(GetGroup4(), false);
            }
        }
        public void SetCarTrafficLightsToRed()
        {

            this.SetLight(GetGroup1(), false);


            this.SetLight(GetGroup2(), false);


            this.SetLight(GetGroup3(), false);


            this.SetLight(GetGroup4(), false);
        }


        /// <summary>
        /// Sets the light of a grp
        /// </summary>
        /// <param name="TLGroup">List of the trafficlights</param>
        /// <param name="IsGreenLight">Boolean that checks if the light is green</param>
        public void SetLight(List<TrafficLight> TLGroup, bool IsGreenLight)
        {
            foreach (TrafficLight trafficLight in TLGroup)
            {
                trafficLight.IsGreen = IsGreenLight;
            }
        }

        public void AddToGroup1(TrafficLight tl)
        {
            group1.Add(tl);
        }

        public void AddToGroup2(TrafficLight tl)
        {
            group2.Add(tl);
        }

        public void AddToGroup3(TrafficLight tl)
        {
            group3.Add(tl);
        }

        public void AddToGroup4(TrafficLight tl)
        {
            group4.Add(tl);
        }
        public void AddToPedestrianTLG1(TrafficLight tl)
        {
            pedestriantlg1.Add(tl);
        }
        public void AddToPedestrianTLG2(TrafficLight tl)
        {
            pedestriantlg2.Add(tl);
        }
    }
}
