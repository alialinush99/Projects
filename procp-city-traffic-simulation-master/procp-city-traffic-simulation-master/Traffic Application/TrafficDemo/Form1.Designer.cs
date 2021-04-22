namespace TrafficDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gridsizelabel = new System.Windows.Forms.Label();
            this.tbVehicelsAmount = new System.Windows.Forms.TextBox();
            this.environmentslabel = new System.Windows.Forms.Label();
            this.vehicleslabel = new System.Windows.Forms.Label();
            this.pedestrianslabel = new System.Windows.Forms.Label();
            this.pdstrupper = new System.Windows.Forms.TextBox();
            this.pdstrlower = new System.Windows.Forms.TextBox();
            this.trackBarPedestrians = new System.Windows.Forms.TrackBar();
            this.surpriseelementslabel = new System.Windows.Forms.Label();
            this.sprselcheckbox = new System.Windows.Forms.CheckBox();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.GBalgorithm = new System.Windows.Forms.GroupBox();
            this.rbAStar = new System.Windows.Forms.RadioButton();
            this.rbDijkstra = new System.Windows.Forms.RadioButton();
            this.envthreebtn = new System.Windows.Forms.Button();
            this.btnChooseDestination = new System.Windows.Forms.Button();
            this.btnVehicleStart = new System.Windows.Forms.Button();
            this.materialRaisedButton4 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblEVStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSimulationTime = new System.Windows.Forms.TextBox();
            this.TrackBarSpeed = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.envonebtn = new System.Windows.Forms.Button();
            this.envtwobtn = new System.Windows.Forms.Button();
            this.cbEmergency = new System.Windows.Forms.CheckBox();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.materialTabSelector2 = new MaterialSkin.Controls.MaterialTabSelector();
            this.dragAndDropPanel = new System.Windows.Forms.Panel();
            this.btnFlip = new System.Windows.Forms.Button();
            this.TwoWay = new System.Windows.Forms.PictureBox();
            this.roundabout = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.roadType1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.roadType2 = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeCrossingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.listbox1 = new System.Windows.Forms.ListBox();
            this.timerActualTime = new System.Windows.Forms.Timer(this.components);
            this.shortestPathCounter = new System.Windows.Forms.Timer(this.components);
            this.btnSmallGridSelected = new System.Windows.Forms.Button();
            this.btnMiddleGridSelected = new System.Windows.Forms.Button();
            this.btnLargeGridSelected = new System.Windows.Forms.Button();
            this.saveasbtn = new System.Windows.Forms.Button();
            this.savebtn = new System.Windows.Forms.Button();
            this.openbtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Header = new System.Windows.Forms.PictureBox();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.materialRaisedButton2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton3 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector3 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialRaisedButton5 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector4 = new MaterialSkin.Controls.MaterialTabSelector();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPedestrians)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.GBalgorithm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSpeed)).BeginInit();
            this.dragAndDropPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TwoWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundabout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadType2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Header)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 75;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gridsizelabel
            // 
            this.gridsizelabel.AutoSize = true;
            this.gridsizelabel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.gridsizelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridsizelabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridsizelabel.Location = new System.Drawing.Point(68, 328);
            this.gridsizelabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gridsizelabel.Name = "gridsizelabel";
            this.gridsizelabel.Size = new System.Drawing.Size(95, 24);
            this.gridsizelabel.TabIndex = 4;
            this.gridsizelabel.Text = "Grid Size";
            // 
            // tbVehicelsAmount
            // 
            this.tbVehicelsAmount.Location = new System.Drawing.Point(155, 171);
            this.tbVehicelsAmount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbVehicelsAmount.Name = "tbVehicelsAmount";
            this.tbVehicelsAmount.Size = new System.Drawing.Size(34, 20);
            this.tbVehicelsAmount.TabIndex = 18;
            this.tbVehicelsAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbVehicelsAmount.TextChanged += new System.EventHandler(this.tbVehicelsAmount_TextChanged);
            // 
            // environmentslabel
            // 
            this.environmentslabel.AutoSize = true;
            this.environmentslabel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.environmentslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.environmentslabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.environmentslabel.Location = new System.Drawing.Point(110, 19);
            this.environmentslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.environmentslabel.Name = "environmentslabel";
            this.environmentslabel.Size = new System.Drawing.Size(138, 24);
            this.environmentslabel.TabIndex = 22;
            this.environmentslabel.Text = "Environments";
            // 
            // vehicleslabel
            // 
            this.vehicleslabel.AutoSize = true;
            this.vehicleslabel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.vehicleslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vehicleslabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.vehicleslabel.Location = new System.Drawing.Point(92, 128);
            this.vehicleslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.vehicleslabel.Name = "vehicleslabel";
            this.vehicleslabel.Size = new System.Drawing.Size(174, 24);
            this.vehicleslabel.TabIndex = 24;
            this.vehicleslabel.Text = "Vehicles per lane";
            // 
            // pedestrianslabel
            // 
            this.pedestrianslabel.AutoSize = true;
            this.pedestrianslabel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pedestrianslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pedestrianslabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pedestrianslabel.Location = new System.Drawing.Point(118, 284);
            this.pedestrianslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pedestrianslabel.Name = "pedestrianslabel";
            this.pedestrianslabel.Size = new System.Drawing.Size(119, 24);
            this.pedestrianslabel.TabIndex = 29;
            this.pedestrianslabel.Text = "Pedestrians";
            // 
            // pdstrupper
            // 
            this.pdstrupper.Location = new System.Drawing.Point(253, 319);
            this.pdstrupper.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pdstrupper.Name = "pdstrupper";
            this.pdstrupper.Size = new System.Drawing.Size(34, 20);
            this.pdstrupper.TabIndex = 27;
            // 
            // pdstrlower
            // 
            this.pdstrlower.Location = new System.Drawing.Point(79, 319);
            this.pdstrlower.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pdstrlower.Name = "pdstrlower";
            this.pdstrlower.Size = new System.Drawing.Size(34, 20);
            this.pdstrlower.TabIndex = 26;
            // 
            // trackBarPedestrians
            // 
            this.trackBarPedestrians.BackColor = System.Drawing.Color.DarkSlateGray;
            this.trackBarPedestrians.Location = new System.Drawing.Point(116, 319);
            this.trackBarPedestrians.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trackBarPedestrians.Maximum = 5;
            this.trackBarPedestrians.Name = "trackBarPedestrians";
            this.trackBarPedestrians.Size = new System.Drawing.Size(132, 45);
            this.trackBarPedestrians.TabIndex = 25;
            this.trackBarPedestrians.Scroll += new System.EventHandler(this.trackBarPedestrians_Scroll);
            // 
            // surpriseelementslabel
            // 
            this.surpriseelementslabel.AutoSize = true;
            this.surpriseelementslabel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.surpriseelementslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.surpriseelementslabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.surpriseelementslabel.Location = new System.Drawing.Point(62, 625);
            this.surpriseelementslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.surpriseelementslabel.Name = "surpriseelementslabel";
            this.surpriseelementslabel.Size = new System.Drawing.Size(181, 24);
            this.surpriseelementslabel.TabIndex = 30;
            this.surpriseelementslabel.Text = "Surprise Elements";
            // 
            // sprselcheckbox
            // 
            this.sprselcheckbox.AutoSize = true;
            this.sprselcheckbox.Location = new System.Drawing.Point(275, 634);
            this.sprselcheckbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sprselcheckbox.Name = "sprselcheckbox";
            this.sprselcheckbox.Size = new System.Drawing.Size(15, 14);
            this.sprselcheckbox.TabIndex = 32;
            this.sprselcheckbox.UseVisualStyleBackColor = true;
            this.sprselcheckbox.CheckedChanged += new System.EventHandler(this.sprselcheckbox_CheckedChanged);
            // 
            // gridPanel
            // 
            this.gridPanel.AllowDrop = true;
            this.gridPanel.AutoScrollMargin = new System.Drawing.Size(0, 200);
            this.gridPanel.BackColor = System.Drawing.Color.Transparent;
            this.gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridPanel.Location = new System.Drawing.Point(265, 251);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(600, 400);
            this.gridPanel.TabIndex = 40;
            this.gridPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridPanel_DragDrop);
            this.gridPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.gridPanel_DragOver);
            this.gridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gridPanel_Paint);
            this.gridPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridPanel_MouseClick);
            // 
            // settingsPanel
            // 
            this.settingsPanel.AllowDrop = true;
            this.settingsPanel.AutoScrollMargin = new System.Drawing.Size(0, 200);
            this.settingsPanel.BackColor = System.Drawing.Color.Azure;
            this.settingsPanel.Controls.Add(this.GBalgorithm);
            this.settingsPanel.Controls.Add(this.envthreebtn);
            this.settingsPanel.Controls.Add(this.btnChooseDestination);
            this.settingsPanel.Controls.Add(this.btnVehicleStart);
            this.settingsPanel.Controls.Add(this.materialRaisedButton4);
            this.settingsPanel.Controls.Add(this.lblEVStatus);
            this.settingsPanel.Controls.Add(this.label7);
            this.settingsPanel.Controls.Add(this.tbSimulationTime);
            this.settingsPanel.Controls.Add(this.TrackBarSpeed);
            this.settingsPanel.Controls.Add(this.textBox1);
            this.settingsPanel.Controls.Add(this.label4);
            this.settingsPanel.Controls.Add(this.environmentslabel);
            this.settingsPanel.Controls.Add(this.envonebtn);
            this.settingsPanel.Controls.Add(this.trackBarPedestrians);
            this.settingsPanel.Controls.Add(this.envtwobtn);
            this.settingsPanel.Controls.Add(this.pdstrupper);
            this.settingsPanel.Controls.Add(this.pdstrlower);
            this.settingsPanel.Controls.Add(this.vehicleslabel);
            this.settingsPanel.Controls.Add(this.pedestrianslabel);
            this.settingsPanel.Controls.Add(this.sprselcheckbox);
            this.settingsPanel.Controls.Add(this.tbVehicelsAmount);
            this.settingsPanel.Controls.Add(this.cbEmergency);
            this.settingsPanel.Controls.Add(this.lblEmergency);
            this.settingsPanel.Controls.Add(this.surpriseelementslabel);
            this.settingsPanel.Controls.Add(this.materialTabSelector2);
            this.settingsPanel.Location = new System.Drawing.Point(862, 50);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(363, 870);
            this.settingsPanel.TabIndex = 44;
            this.settingsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.settingsPanel_Paint);
            // 
            // GBalgorithm
            // 
            this.GBalgorithm.BackColor = System.Drawing.Color.DarkSlateGray;
            this.GBalgorithm.Controls.Add(this.rbAStar);
            this.GBalgorithm.Controls.Add(this.rbDijkstra);
            this.GBalgorithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GBalgorithm.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GBalgorithm.Location = new System.Drawing.Point(114, 496);
            this.GBalgorithm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GBalgorithm.Name = "GBalgorithm";
            this.GBalgorithm.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GBalgorithm.Size = new System.Drawing.Size(182, 43);
            this.GBalgorithm.TabIndex = 40;
            this.GBalgorithm.TabStop = false;
            this.GBalgorithm.Text = "Algorithm";
            // 
            // rbAStar
            // 
            this.rbAStar.AutoSize = true;
            this.rbAStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAStar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbAStar.Location = new System.Drawing.Point(32, 21);
            this.rbAStar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbAStar.Name = "rbAStar";
            this.rbAStar.Size = new System.Drawing.Size(39, 19);
            this.rbAStar.TabIndex = 1;
            this.rbAStar.TabStop = true;
            this.rbAStar.Text = "A*";
            this.rbAStar.UseVisualStyleBackColor = true;
            this.rbAStar.CheckedChanged += new System.EventHandler(this.rbAStar_CheckedChanged);
            // 
            // rbDijkstra
            // 
            this.rbDijkstra.AutoSize = true;
            this.rbDijkstra.Checked = true;
            this.rbDijkstra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbDijkstra.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbDijkstra.Location = new System.Drawing.Point(92, 21);
            this.rbDijkstra.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbDijkstra.Name = "rbDijkstra";
            this.rbDijkstra.Size = new System.Drawing.Size(74, 19);
            this.rbDijkstra.TabIndex = 0;
            this.rbDijkstra.TabStop = true;
            this.rbDijkstra.Text = "Dijkstra";
            this.rbDijkstra.UseVisualStyleBackColor = true;
            this.rbDijkstra.CheckedChanged += new System.EventHandler(this.rbDijkstra_CheckedChanged);
            // 
            // envthreebtn
            // 
            this.envthreebtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.envthreebtn.BackgroundImage = global::TrafficDemo.Properties.Resources.trafficWhite;
            this.envthreebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.envthreebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.envthreebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.envthreebtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.envthreebtn.Location = new System.Drawing.Point(60, 496);
            this.envthreebtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.envthreebtn.Name = "envthreebtn";
            this.envthreebtn.Size = new System.Drawing.Size(46, 42);
            this.envthreebtn.TabIndex = 13;
            this.envthreebtn.UseVisualStyleBackColor = false;
            this.envthreebtn.Click += new System.EventHandler(this.envthreebtn_Click);
            this.envthreebtn.MouseHover += new System.EventHandler(this.envthreebtn_MouseHover);
            // 
            // btnChooseDestination
            // 
            this.btnChooseDestination.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnChooseDestination.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseDestination.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChooseDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChooseDestination.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChooseDestination.Location = new System.Drawing.Point(202, 544);
            this.btnChooseDestination.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnChooseDestination.Name = "btnChooseDestination";
            this.btnChooseDestination.Size = new System.Drawing.Size(94, 29);
            this.btnChooseDestination.TabIndex = 33;
            this.btnChooseDestination.Text = "Destination";
            this.btnChooseDestination.UseVisualStyleBackColor = false;
            this.btnChooseDestination.Click += new System.EventHandler(this.btnChooseDestination_Click);
            // 
            // btnVehicleStart
            // 
            this.btnVehicleStart.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnVehicleStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVehicleStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVehicleStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnVehicleStart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVehicleStart.Location = new System.Drawing.Point(60, 544);
            this.btnVehicleStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnVehicleStart.Name = "btnVehicleStart";
            this.btnVehicleStart.Size = new System.Drawing.Size(94, 29);
            this.btnVehicleStart.TabIndex = 33;
            this.btnVehicleStart.Text = "Start";
            this.btnVehicleStart.UseVisualStyleBackColor = false;
            this.btnVehicleStart.Click += new System.EventHandler(this.btnVehicleStart_Click);
            // 
            // materialRaisedButton4
            // 
            this.materialRaisedButton4.Depth = 0;
            this.materialRaisedButton4.Location = new System.Drawing.Point(62, 665);
            this.materialRaisedButton4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialRaisedButton4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton4.Name = "materialRaisedButton4";
            this.materialRaisedButton4.Primary = true;
            this.materialRaisedButton4.Size = new System.Drawing.Size(187, 51);
            this.materialRaisedButton4.TabIndex = 55;
            this.materialRaisedButton4.Text = "Reset Default Values";
            this.materialRaisedButton4.UseVisualStyleBackColor = true;
            this.materialRaisedButton4.Click += new System.EventHandler(this.materialRaisedButton4_Click_1);
            // 
            // lblEVStatus
            // 
            this.lblEVStatus.AutoSize = true;
            this.lblEVStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEVStatus.Location = new System.Drawing.Point(79, 652);
            this.lblEVStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEVStatus.Name = "lblEVStatus";
            this.lblEVStatus.Size = new System.Drawing.Size(0, 20);
            this.lblEVStatus.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(98, 201);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 24);
            this.label7.TabIndex = 38;
            this.label7.Text = "Simulation Time";
            // 
            // tbSimulationTime
            // 
            this.tbSimulationTime.Location = new System.Drawing.Point(155, 241);
            this.tbSimulationTime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSimulationTime.Name = "tbSimulationTime";
            this.tbSimulationTime.Size = new System.Drawing.Size(34, 20);
            this.tbSimulationTime.TabIndex = 37;
            this.tbSimulationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TrackBarSpeed
            // 
            this.TrackBarSpeed.BackColor = System.Drawing.Color.DarkSlateGray;
            this.TrackBarSpeed.Location = new System.Drawing.Point(118, 427);
            this.TrackBarSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TrackBarSpeed.Maximum = 15;
            this.TrackBarSpeed.Minimum = 1;
            this.TrackBarSpeed.Name = "TrackBarSpeed";
            this.TrackBarSpeed.Size = new System.Drawing.Size(132, 45);
            this.TrackBarSpeed.TabIndex = 34;
            this.TrackBarSpeed.Value = 1;
            this.TrackBarSpeed.Scroll += new System.EventHandler(this.TrackBarSpeed_Scroll);
            this.TrackBarSpeed.ValueChanged += new System.EventHandler(this.TrackBarSpeed_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(253, 427);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(34, 20);
            this.textBox1.TabIndex = 35;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(92, 392);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 24);
            this.label4.TabIndex = 36;
            this.label4.Text = "Simulation speed";
            // 
            // envonebtn
            // 
            this.envonebtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.envonebtn.BackgroundImage = global::TrafficDemo.Properties.Resources.sunWhite;
            this.envonebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.envonebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.envonebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.envonebtn.Location = new System.Drawing.Point(108, 52);
            this.envonebtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.envonebtn.Name = "envonebtn";
            this.envonebtn.Size = new System.Drawing.Size(46, 42);
            this.envonebtn.TabIndex = 15;
            this.envonebtn.UseVisualStyleBackColor = false;
            this.envonebtn.Click += new System.EventHandler(this.envonebtn_Click);
            this.envonebtn.MouseHover += new System.EventHandler(this.envonebtn_MouseHover);
            // 
            // envtwobtn
            // 
            this.envtwobtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.envtwobtn.BackgroundImage = global::TrafficDemo.Properties.Resources.moonWhite;
            this.envtwobtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.envtwobtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.envtwobtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.envtwobtn.Location = new System.Drawing.Point(198, 52);
            this.envtwobtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.envtwobtn.Name = "envtwobtn";
            this.envtwobtn.Size = new System.Drawing.Size(46, 42);
            this.envtwobtn.TabIndex = 14;
            this.envtwobtn.UseVisualStyleBackColor = false;
            this.envtwobtn.Click += new System.EventHandler(this.envtwobtn_Click);
            this.envtwobtn.MouseHover += new System.EventHandler(this.envtwobtn_MouseHover);
            // 
            // cbEmergency
            // 
            this.cbEmergency.AutoSize = true;
            this.cbEmergency.Location = new System.Drawing.Point(275, 597);
            this.cbEmergency.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbEmergency.Name = "cbEmergency";
            this.cbEmergency.Size = new System.Drawing.Size(15, 14);
            this.cbEmergency.TabIndex = 32;
            this.cbEmergency.UseVisualStyleBackColor = true;
            this.cbEmergency.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblEmergency
            // 
            this.lblEmergency.AutoSize = true;
            this.lblEmergency.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblEmergency.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEmergency.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblEmergency.Location = new System.Drawing.Point(60, 587);
            this.lblEmergency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(204, 24);
            this.lblEmergency.TabIndex = 30;
            this.lblEmergency.Text = "Emergency Vehicles";
            this.lblEmergency.Click += new System.EventHandler(this.lblEmergency_Click);
            // 
            // materialTabSelector2
            // 
            this.materialTabSelector2.BaseTabControl = null;
            this.materialTabSelector2.Depth = 0;
            this.materialTabSelector2.Location = new System.Drawing.Point(0, 0);
            this.materialTabSelector2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialTabSelector2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector2.Name = "materialTabSelector2";
            this.materialTabSelector2.Size = new System.Drawing.Size(347, 847);
            this.materialTabSelector2.TabIndex = 51;
            this.materialTabSelector2.Text = "materialTabSelector2";
            this.materialTabSelector2.Click += new System.EventHandler(this.materialTabSelector2_Click);
            // 
            // dragAndDropPanel
            // 
            this.dragAndDropPanel.AllowDrop = true;
            this.dragAndDropPanel.AutoScrollMargin = new System.Drawing.Size(0, 200);
            this.dragAndDropPanel.BackColor = System.Drawing.Color.Transparent;
            this.dragAndDropPanel.Controls.Add(this.btnFlip);
            this.dragAndDropPanel.Controls.Add(this.TwoWay);
            this.dragAndDropPanel.Controls.Add(this.roundabout);
            this.dragAndDropPanel.Controls.Add(this.btnStop);
            this.dragAndDropPanel.Controls.Add(this.roadType1);
            this.dragAndDropPanel.Controls.Add(this.btnStart);
            this.dragAndDropPanel.Controls.Add(this.roadType2);
            this.dragAndDropPanel.Controls.Add(this.btnPause);
            this.dragAndDropPanel.Location = new System.Drawing.Point(265, 69);
            this.dragAndDropPanel.Name = "dragAndDropPanel";
            this.dragAndDropPanel.Size = new System.Drawing.Size(600, 152);
            this.dragAndDropPanel.TabIndex = 41;
            // 
            // btnFlip
            // 
            this.btnFlip.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnFlip.BackColor = System.Drawing.Color.Transparent;
            this.btnFlip.BackgroundImage = global::TrafficDemo.Properties.Resources.flip;
            this.btnFlip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFlip.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.btnFlip.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFlip.Location = new System.Drawing.Point(547, 34);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(39, 29);
            this.btnFlip.TabIndex = 42;
            this.btnFlip.UseVisualStyleBackColor = false;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // TwoWay
            // 
            this.TwoWay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TwoWay.Image = global::TrafficDemo.Properties.Resources.twoWay_h;
            this.TwoWay.Location = new System.Drawing.Point(462, 52);
            this.TwoWay.Name = "TwoWay";
            this.TwoWay.Size = new System.Drawing.Size(111, 96);
            this.TwoWay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TwoWay.TabIndex = 41;
            this.TwoWay.TabStop = false;
            this.TwoWay.Tag = "two-way-h";
            this.TwoWay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TwoWay_MouseDown);
            // 
            // roundabout
            // 
            this.roundabout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundabout.Image = global::TrafficDemo.Properties.Resources.roundabout;
            this.roundabout.Location = new System.Drawing.Point(318, 52);
            this.roundabout.Name = "roundabout";
            this.roundabout.Size = new System.Drawing.Size(111, 96);
            this.roundabout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roundabout.TabIndex = 40;
            this.roundabout.TabStop = false;
            this.roundabout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.roundabout_MouseDown);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.BackgroundImage = global::TrafficDemo.Properties.Resources.stop;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop.Location = new System.Drawing.Point(358, 0);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(46, 42);
            this.btnStop.TabIndex = 36;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.MouseHover += new System.EventHandler(this.btnStop_MouseHover);
            // 
            // roadType1
            // 
            this.roadType1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.roadType1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roadType1.ErrorImage = null;
            this.roadType1.Image = global::TrafficDemo.Properties.Resources.roadType1;
            this.roadType1.InitialImage = null;
            this.roadType1.Location = new System.Drawing.Point(10, 52);
            this.roadType1.Name = "roadType1";
            this.roadType1.Size = new System.Drawing.Size(111, 96);
            this.roadType1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roadType1.TabIndex = 38;
            this.roadType1.TabStop = false;
            this.roadType1.Click += new System.EventHandler(this.roadType1_Click);
            this.roadType1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.roadType1_MouseDown);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BackgroundImage = global::TrafficDemo.Properties.Resources.play;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Location = new System.Drawing.Point(202, 0);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(46, 42);
            this.btnStart.TabIndex = 34;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseHover += new System.EventHandler(this.btnStart_MouseHover);
            // 
            // roadType2
            // 
            this.roadType2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.roadType2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roadType2.Image = global::TrafficDemo.Properties.Resources.roadType2;
            this.roadType2.Location = new System.Drawing.Point(166, 52);
            this.roadType2.Name = "roadType2";
            this.roadType2.Size = new System.Drawing.Size(111, 96);
            this.roadType2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roadType2.TabIndex = 39;
            this.roadType2.TabStop = false;
            this.roadType2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.roadType2_MouseDown);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPause.BackgroundImage = global::TrafficDemo.Properties.Resources.pause;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPause.Location = new System.Drawing.Point(279, 0);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(46, 42);
            this.btnPause.TabIndex = 35;
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            this.btnPause.MouseHover += new System.EventHandler(this.btnPause_MouseHover);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeCrossingToolStripMenuItem,
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // removeCrossingToolStripMenuItem
            // 
            this.removeCrossingToolStripMenuItem.Name = "removeCrossingToolStripMenuItem";
            this.removeCrossingToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.removeCrossingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeCrossingToolStripMenuItem.Text = "&Remove Crossing";
            this.removeCrossingToolStripMenuItem.Click += new System.EventHandler(this.removeCrossingToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // listbox1
            // 
            this.listbox1.BackColor = System.Drawing.Color.White;
            this.listbox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listbox1.ForeColor = System.Drawing.Color.Black;
            this.listbox1.FormattingEnabled = true;
            this.listbox1.ItemHeight = 16;
            this.listbox1.Location = new System.Drawing.Point(664, 1395);
            this.listbox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.listbox1.Name = "listbox1";
            this.listbox1.Size = new System.Drawing.Size(606, 16);
            this.listbox1.TabIndex = 45;
            this.listbox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listbox1_DrawItem);
            this.listbox1.SelectedIndexChanged += new System.EventHandler(this.listbox1_SelectedIndexChanged);
            // 
            // timerActualTime
            // 
            this.timerActualTime.Interval = 1000;
            this.timerActualTime.Tick += new System.EventHandler(this.timerActualTime_Tick);
            // 
            // shortestPathCounter
            // 
            this.shortestPathCounter.Interval = 1000;
            this.shortestPathCounter.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // btnSmallGridSelected
            // 
            this.btnSmallGridSelected.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSmallGridSelected.BackgroundImage = global::TrafficDemo.Properties.Resources.stopWhite;
            this.btnSmallGridSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmallGridSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSmallGridSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSmallGridSelected.Location = new System.Drawing.Point(17, 364);
            this.btnSmallGridSelected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSmallGridSelected.Name = "btnSmallGridSelected";
            this.btnSmallGridSelected.Size = new System.Drawing.Size(46, 42);
            this.btnSmallGridSelected.TabIndex = 7;
            this.btnSmallGridSelected.UseVisualStyleBackColor = false;
            this.btnSmallGridSelected.Click += new System.EventHandler(this.btnSmallGridSelected_Click);
            this.btnSmallGridSelected.MouseHover += new System.EventHandler(this.btnSmallGridSelected_MouseHover);
            // 
            // btnMiddleGridSelected
            // 
            this.btnMiddleGridSelected.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnMiddleGridSelected.BackgroundImage = global::TrafficDemo.Properties.Resources.fourWhite;
            this.btnMiddleGridSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMiddleGridSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMiddleGridSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMiddleGridSelected.Location = new System.Drawing.Point(88, 364);
            this.btnMiddleGridSelected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnMiddleGridSelected.Name = "btnMiddleGridSelected";
            this.btnMiddleGridSelected.Size = new System.Drawing.Size(46, 42);
            this.btnMiddleGridSelected.TabIndex = 6;
            this.btnMiddleGridSelected.UseVisualStyleBackColor = false;
            this.btnMiddleGridSelected.Click += new System.EventHandler(this.btnMiddleGridSelected_Click);
            this.btnMiddleGridSelected.MouseHover += new System.EventHandler(this.btnMiddleGridSelected_MouseHover);
            // 
            // btnLargeGridSelected
            // 
            this.btnLargeGridSelected.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnLargeGridSelected.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLargeGridSelected.BackgroundImage")));
            this.btnLargeGridSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLargeGridSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLargeGridSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLargeGridSelected.Location = new System.Drawing.Point(158, 364);
            this.btnLargeGridSelected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLargeGridSelected.Name = "btnLargeGridSelected";
            this.btnLargeGridSelected.Size = new System.Drawing.Size(46, 42);
            this.btnLargeGridSelected.TabIndex = 5;
            this.btnLargeGridSelected.UseVisualStyleBackColor = false;
            this.btnLargeGridSelected.Click += new System.EventHandler(this.btnLargeGridSelected_Click);
            this.btnLargeGridSelected.MouseHover += new System.EventHandler(this.btnLargeGridSelected_MouseHover);
            // 
            // saveasbtn
            // 
            this.saveasbtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.saveasbtn.BackgroundImage = global::TrafficDemo.Properties.Resources.saveasWhite;
            this.saveasbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveasbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveasbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveasbtn.Location = new System.Drawing.Point(158, 80);
            this.saveasbtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveasbtn.Name = "saveasbtn";
            this.saveasbtn.Size = new System.Drawing.Size(46, 42);
            this.saveasbtn.TabIndex = 2;
            this.saveasbtn.UseVisualStyleBackColor = false;
            this.saveasbtn.Click += new System.EventHandler(this.saveasbtn_Click);
            this.saveasbtn.MouseHover += new System.EventHandler(this.saveasbtn_MouseHover);
            // 
            // savebtn
            // 
            this.savebtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.savebtn.BackgroundImage = global::TrafficDemo.Properties.Resources.saveWhite;
            this.savebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.savebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.savebtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.savebtn.Location = new System.Drawing.Point(88, 80);
            this.savebtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(46, 42);
            this.savebtn.TabIndex = 1;
            this.savebtn.UseVisualStyleBackColor = false;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            this.savebtn.MouseHover += new System.EventHandler(this.savebtn_MouseHover);
            // 
            // openbtn
            // 
            this.openbtn.BackColor = System.Drawing.Color.DarkSlateGray;
            this.openbtn.BackgroundImage = global::TrafficDemo.Properties.Resources.openWhite;
            this.openbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.openbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.openbtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openbtn.Location = new System.Drawing.Point(17, 80);
            this.openbtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(46, 42);
            this.openbtn.TabIndex = 0;
            this.openbtn.UseVisualStyleBackColor = false;
            this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
            this.openbtn.MouseHover += new System.EventHandler(this.openbtn_MouseHover);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Header.Cursor = System.Windows.Forms.Cursors.Default;
            this.Header.Image = ((System.Drawing.Image)(resources.GetObject("Header.Image")));
            this.Header.Location = new System.Drawing.Point(49, 155);
            this.Header.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(143, 145);
            this.Header.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Header.TabIndex = 48;
            this.Header.TabStop = false;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(17, 449);
            this.materialRaisedButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(187, 51);
            this.materialRaisedButton1.TabIndex = 49;
            this.materialRaisedButton1.Text = "Current Flow of Cars";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            this.materialRaisedButton1.MouseHover += new System.EventHandler(this.materialRaisedButton1_MouseHover_1);
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = null;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 50);
            this.materialTabSelector1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(240, 847);
            this.materialTabSelector1.TabIndex = 50;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(176, 224);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(64, 19);
            this.materialDivider1.TabIndex = 51;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialDivider2
            // 
            this.materialDivider2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(0, 224);
            this.materialDivider2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(64, 19);
            this.materialDivider2.TabIndex = 52;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Location = new System.Drawing.Point(17, 517);
            this.materialRaisedButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialRaisedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            this.materialRaisedButton2.Size = new System.Drawing.Size(187, 51);
            this.materialRaisedButton2.TabIndex = 53;
            this.materialRaisedButton2.Text = "Clear All";
            this.materialRaisedButton2.UseVisualStyleBackColor = true;
            this.materialRaisedButton2.Click += new System.EventHandler(this.materialRaisedButton2_Click);
            // 
            // materialRaisedButton3
            // 
            this.materialRaisedButton3.Depth = 0;
            this.materialRaisedButton3.Location = new System.Drawing.Point(17, 585);
            this.materialRaisedButton3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialRaisedButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton3.Name = "materialRaisedButton3";
            this.materialRaisedButton3.Primary = true;
            this.materialRaisedButton3.Size = new System.Drawing.Size(187, 51);
            this.materialRaisedButton3.TabIndex = 54;
            this.materialRaisedButton3.Text = "Generate Report";
            this.materialRaisedButton3.UseVisualStyleBackColor = true;
            this.materialRaisedButton3.Click += new System.EventHandler(this.materialRaisedButton3_Click);
            // 
            // materialTabSelector3
            // 
            this.materialTabSelector3.BaseTabControl = null;
            this.materialTabSelector3.Depth = 0;
            this.materialTabSelector3.Location = new System.Drawing.Point(231, 681);
            this.materialTabSelector3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialTabSelector3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector3.Name = "materialTabSelector3";
            this.materialTabSelector3.Size = new System.Drawing.Size(634, 195);
            this.materialTabSelector3.TabIndex = 55;
            this.materialTabSelector3.Text = "materialTabSelector3";
            // 
            // materialRaisedButton5
            // 
            this.materialRaisedButton5.Depth = 0;
            this.materialRaisedButton5.Location = new System.Drawing.Point(17, 652);
            this.materialRaisedButton5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.materialRaisedButton5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton5.Name = "materialRaisedButton5";
            this.materialRaisedButton5.Primary = true;
            this.materialRaisedButton5.Size = new System.Drawing.Size(187, 51);
            this.materialRaisedButton5.TabIndex = 56;
            this.materialRaisedButton5.Text = "Statistics";
            this.materialRaisedButton5.UseVisualStyleBackColor = true;
            this.materialRaisedButton5.Click += new System.EventHandler(this.materialRaisedButton5_Click);
            // 
            // materialTabSelector4
            // 
            this.materialTabSelector4.BaseTabControl = null;
            this.materialTabSelector4.Depth = 0;
            this.materialTabSelector4.Location = new System.Drawing.Point(231, 50);
            this.materialTabSelector4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector4.Name = "materialTabSelector4";
            this.materialTabSelector4.Size = new System.Drawing.Size(655, 653);
            this.materialTabSelector4.TabIndex = 43;
            this.materialTabSelector4.Text = "materialTabSelector4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1208, 876);
            this.Controls.Add(this.dragAndDropPanel);
            this.Controls.Add(this.materialRaisedButton5);
            this.Controls.Add(this.materialRaisedButton3);
            this.Controls.Add(this.materialRaisedButton2);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.listbox1);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.btnSmallGridSelected);
            this.Controls.Add(this.btnMiddleGridSelected);
            this.Controls.Add(this.btnLargeGridSelected);
            this.Controls.Add(this.gridsizelabel);
            this.Controls.Add(this.saveasbtn);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.openbtn);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabSelector3);
            this.Controls.Add(this.materialTabSelector4);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Sizable = false;
            this.Text = "Hyena Crossing Traffic Simulation";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPedestrians)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.GBalgorithm.ResumeLayout(false);
            this.GBalgorithm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSpeed)).EndInit();
            this.dragAndDropPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TwoWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundabout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadType2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Header)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button openbtn;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Button saveasbtn;
        private System.Windows.Forms.Label gridsizelabel;
        private System.Windows.Forms.Button btnSmallGridSelected;
        private System.Windows.Forms.Button btnMiddleGridSelected;
        private System.Windows.Forms.Button btnLargeGridSelected;
        private System.Windows.Forms.Button envthreebtn;
        private System.Windows.Forms.Label environmentslabel;
        private System.Windows.Forms.Label vehicleslabel;
        private System.Windows.Forms.Label pedestrianslabel;
        private System.Windows.Forms.TrackBar trackBarPedestrians;
        private System.Windows.Forms.Label surpriseelementslabel;
        private System.Windows.Forms.CheckBox sprselcheckbox;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Panel dragAndDropPanel;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox roadType1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox roadType2;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeCrossingToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ListBox listbox1;
       private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Timer timerActualTime;
        private System.Windows.Forms.Timer shortestPathCounter;
        private System.Windows.Forms.PictureBox roundabout;
        private System.Windows.Forms.PictureBox TwoWay;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSimulationTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbEmergency;
        private System.Windows.Forms.Label lblEmergency;
        private System.Windows.Forms.Button btnChooseDestination;
        private System.Windows.Forms.Button btnVehicleStart;
        public System.Windows.Forms.TrackBar TrackBarSpeed;
        private System.Windows.Forms.Label lblEVStatus;
        public System.Windows.Forms.TextBox pdstrupper;
        public System.Windows.Forms.TextBox pdstrlower;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox tbVehicelsAmount;
        public System.Windows.Forms.Button envtwobtn;
        public System.Windows.Forms.Button envonebtn;
        private System.Windows.Forms.GroupBox GBalgorithm;
        private System.Windows.Forms.RadioButton rbAStar;
        private System.Windows.Forms.RadioButton rbDijkstra;
        private System.Windows.Forms.PictureBox Header;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton2;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton3;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton4;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector3;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton5;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector4;
    }
}

