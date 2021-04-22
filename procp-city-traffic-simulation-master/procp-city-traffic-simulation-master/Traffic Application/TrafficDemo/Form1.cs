using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrafficDemo.Classes;
using System.Drawing.Drawing2D;
using MaterialSkin.Controls;
using MaterialSkin;
using MaterialSkin.Animations;

namespace TrafficDemo
{

    public partial class Form1 : MaterialForm
    {
        Simulation mySimulation;
        Mediator myMediator;
        Grid grid;
        Bitmap typeOfRoad;
        PictureBox roadToBeDeleted;
        int selectedCell = 0;
        List<PictureBox> pictureBoxes;
        PictureBox pb_b;
        private string savedFile = "";
        int NumberOfCars;
        int NumberOfPedestrians;
        bool IsNighMode = false;
        private int ticks = 0;

        // Shortest path algorithm 

        string algorithm = "Dijkstra"; // For now, change when adding A* 
        int emergencyVehiclesCount = 0;
        double emergencyVehicleWaitingTime = 0;  
        List<Node> shortestPathFromStartToCrash = new List<Node>();
        List<Node> shortestPathFromEndToCrash = new List<Node>();
        GPSTracker trackerStartToCrash;
        GPSTracker trackerEndToCrash;
        Dictionary<Road, List<Node>> startToCrashDictionary;
        Dictionary<Road, List<Node>> endToCrashDictionary;
        Point emergencyVehicleStartingPoint;
        Point emergencyVehicleEndPoint;
        //--------------


        private int maxflow = 0;
        double duration;
        int numberOfCarsEntered;
        double initialDuration;

        public Form1()
        {
            InitializeComponent();
            gridPanel.Location = new Point(249, 230);
            gridPanel.Visible = false;
            DoubleBuffered = true; // Removes Flickering
            mySimulation = new Simulation("Test simulation", 600);
            myMediator = new Mediator();
            pictureBoxes = new List<PictureBox>();
            NumberOfCars = 0;
            NumberOfPedestrians = 0;
            this.Subscribe();
            listbox1.DrawMode = DrawMode.OwnerDrawFixed;
            startToCrashDictionary = new Dictionary<Road, List<Node>>();
            endToCrashDictionary = new Dictionary<Road, List<Node>>();
            //simulation speed
            textBox1.Text = "1x";
            listbox1.Location = new Point(249, 670);
            initialDuration = -1;
            numberOfCarsEntered = 0;
            listbox1.Height = 200;
        }

        public void Subscribe()
        {
            myMediator.CarsCrashed += new Mediator.Notication(UpdateListBox);
            myMediator.CarReachedEnd += new Mediator.Notication(UpdateListBox);

        }
        public void InitializeGridTesting()
        {
            grid = new Grid(3, 4);
        }

        public List<PictureBox> ReturnPictureBoxes()
        {
            return pictureBoxes;
        }

        public Grid ReturnGrid()
        {
            return grid;
        }

        public void UpdateListBox(String X)
        {
            listbox1.SelectedIndex = listbox1.Items.Count - 1;
            listbox1.SelectedIndex = -1;
            ListBoxItem m;
            if (X.Equals("Cars have crashed into each other!"))
            {
                m = new ListBoxItem(Color.Red, "[" + DateTime.Now.ToLocalTime().ToString("h mm ss tt") + "]  " + X);
            }
            else
            {
                m = new ListBoxItem(IsNighMode ? Color.White : Color.Black, "[" + DateTime.Now.ToLocalTime().ToString("h mm ss tt") + "]  " + X);
            }
            listbox1.Items.Add(m);
        }

        private void TwoWay_MouseDown(object sender, MouseEventArgs e)
        {
            if (TwoWay.Tag.ToString() == "two-way-h" || TwoWay.Tag.ToString() == "two-way-h-Night")
            {
                typeOfRoad = IsNighMode ? Properties.Resources.twoWay_h_Night : Properties.Resources.twoWay_h;
                typeOfRoad.Tag = "two-way-h";
            }
            else
            {
                typeOfRoad = IsNighMode ? Properties.Resources.twoWay_v_Night : Properties.Resources.twoWay_v;
                typeOfRoad.Tag = "two-way-v";
            }
            TwoWay.DoDragDrop(typeOfRoad, DragDropEffects.Copy);
        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            if (TwoWay.Tag.ToString() == "two-way-h" || TwoWay.Tag.ToString() == "two-way-h-Night")
            {
                TwoWay.Image = IsNighMode ? Properties.Resources.twoWay_v_Night : Properties.Resources.twoWay_v;
                TwoWay.Tag = IsNighMode ? "two-way-v-Night" : "two-way-v";
            }
            else
            {
                TwoWay.Image = IsNighMode ? Properties.Resources.twoWay_h_Night : Properties.Resources.twoWay_h;
                TwoWay.Tag = IsNighMode ? "two-way-h-Night" : "two-way-h";
            }
        }
        private void roundabout_MouseDown(object sender, MouseEventArgs e)
        {
            typeOfRoad = IsNighMode ? Properties.Resources.NightModeRoundabout : Properties.Resources.roundabout;
            typeOfRoad.Tag = "round";
            roundabout.DoDragDrop(typeOfRoad, DragDropEffects.Copy);
        }

