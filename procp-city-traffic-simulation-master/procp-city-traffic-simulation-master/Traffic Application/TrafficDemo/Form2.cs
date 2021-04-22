using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficDemo.Classes;
using MaterialSkin;
using MaterialSkin.Controls;
using MaterialSkin.Animations;
namespace TrafficDemo
{
    
    public partial class Form2 : MaterialSkin.Controls.MaterialForm
    {
        public static string dirParameter = "";
        public Form2(double duration,double initialDuration,int nrCarsEntered,Grid grid, Mediator mediator, double averageEmergencyTime, int crashesCount, string algorithm)
        {
            InitializeComponent();
            TimeSpan time = TimeSpan.FromSeconds(initialDuration);
            string strInitialTime = time.ToString(@"hh\:mm\:ss");

            time = TimeSpan.FromSeconds(duration);
            string strActualTime = time.ToString(@"hh\:mm\:ss");

            lbActualTime.Text = strActualTime;
            lbPlannedRuntime.Text = strInitialTime;
            lbResultCarsEntered.Text = nrCarsEntered.ToString();

            lbCarsLeft.Text = (nrCarsEntered - grid.GetCarFlow()) + "";
            if (grid.Cells.Count == 6) lbGridSize.Text = "Small";
            else if (grid.Cells.Count == 9) lbGridSize.Text = "Medium";
            else lbGridSize.Text = "Large";

            lbNrOfRoads.Text = grid.Roads.Count().ToString();
            lbResSurpriseElem.Text = mediator.SurpriseElements.ToString();
            lbAccidents.Text = crashesCount.ToString();

            time = TimeSpan.FromSeconds(averageEmergencyTime);
            string strAverageEmergencyTime = time.ToString(@"hh\:mm\:ss");

            lbEmergencyCarTime.Text = strAverageEmergencyTime;
            lbSPAlgorithm.Text = algorithm;

        }
        private void saveFile()
        {
            string Msg = "Grid Size " + lbGridSize.Text + "\n" + "Planned Runtime " + lbPlannedRuntime.Text + "\n" + "Actual Runtime " + lbActualTime.Text + "\n"
                + "Number of Roads " + lbNrOfRoads.Text + "\n" + "Cars Entered " + lbResultCarsEntered.Text + "\n" + "Cars Left " + lbCarsLeft.Text + "\n" 
                + "Avarage Emergency Car Driving Time " + lbEmergencyCarTime.Text +"\n" + "Pedestrians Entered " + lbResPedesEntered.Text + "\n"
                + "Pedestrians Left " + lbPedestriansLeft.Text + "\n"
                + "Shortest Path Algorithm " + lbSPAlgorithm.Text + "\n" + "Suprised Elements " + lbResSurpriseElem.Text + "\n" + "Accidents " + lbAccidents.Text;
            // Save File to .txt  
            FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);
            m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
            m_WriterParameter.Write(Msg);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();
            MessageBox.Show("Successfully Saved!");
        }

        private void lbSaveReport_MouseHover(object sender, EventArgs e)
        {
            lbSaveReport.Font = new Font(lbSaveReport.Font, FontStyle.Underline | FontStyle.Bold);
            lbSaveReport.ForeColor = Color.Black;
        }

        private void lbSaveReport_MouseLeave(object sender, EventArgs e)
        {
            lbSaveReport.Font = new Font(lbSaveReport.Font, FontStyle.Bold);
            lbSaveReport.ForeColor = Color.DodgerBlue;
        }

        private void lbSaveReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = @"C:\";      
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dirParameter = saveFileDialog1.FileName;
                try
                {
                    if (lbPlannedRuntime.Text != null && lbActualTime.Text != null && lbResultCarsEntered.Text != null && lbCarsLeft.Text != null
                        && lbResPedesEntered.Text != null && lbPedestriansLeft.Text != null && lbResSurpriseElem.Text != null && lbAccidents.Text != null
                        && lbGridSize.Text != null && lbSPAlgorithm.Text != null && lbEmergencyCarTime.Text != null && lbNrOfRoads != null)
                    {
                        saveFile();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
