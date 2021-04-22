using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficDemo.Classes;

namespace TrafficDemo
{
    public partial class Form3 : MaterialSkin.Controls.MaterialForm
    {
        Simulation mySimulation;
        List<Road> Roads;
        Bitmap typeOfroad;
        Road selectedRoad;
        public Form3(Simulation s,PictureBox p, Bitmap bm, int selectedCell)
        {
            InitializeComponent();
            pbRoad.Image = p.Image;
            mySimulation = s;
            typeOfroad = bm;
            if (typeOfroad.Tag.Equals("type1"))
            {
                this.Text = "Road Type 1";
            }
            else
            {
                this.Text = "Road Type 2";
            }

            selectedRoad = s.Grid.Cells[selectedCell - 1].Road;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedRoad != null)
            {
                selectedRoad.trafficGroups.DurationGroup1 = Convert.ToInt32(tbTrafficLightTime.Text);
                selectedRoad.trafficGroups.DurationGroup2 = Convert.ToInt32(tbTrafficLightTime.Text);
                selectedRoad.trafficGroups.DurationGroup3 = Convert.ToInt32(tbTrafficLightTime.Text);
                selectedRoad.trafficGroups.DurationGroup4 = Convert.ToInt32(tbTrafficLightTime.Text);
                MessageBox.Show("SAVED");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