        private void roadType1_MouseDown(object sender, MouseEventArgs e)
        {
            typeOfRoad = IsNighMode ? Properties.Resources.NighModeRoadType1 : Properties.Resources.roadType1;
            typeOfRoad.Tag = "type1";
            roadType1.DoDragDrop(typeOfRoad, DragDropEffects.Copy);
        }

        private void roadType2_MouseDown(object sender, MouseEventArgs e)
        {
            typeOfRoad = IsNighMode ? Properties.Resources.NighModeRoadType2 : Properties.Resources.roadType2;
            typeOfRoad.Tag = "type2";
            roadType2.DoDragDrop(typeOfRoad, DragDropEffects.Copy);
        }
        private void determineCell(DragEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position);
            int column = ((cursor.X - gridPanel.Left) / 200) + 1;
            int row = (cursor.Y - gridPanel.Top) / 200;
            selectedCell = row * (gridPanel.Width / 200) + column;
        }
        private void gridPanel_DragDrop(object sender, DragEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position);
            Point drawPoint = new Point();
            determineCell(e);
            drawPoint = grid.Cells[selectedCell - 1].Location;
            if (grid.Cells[selectedCell - 1].Taken)
            {
                MessageBox.Show("This grid is already taken. Please delete it first if you want to replace it !");
            }
            else
            {
                grid.Cells[selectedCell - 1].Taken = true;
                //Create a a picture box to be placed in the cell
                pb_b = new PictureBox
                {
                    AutoScrollOffset = drawPoint,
                    Location = drawPoint,
                    Height = 200,
                    Width = 200,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                };

                //add mouse handler for showing the statistic for example or deleting the road 
                pb_b.MouseDown += new MouseEventHandler(Mouse_Down_Road_Menu);
                pb_b.Paint += new PaintEventHandler(road_Paint_event);
                pb_b.Image = typeOfRoad;
                //Which crossing will be dragged
                if (typeOfRoad.Tag.Equals("type1"))
                {
                    RoadType1 road = new RoadType1(drawPoint, pb_b);
                    pb_b.Tag = road;
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);
                    grid.AddRoad(road, selectedCell - 1);
                    maxflow += 56;
                    // We now add a lane to the road 
                    // First, we need to determine the points that the lane should have

                    AddMapEndpointsToLanes(road);

                }
                else if (typeOfRoad.Tag.Equals("type2"))
                {
                    RoadType2 road = new RoadType2(drawPoint, pb_b);
                    pb_b.Tag = road;
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);
                    grid.AddRoad(road, selectedCell - 1);
                    AddMapEndpointsToLanes(road);
                    maxflow += 56;
                }
                else if (typeOfRoad.Tag.Equals("round"))
                {
                    Roundabout road = new Roundabout(drawPoint, pb_b);
                    pb_b.Tag = road;
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);
                    grid.AddRoad(road, selectedCell - 1);
                    AddMapEndpointsToLanes(road);
                    maxflow += 70;
                }
                else if (typeOfRoad.Tag.Equals("two-way-h"))
                {
                    TwoWayHorizontal road = new TwoWayHorizontal(drawPoint, pb_b);
                    pb_b.Tag = road;
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);
                    grid.AddRoad(road, selectedCell - 1);
                    AddMapEndpointsToLanes(road);
                    maxflow += 28;
                }
                else
                {
                    TwoWayVertical road = new TwoWayVertical(drawPoint, pb_b);
                    pb_b.Tag = road;
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);
                    grid.AddRoad(road, selectedCell - 1);
                    AddMapEndpointsToLanes(road);
                    maxflow += 28;
                }


                this.Controls.Add(pb_b);
                pb_b.Parent = gridPanel;
                pb_b.MouseClick += new MouseEventHandler(road_MouseClick);
                //Which crossing will be dragged
            }


        }

        private void AddMapEndpointsToLanes(Road road)
        {
            foreach (string group in road.lanesGroups.Keys)
            {
                foreach (Lane L in road.lanesGroups[group])
                {
                    L.StartPointOnScreen = new Point(Convert.ToInt32(L.StartPoint.X) + pb_b.Location.X, Convert.ToInt32(L.StartPoint.Y) + pb_b.Location.Y);
                    L.EndPointsOnScreen.Add(1, new Point(Convert.ToInt32(L.EndPoint.X) + pb_b.Location.X, Convert.ToInt32(L.EndPoint.Y) + pb_b.Location.Y));
                    if (L.Edges != null)
                    {
                        foreach (PointF edge in L.Edges)
                        {
                            L.EdgesOnScreen.Add(new Point(Convert.ToInt32(edge.X) + pb_b.Location.X, Convert.ToInt32(edge.Y) + pb_b.Location.Y));
                        }
                    }
                }
            }
        }

        private void removeCrossingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this crossing?", "Remove Crossing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                roadToBeDeleted.Dispose();
                pictureBoxes.Remove(roadToBeDeleted);
                grid.RemoveCrossing(selectedCell);
                DrawGrid();
            }
        }
        private void Mouse_Down_Road_Menu(object sender, MouseEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position);
            int column = ((cursor.X - gridPanel.Left) / 200) + 1;
            int row = (cursor.Y - gridPanel.Top) / 200;
            selectedCell = row * (gridPanel.Width / 200) + column;

            roadToBeDeleted = (PictureBox)sender;
            if (e.Button == MouseButtons.Left)
            {
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, cursor.X, cursor.Y);
            }
        }
        private void gridPanel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        //hardcoded values 
        private void btnSmallGridSelected_Click(object sender, EventArgs e)
        {
            stretchDesignOnGridChange(1180, 880, 250, 850, 650, 600);
            GenerateGrid(600, 400);

        }
        //hardcoded values 
        private void btnMiddleGridSelected_Click(object sender, EventArgs e)
        {
            stretchDesignOnGridChange(1180, 1050, 250, 850, 850, 600);
            GenerateGrid(600, 600);

        }
        //hardcoded values 
        private void btnLargeGridSelected_Click(object sender, EventArgs e)
        {
            stretchDesignOnGridChange(1380, 1050, 330, 1050, 850, 800);
            GenerateGrid(800, 600);

        }
        private void stretchDesignOnGridChange(int formWidth, int formHeight, int dragAndDropPanelX, int settingsPanelX, int listBox1Y, int listBox1Width)
        {
            this.Width = formWidth;
            this.Height = formHeight;
            materialTabSelector1.Height = formHeight;
            materialTabSelector2.Height = formHeight;
            settingsPanel.Height = formHeight;
            materialTabSelector4.Height = formHeight;
            materialTabSelector4.Width = listBox1Width + 50;
            dragAndDropPanel.Location = new Point(dragAndDropPanelX, dragAndDropPanel.Location.Y);
            settingsPanel.Location = new Point(settingsPanelX, settingsPanel.Location.Y);
            listbox1.Location = new Point(listbox1.Location.X, listBox1Y);
            listbox1.Width = listBox1Width;
        }

        private void GenerateGrid(int weidth, int height)
        {
            if (gridPanel.Visible == false) gridPanel.Visible = true;

            gridPanel.Show();
            gridPanel.Controls.Clear();
            gridPanel.Height = height;
            gridPanel.Width = weidth;
            grid = new Grid(height / 200, weidth / 200);
            mySimulation.Grid = grid;
            DrawGrid();
        }

        public void DrawGrid()
        {
            Pen myPen;
            myPen = new Pen(!IsNighMode ? Color.Black : Color.White);
            Graphics formGraphics = this.gridPanel.CreateGraphics();


            //drawing cells rows
            foreach (var item in grid.Cells)
            {
                formGraphics.DrawLine(myPen, item.Location.X, item.Location.Y, (item.Location.X + 200), item.Location.Y);//top line from the left to right
                formGraphics.DrawLine(myPen, item.Location.X, item.Location.Y, item.Location.X, (item.Location.Y + 200));//left line from top to bottom
                formGraphics.DrawLine(myPen, item.Location.X, (item.Location.Y + 199), (item.Location.X + 200), item.Location.Y + 199);//bottom line from the left to right
                formGraphics.DrawLine(myPen, (item.Location.X + 199), item.Location.Y, (item.Location.X + 199), (item.Location.Y + 200));//right line from top to bottom
            }
            myPen.Dispose();
            formGraphics.Dispose();
        }



        /// <summary>
        /// Serializes an object to be saved
        /// </summary>
        /// <param name="filename">The name of the file to be saved</param>
        public bool Save(string filename)
        {
            Serialize serialize = new Serialize();
            serialize.SerializeObject(filename, grid);
            return true;
        }

        public void SaveAs()
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.FileName = "My simulation";
            saveas.Filter = "Simulation file|*.sim";

            if (saveas.ShowDialog() == DialogResult.OK)
            {
                Save(saveas.FileName);
                MessageBox.Show("Saved");
                this.savedFile = saveas.FileName;
            }
        }

        /// <summary>
        /// Loads a saved file from a chosen directory in a .sim format
        /// </summary>
        public void loadfile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "My simulaiton";
            openFile.Filter = "Simulation file|*.sim";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("open");
                gridPanel.Controls.Clear();
                loadFile(openFile.FileName);
            }
        }

        /// <summary>
        /// Deserializes an object to be loaded
        /// </summary>
        /// <param name="filename">The name of the file to be loaded</param>

        public void loadFile(string filename)
        {
            ClearGrid();
            Serialize serialize = new Serialize();
            this.grid = (Grid)serialize.DeSerializeObject(filename);
            mySimulation.Grid = grid;
            if (grid.Cells.Count == 6)
            {
                stretchDesignOnGridChange(1250, 800, 260, 850, 580, 600);
                if (gridPanel.Visible == false) gridPanel.Visible = true;
                gridPanel.Height = 400;
                gridPanel.Width = 600;
                gridPanel.Show();
                gridPanel.Controls.Clear();
            }
            else if (grid.Cells.Count == 9)
            {
                stretchDesignOnGridChange(1250, 1000, 260, 850, 770, 600);
                if (gridPanel.Visible == false) gridPanel.Visible = true;
                gridPanel.Height = 600;
                gridPanel.Width = 600;
                gridPanel.Show();
                gridPanel.Controls.Clear();
            }
            else if (grid.Cells.Count == 12)
            {
                stretchDesignOnGridChange(1420, 1000, 350, 1050, 770, 800);
                gridPanel.Height = 600;
                gridPanel.Width = 800;
                gridPanel.Show();
                gridPanel.Controls.Clear();
            }

            foreach (Road cr in grid.Roads)
            {
                if (cr != null)
                {
                    PictureBox pb_b = new PictureBox();
                    if (grid.Cells.Count == 6)
                    {
                        pb_b.Height = gridPanel.Height / 2;
                        pb_b.Width = gridPanel.Width / 3;
                    }
                    else if (grid.Cells.Count == 9)
                    {
                        pb_b.Height = gridPanel.Height / 3;
                        pb_b.Width = gridPanel.Width / 3;
                    }
                    else if (grid.Cells.Count == 12)
                    {
                        pb_b.Height = gridPanel.Height / 3;
                        pb_b.Width = gridPanel.Width / 4;
                    }

                    pb_b.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb_b.BackColor = Color.Transparent;
                    pb_b.MouseDown += new MouseEventHandler(Mouse_Down_Road_Menu);

                    if (cr is RoadType1)
                    {
                        pb_b.Tag = cr;
                        pb_b.Image = Properties.Resources.roadType1;

                    }
                    else if (cr is RoadType2)
                    {
                        pb_b.Tag = cr;
                        pb_b.Image = Properties.Resources.roadType2;
                    }
                    else if (cr is TwoWayHorizontal)
                    {
                        pb_b.Tag = cr;
                        pb_b.Image = Properties.Resources.twoWay_h;
                    }
                    else if (cr is TwoWayVertical)
                    {
                        pb_b.Tag = cr;
                        pb_b.Image = Properties.Resources.twoWay_v;
                    }
                    else
                    {
                        pb_b.Tag = cr;
                        pb_b.Image = Properties.Resources.roundabout;
                    }
                    this.Controls.Add(pb_b);
                    pb_b.SendToBack();
                    pictureBoxes.Add(pb_b);

                    pb_b.Location = cr.Location;

                    pb_b.Parent = gridPanel;

                    cr.Pb_Background = pb_b;

                }
            }
            grid.InitializeGrid();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbVehicelsAmount.Text, out int result))
            {
                MessageBox.Show("Please enter a valid amount of cars first!");
                return;
            }
            if (grid == null)
            {
                MessageBox.Show("Please generate a map layout first!");
                return;
            }
            if (grid.Roads.Count <= 0)
            {
                MessageBox.Show("Please add at least one type of road!");
                return;
            }
            if (tbSimulationTime.Text == "")
            {
                MessageBox.Show("Please add Simulation time!");
                return;
            }
            initialDuration = Convert.ToDouble(tbSimulationTime.Text);
            openbtn.Enabled = false;
            savebtn.Enabled = false;
            saveasbtn.Enabled = false;
            btnSmallGridSelected.Enabled = false;
            btnMiddleGridSelected.Enabled = false;
            btnLargeGridSelected.Enabled = false;
            materialRaisedButton2.Enabled = false;
            materialRaisedButton3.Enabled = false;
            envonebtn.Enabled = false;
            envtwobtn.Enabled = false;
            rbAStar.Enabled = false;
            rbDijkstra.Enabled = false;
            //  envthreebtn.Enabled = false;
            tbVehicelsAmount.Enabled = false;
            pdstrlower.Enabled = false;
            pdstrupper.Enabled = false;
            sprselcheckbox.Enabled = false;
            materialRaisedButton4.Enabled = false;
            roadType1.Enabled = false;
            roadType2.Enabled = false;
            NumberOfCars = Convert.ToInt32(tbVehicelsAmount.Text);
            listbox1.Items.Add(new ListBoxItem(IsNighMode ? Color.White : Color.Black,
                "[" + DateTime.Now.ToLocalTime().ToString("h mm ss tt") + "]  " + "Simulation Has started!"));
            listbox1.Items.Add(new ListBoxItem(IsNighMode ? Color.White : Color.Black,
                "[" + DateTime.Now.ToLocalTime().ToString("h mm ss tt") + "]  " + "Maximum Flow of cars: " + maxflow));
            timerActualTime.Start();
            foreach (Road R in grid.Roads)
            {
                myMediator.AddRoad(R);
                foreach (string group in R.pedestriansLanesGroups.Keys)
                {
                    foreach (PedestrianLane pl in R.pedestriansLanesGroups[group])
                    {
                        R.AddPedestrians(pl, NumberOfPedestrians);
                    }
                }
                foreach (string group in R.lanesGroups.Keys)
                    {
                        bool isAddedCar = false;
                        foreach (Lane L in R.lanesGroups[group])
                        {
                            bool checkSpawingnLanes = mySimulation.Grid.CheckSpawningLanes(L);
                            if (checkSpawingnLanes && !isAddedCar)
                            {
                                R.AddCars(L, NumberOfCars);
                                numberOfCarsEntered += NumberOfCars;
                                
                                isAddedCar = true;
                            }
/*
                            if (!checkSpawingnLanes)
                            {
                                L.RemoveAllCars();
                            }
                            */
                        }
                    }
            }

            grid.StartTimers();
            timerActualTime.Start();
            timer2.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timerActualTime.Stop();
            timer2.Stop();
            grid.PauseTimers();
            openbtn.Enabled = true;
            savebtn.Enabled = true;
            saveasbtn.Enabled = true;
            materialRaisedButton2.Enabled = true;
            materialRaisedButton3.Enabled = true;
            tbVehicelsAmount.Enabled = true;
            pdstrlower.Enabled = true;
            pdstrupper.Enabled = true;
            sprselcheckbox.Enabled = true;
            materialRaisedButton4.Enabled = true;
            roadType1.Enabled = true;
            roadType2.Enabled = true;


        }
        public void ClearGrid()
        {
            if (grid != null)
            {
                for (int i = 1; i <= grid.Cells.ToArray().Length; i++)
                {
                    if (grid.Cells[i - 1].Road != null)
                    {
                        pictureBoxes.Clear();
                        grid.RemoveCrossing(i);
                    }
                }
                DrawGrid();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnVehicleStart.Visible = false;
            GBalgorithm.Visible = false;
            envthreebtn.Visible = false;
            btnChooseDestination.Visible = false;
        }

        private void road_Paint_event(object sender, PaintEventArgs e)
        {
            /* PictureBox pb = (PictureBox)sender;

             Cell cell = grid.Cells.Find(c => c.Taken && c.Location == pb.Location);
             if (pb != null && !pb.IsDisposed && cell.Road != null)
             {
                 Graphics g = e.Graphics;
                 cell.Road.lanesGroups.Values.ToList().ForEach(list => list.ForEach(l => {
                     g.DrawPath((Pen)Pens.Aqua, l.MyPath);
                 }));
             }*/
        }

        private void tbVehicelsAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerActualTime.Stop();
            timer2.Stop();
            duration = 0;
            ticks = 0;
            NumberOfCars = 0;
            numberOfCarsEntered = 0;
            openbtn.Enabled = true;
            savebtn.Enabled = true;
            saveasbtn.Enabled = true;
            btnSmallGridSelected.Enabled = true;
            btnMiddleGridSelected.Enabled = true;
            btnLargeGridSelected.Enabled = true;
            materialRaisedButton2.Enabled = true;
            materialRaisedButton3.Enabled = true;
            envonebtn.Enabled = true;
            envtwobtn.Enabled = true;
            envthreebtn.Enabled = true;
            tbVehicelsAmount.Enabled = true;
            pdstrlower.Enabled = true;
            pdstrupper.Enabled = true;
            sprselcheckbox.Enabled = true;
            materialRaisedButton4.Enabled = true;
            roadType1.Enabled = true;
            roadType2.Enabled = true;
            rbAStar.Enabled = true;
            rbDijkstra.Enabled = true;
            maxflow = 0;
            listbox1.Items.Clear();
            grid.StopSimulation();

            myMediator.Clients = new List<Car>();
            pictureBoxes.ForEach(p => p.Refresh());
            //   ClearGrid();
        }

        private void resetvaluesbtn_Click(object sender, EventArgs e)
        {

           


        }
        public void ResetValues()
        {
            tbVehicelsAmount.Text = "";
            pdstrlower.Text = "";
            pdstrupper.Text = "";
        }

        private void saveasbtn_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (savedFile == "")
            {
                SaveAs();
            }
            else
            { Save(savedFile);
                MessageBox.Show("saved");
            }
        }



        private void openbtn_Click(object sender, EventArgs e)
        {
            loadfile();
        }

        private void reportbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(mySimulation, roadToBeDeleted, typeOfRoad, selectedCell);
            form3.ShowDialog();
        }

        private void timerActualTime_Tick(object sender, EventArgs e)
        {
            duration++;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            emergencyVehicleWaitingTime += 1;
        

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ticks++;
            if (ticks == Convert.ToInt32(tbSimulationTime.Text) || (grid.GetCarFlow() <= 0 && myMediator.EV == null)) 
            {
                timerActualTime.Stop();
                openbtn.Enabled = true;
                savebtn.Enabled = true;
                saveasbtn.Enabled = true;
                btnSmallGridSelected.Enabled = true;
                btnMiddleGridSelected.Enabled = true;
                btnLargeGridSelected.Enabled = true;
                materialRaisedButton2.Enabled = true;
                materialRaisedButton3.Enabled = true;
                envonebtn.Enabled = true;
                envtwobtn.Enabled = true;
                envthreebtn.Enabled = true;
                tbVehicelsAmount.Enabled = true;
                pdstrlower.Enabled = true;
                pdstrupper.Enabled = true;
                sprselcheckbox.Enabled = true;
                materialRaisedButton4.Enabled = true;
                roadType1.Enabled = true;
                roadType2.Enabled = true;
                listbox1.Items.Clear();

                if (emergencyVehiclesCount == 0)
                {
                    emergencyVehiclesCount = 1;
                }
                Form2 form2 = new Form2(duration, initialDuration, numberOfCarsEntered, grid, myMediator, emergencyVehicleWaitingTime/emergencyVehiclesCount, myMediator.CrashesCount, algorithm);
                form2.Show();
                grid.StopSimulation();

                duration = 0;
                ticks = 0;
                NumberOfCars = 0;
                maxflow = 0;
                numberOfCarsEntered = 0;

                myMediator.Clients = new List<Car>();
                myMediator.EV = null;

                pictureBoxes.ForEach(p => p.Refresh());
                shortestPathCounter.Stop();
                //   ClearGrid();
                timer2.Stop();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void sprselcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sprselcheckbox.Checked == true)
            {
                this.myMediator.SurpriseElements = true;
            }
            else
            {
                this.myMediator.SurpriseElements = false;
            }
        }

        public void envtwobtn_Click(object sender, EventArgs e)
        {
            IsNighMode = false;
            ClearGrid();
            Header.ForeColor = Color.Black;
            gridsizelabel.ForeColor = Color.Black;
            environmentslabel.ForeColor = Color.Black;
            pedestrianslabel.ForeColor = Color.Black;
            surpriseelementslabel.ForeColor = Color.Black;
            vehicleslabel.ForeColor = Color.Black;
            materialRaisedButton1.ForeColor = Color.Black;
            materialRaisedButton2.ForeColor = Color.Black;
            materialRaisedButton3.ForeColor = Color.Black;
            materialRaisedButton4.ForeColor = Color.Black;
            listbox1.ForeColor = Color.Gray;
            listbox1.BackColor = Color.White;
            roadType1.Image = Properties.Resources.roadType1;
            roadType2.Image = Properties.Resources.roadType2;
            roundabout.Image = Properties.Resources.roundabout;
            btnPause.BackgroundImage = Properties.Resources.pause;
            btnStart.BackgroundImage = Properties.Resources.play;
            btnStop.BackgroundImage = Properties.Resources.stop;
            btnSmallGridSelected.BackgroundImage = Properties.Resources.stop;
            btnMiddleGridSelected.BackgroundImage = Properties.Resources.four;
            btnLargeGridSelected.BackgroundImage = Properties.Resources.nine;
            envonebtn.BackgroundImage = Properties.Resources.sun;
            envtwobtn.BackgroundImage = Properties.Resources.moon;
            envthreebtn.BackgroundImage = Properties.Resources.traffic;
            openbtn.BackgroundImage = Properties.Resources.open;
            saveasbtn.BackgroundImage = Properties.Resources.saveas;
            savebtn.BackgroundImage = Properties.Resources.save;
            btnFlip.BackgroundImage = Properties.Resources.flip;
            if (TwoWay.Tag.ToString() == "two-way-h" || TwoWay.Tag.ToString() == "two-way-h-Night")
            {
                TwoWay.Image = Properties.Resources.twoWay_h;
                TwoWay.Tag = "two-way-h";
            }
            else
            {
                TwoWay.Image = Properties.Resources.twoWay_v;
                TwoWay.Tag = "two-way-v";
            }
            this.BackColor = Color.White;


        }

        public void envonebtn_Click(object sender, EventArgs e)
        {



            IsNighMode = true;
            tbVehicelsAmount.Text = "1";
            ClearGrid();
            Header.ForeColor = Color.White;
            gridsizelabel.ForeColor = Color.White;
            environmentslabel.ForeColor = Color.White;
            pedestrianslabel.ForeColor = Color.White;
            surpriseelementslabel.ForeColor = Color.White;
            vehicleslabel.ForeColor = Color.White;
            materialRaisedButton1.ForeColor = Color.White;
            materialRaisedButton2.ForeColor = Color.White;
            materialRaisedButton3.ForeColor = Color.White;
            materialRaisedButton4.ForeColor = Color.White;
            listbox1.ForeColor = Color.White;
            listbox1.BackColor = Color.DarkSlateBlue;
            roadType1.Image = Properties.Resources.NighModeRoadType1;
            roadType2.Image = Properties.Resources.NighModeRoadType2;
            roundabout.Image = Properties.Resources.NightModeRoundabout;
            btnPause.BackgroundImage = Properties.Resources.pauseWhite;
            btnStart.BackgroundImage = Properties.Resources.playWhite;
            btnStop.BackgroundImage = Properties.Resources.stopWhite;
            btnSmallGridSelected.BackgroundImage = Properties.Resources.stopWhite;
            btnMiddleGridSelected.BackgroundImage = Properties.Resources.fourWhite;
            btnLargeGridSelected.BackgroundImage = Properties.Resources.nineWhite;
            envonebtn.BackgroundImage = Properties.Resources.sunWhite;
            envtwobtn.BackgroundImage = Properties.Resources.moonWhite;
            envthreebtn.BackgroundImage = Properties.Resources.trafficWhite;
            openbtn.BackgroundImage = Properties.Resources.openWhite;
            saveasbtn.BackgroundImage = Properties.Resources.saveasWhite;
            savebtn.BackgroundImage = Properties.Resources.saveWhite;
            btnFlip.BackgroundImage = Properties.Resources.flipWhite;
            if (TwoWay.Tag.ToString() == "two-way-h" || TwoWay.Tag.ToString() == "two-way-h-Night")
            {
                TwoWay.Image = Properties.Resources.twoWay_h_Night;
                TwoWay.Tag = "two-way-h-Night";
            }
            else
            {
                TwoWay.Image = Properties.Resources.twoWay_v_Night;
                TwoWay.Tag = "two-way-v-Night";
            }
            this.BackColor = Color.DarkSlateBlue;
        }

        private void listbox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ListBoxItem item = listbox1.Items[e.Index] as ListBoxItem; // Get the current item and cast it to MyListBoxItem
                if (item != null)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    e.Graphics.DrawString(item.Message, listbox1.Font, new SolidBrush(item.ItemColor), e.Bounds);
                }
            }
        }

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void envthreebtn_Click(object sender, EventArgs e)
        {
            if (cbEmergency.Checked == true)
            {
                if (myMediator.EV == null)
                {

                    // Reset the values in case we have an older crash 
                    trackerEndToCrash = null;
                    trackerStartToCrash = null;
                    shortestPathFromStartToCrash = new List<Node>(); ;
                    shortestPathFromEndToCrash = new List<Node>() ;
                    startToCrashDictionary = new Dictionary<Road, List<Node>>();
                    endToCrashDictionary = new Dictionary<Road, List<Node>>();


                    if (this.myMediator.CrashLocations.Count > 0)
                    {


                        trackerStartToCrash = new GPSTracker(this.grid, this.emergencyVehicleStartingPoint, this.myMediator.CrashLocations.Values.ElementAt(0), this.myMediator.CrashLocations.Keys.ElementAt(0), algorithm);
                        trackerEndToCrash = new GPSTracker(this.grid, this.emergencyVehicleEndPoint, this.myMediator.CrashLocations.Values.ElementAt(0), this.myMediator.CrashLocations.Keys.ElementAt(0), algorithm);
                        if (algorithm == "A*")
                        {
                            shortestPathFromStartToCrash = trackerStartToCrash.GetShortestPathAstar();
                            shortestPathFromEndToCrash = trackerEndToCrash.GetShortestPathAstar();



                        }

                        else
                        {
                            shortestPathFromStartToCrash = trackerStartToCrash.GetShortesPathDijkstra();
                            shortestPathFromEndToCrash = trackerEndToCrash.GetShortesPathDijkstra();
                        }

                        // Getting the Road - NodeList dictionary for the start to crash
                        List<Node> holder1 = new List<Node>();
                        foreach (Cell C in this.grid.Cells)
                        {
                            if (C.Road != null)
                            {
                                foreach (Node N in this.shortestPathFromStartToCrash)
                                {
                                    if (N.parentLane.ParentRoad == C.Road)
                                    {
                                        holder1.Add(N);
                                    }
                                }
                                startToCrashDictionary.Add(C.Road, holder1);
                                holder1 = new List<Node>();
                            }
                        }

                        // Getting the Road - NodeList dictionary for the end to crash
                        List<Node> holder2 = new List<Node>();
                        foreach (Cell C in this.grid.Cells)
                        {
                            if (C.Road != null)
                            {
                                foreach (Node N in this.shortestPathFromEndToCrash)
                                {
                                    if (N.parentLane.ParentRoad == C.Road)
                                    {
                                        holder2.Add(N);
                                    }
                                }
                                endToCrashDictionary.Add(C.Road, holder2);
                                holder2 = new List<Node>();
                            }
                        }


                        this.myMediator.SpawnEmergencyVehicle(this.emergencyVehicleStartingPoint, this.myMediator.CrashLocations.Values.ElementAt(0), shortestPathFromStartToCrash, shortestPathFromEndToCrash, this.emergencyVehicleEndPoint);
                        emergencyVehiclesCount += 1;
                    }
                }
                shortestPathCounter.Start();

            }
            else
            {
                MessageBox.Show("Emergency vehicles aren't enabled");
            }



        }




        private void gridPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {
            
        }

        private void openbtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Open simulation", openbtn);
        }

        private void savebtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Save simulation", savebtn);
        }

        private void saveasbtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Save simulation as ...", saveasbtn);
        }

        private void btnSmallGridSelected_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Small grid 3x2", btnSmallGridSelected);
        }

        private void btnMiddleGridSelected_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Medium grid 3x3", btnMiddleGridSelected);
        }

        private void btnLargeGridSelected_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Large grid 4x3", btnLargeGridSelected);
        }

        private void btnStart_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Start simulation", btnStart);
        }

        private void btnPause_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Pause simulation", btnPause);
        }

        private void btnStop_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Stop simulation", btnStop);
        }

        private void envonebtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Day mode", envonebtn);
        }

        private void envtwobtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Night mode", envtwobtn);
        }

        private void envthreebtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Spawn an emergency vehicle", envthreebtn);
        }

        private void materialRaisedButton1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("The number of cars currently on the road network displayed on the listbox", materialRaisedButton1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void graphicalOverlay1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBarPedestrians_Scroll(object sender, EventArgs e)
        {
            NumberOfPedestrians = trackBarPedestrians.Value;
            pdstrlower.Text = trackBarPedestrians.Minimum.ToString();
            pdstrupper.Text = trackBarPedestrians.Maximum.ToString();


        }

        private void roadType1_Click(object sender, EventArgs e)
        {

        }

        private void TrackBarSpeed_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = "x" + TrackBarSpeed.Value.ToString();
            if (grid != null)
            {
                grid.SimSpeed = TrackBarSpeed.Value;
                foreach (Road r in grid.Roads)
                {
                    r.TrSpeed(TrackBarSpeed.Value);
                }
            }
        }

        private void gridPanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void road_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (sender as PictureBox);
            Road road = (pb.Tag as Road);
            int X = e.Location.X + road.Location.X;
            int Y = e.Location.Y + road.Location.Y;
            if (btnVehicleStart.Enabled == false)
            {
                btnVehicleStart.Text = "(" +  X + "," + Y + ")";
                btnVehicleStart.Enabled = true;
                this.emergencyVehicleStartingPoint = new Point(X, Y);
                lblEVStatus.Text = "";

            }
            if (btnChooseDestination.Enabled == false)
            {
                btnChooseDestination.Text = "(" + X + "," + Y + ")";
                btnChooseDestination.Enabled = true;
                this.emergencyVehicleEndPoint = new Point(X, Y);
                lblEVStatus.Text = "";
            }


        }

        private void btnVehicleStart_Click(object sender, EventArgs e)
        {
            if (btnVehicleStart.Enabled == true && btnChooseDestination.Enabled == false)
            {
                lblEVStatus.Text = "Please select a destination first";
                return;
            }
           
            btnVehicleStart.Enabled = false;
            lblEVStatus.Text = "Choose a starting point on the grid";
        }

        private void btnChooseDestination_Click(object sender, EventArgs e)
        {
            if (btnChooseDestination.Enabled == true && btnVehicleStart.Enabled == false)
            {
                lblEVStatus.Text = "Please select a start first";
                return;
            }

            btnChooseDestination.Enabled = false;
            lblEVStatus.Text = "Choose a destination point on the grid";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEmergency.Checked == true)
            {
                GBalgorithm.Visible = true;
                envthreebtn.Visible = true;
                btnChooseDestination.Visible = true;
                btnVehicleStart.Visible = true;
                btnVehicleStart.Text = "Start";
                btnChooseDestination.Text = "Destination";
            }
            else
            {
                GBalgorithm.Visible = false;
                envthreebtn.Visible = false;
                btnChooseDestination.Visible = false;
                btnVehicleStart.Visible = false;
                this.emergencyVehicleStartingPoint = new Point();
                this.emergencyVehicleEndPoint = new Point();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lblEmergency_Click(object sender, EventArgs e)
        {

        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void rbAStar_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm = "A*";
        }

        private void rbDijkstra_CheckedChanged(object sender, EventArgs e)
        {
            this.algorithm = "Dijkstra";

        }

        private void btnShowStatistics_Click(object sender, EventArgs e)
        {
            StatisticsView statsView = new StatisticsView();
            statsView.Show();
        }

        private void materialTabSelector2_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            int result = grid.GetCarFlow();

            listbox1.Items.Add(new ListBoxItem(IsNighMode ? Color.White : Color.Black,
                "[" + DateTime.Now.ToLocalTime().ToString("h mm ss tt") + "]  " + "Current Flow of cars: " + result));
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save before you clear?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                maxflow = 0;
                if (savedFile == "")
                {
                    SaveAs();
                    ClearGrid();
                    listbox1.Items.Clear();
                }
                else
                {
                    Save(savedFile);
                    MessageBox.Show("saved");
                    ClearGrid();
                    listbox1.Items.Clear();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                ClearGrid();
                listbox1.Items.Clear();
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            if (emergencyVehiclesCount == 0)
            {
                emergencyVehiclesCount = 1;
            }

            Form2 form2 = new Form2(duration, initialDuration, numberOfCarsEntered, grid, myMediator, emergencyVehicleWaitingTime / emergencyVehiclesCount, myMediator.CrashesCount, algorithm);
            form2.Show();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton4_Click_1(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void materialRaisedButton1_MouseHover_1(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            StatisticsView statsView = new StatisticsView();
            statsView.Show();

        }
        public void TestStatisticsFormLoading(ref StatisticsView stView)
        {
            stView.Show();
        }
    } 
}

