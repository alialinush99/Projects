using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficDemo.Classes;

namespace TrafficDemo
{
    public partial class StatisticsView : MaterialSkin.Controls.MaterialForm
    {
        ReportsData data;
        GraphsUtils graphs;
        public StatisticsView()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            groupBox1.Height = Screen.PrimaryScreen.Bounds.Height;
            dataGridView1.Height = Screen.PrimaryScreen.Bounds.Height;
            int panelOffsetwidth = Screen.PrimaryScreen.Bounds.Width - panel1.Width;
            int panelOffsetheight = Screen.PrimaryScreen.Bounds.Height - panel1.Height;

            panel1.Size = new Size(panel1.Width + panelOffsetwidth, panel1.Height + panelOffsetheight);

            int navigationOffset = Screen.PrimaryScreen.Bounds.Width - btnExport.Location.X - 200;

            btnExport.Location = new Point(btnExport.Location.X + navigationOffset, btnExport.Location.Y);
            btnCompare.Location = new Point(btnCompare.Location.X + navigationOffset, btnExport.Location.Y);
            btnLoadReports.Location = new Point(btnLoadReports.Location.X + navigationOffset, btnExport.Location.Y);
            btnOverall.Location = new Point(btnOverall.Location.X + navigationOffset, btnExport.Location.Y);

            data = new ReportsData();
            graphs = new GraphsUtils(data, this.panel1);
            pbLogo.Location = new Point(
            this.Width / 2 - pbLogo.Size.Width / 2,
            this.Height / 2 - pbLogo.Size.Height / 2);
            pbLogo.Anchor = AnchorStyles.None;
            pbLogo.Size = new Size(900, 700);
        }

        private void btnLoadReports_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true,
                Multiselect = true
            };

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        DateTime creation = File.GetCreationTime(file);
                        string filename = Path.GetFileName(file);
                        // Read a text file line by line.  
                        string[] lines = File.ReadAllLines(file);
                        Report r = new Report
                        (
                            filename,
                            creation,
                            lines[0].Substring(lines[0].LastIndexOf(' ')).Trim(),
                            lines[1].Substring(lines[1].LastIndexOf(' ')).Trim(),
                            lines[2].Substring(lines[2].LastIndexOf(' ')).Trim(),
                            int.Parse(lines[3].Substring(lines[3].LastIndexOf(' ')).Trim()),
                            int.Parse(lines[4].Substring(lines[4].LastIndexOf(' ')).Trim()),
                            int.Parse(lines[5].Substring(lines[5].LastIndexOf(' ')).Trim()),
                            lines[6].Substring(lines[6].LastIndexOf(' ')).Trim(),
                            int.Parse(lines[7].Substring(lines[7].LastIndexOf(' ')).Trim()),
                            int.Parse(lines[8].Substring(lines[8].LastIndexOf(' ')).Trim()),
                            lines[9].Substring(lines[9].LastIndexOf(' ')).Trim(),
                            lines[10].Substring(lines[10].LastIndexOf(' ')).Trim(),
                            int.Parse(lines[11].Substring(lines[11].LastIndexOf(' ')).Trim())
                        );
                        if (!data.Reports.ContainsKey(creation.Year.ToString()))
                        {
                            data.Reports.Add(creation.Year.ToString(), new List<Report>() { r });

                            dataGridView1.Rows.Add(new string[] { "1", filename, creation.ToString() });
                        }
                        else if (data.Reports[creation.Year.ToString()].FindAll(v => v.Name == r.Name).Count == 0)
                        {
                            data.Reports[creation.Year.ToString()].Add(r);

                            dataGridView1.Rows.Add(new string[] { "1", filename, creation.ToString() });
                        }
                        else
                        {
                            MessageBox.Show("The report with name: " + r.Name + " is already added!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pbLogo.Visible = false;
                List<Report> selected = new List<Report>();
                Int32 selectedRowCount =
                dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        string name = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                        Report r = data.GetReortByName(name);
                        if (r != null)
                        {
                            selected.Add(r);
                        }
                    }
                }
                CompareReports(selected);
            }
            else
            {
                MessageBox.Show("Please load at least one report!");
            }
        }
        private void CompareReports(List<Report> selectedReports)
        {
            panel1.Controls.Clear();
            graphs.CompareNumberOfRoads(selectedReports);
            graphs.CompareSimulationTimings(selectedReports);
            graphs.CompareCarsEnteredCarsLeft(selectedReports);
            graphs.HeatMapComparison(selectedReports);
            graphs.AccidentsComparison(selectedReports);
            graphs.ComparePedestrians(selectedReports);
            graphs.CompareAlgorithmsTiming(selectedReports);
            graphs.CompareTotalAccidents(selectedReports);
        }

        public void btnOverall_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pbLogo.Visible = false;
                panel1.Controls.Clear();
                graphs.SimulationsPerGrid();
                graphs.SimulationsPerMonth();
                graphs.DistributionOfCarsAndAccidentsByNumberOfRoads();
                graphs.SimulationsCountCarsLeft();
                graphs.SimulationsCountAccidents();
                graphs.HeatMapPerYearPerMonth("2020");
                graphs.TotalDrivingTimePerAlgorithm();
                graphs.PlanedTimeAndActualTime();
            }
            else
            {
                MessageBox.Show("Please load at least one report!");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save graph";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            var checkSaveDialog = saveFileDialog1.ShowDialog();
            if (checkSaveDialog == DialogResult.OK)
            {
                try
                {
                    this.Export(saveFileDialog1.FileName);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }
        public void Export(string filename) 
        {
            var fileInfo = new FileInfo(filename);
            var directory = fileInfo.Directory.FullName;
            var imageFileInfo = $"{directory}\\{fileInfo.Name}-image.jpg";
            Rectangle bounds = this.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save(imageFileInfo, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            iTextSharp.text.Rectangle pageSize = null;

            using (var srcImage = new Bitmap(imageFileInfo))
            {
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            }

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                var image = iTextSharp.text.Image.GetInstance(imageFileInfo);
                document.Add(image);
                document.Close();

                File.WriteAllBytes(filename, ms.ToArray());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.DarkSlateGray, ButtonBorderStyle.Solid);
        }

    }
}
