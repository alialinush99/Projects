using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using TrafficDemo;
using TrafficDemo.Classes;
using System.Collections.Generic;

namespace Traffic_Simulation_Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// A test method to verify whether it is possible to start the timers of the applications
        /// </summary>
        [TestMethod]
        public void Start_Test()
        {
            //Arrange
            Grid g = new Grid(3, 6);

            //Act
            g.StartTimers();

            //Assert
            Assert.IsTrue(g.StartTimers());
        }

        /// <summary>
        /// A test method to verify whether it is possible to stop the timers of the application
        /// </summary>
        [TestMethod]
        public void Stop_Test()
        {
            //Arrange
            Grid g = new Grid(3, 6);

            //Act
            g.StopSimulation();

            //Assert
            Assert.IsTrue(g.StopSimulation());
        }

        /// <summary>
        /// A test method to verify whether it is possible to pause the timers of the application
        /// </summary>
        [TestMethod]
        public void Pause_Test()
        {
            //Arrange
            Grid g = new Grid(3, 6);

            //Act
            g.PauseTimers();

            //Assert
            Assert.IsTrue(g.PauseTimers());
        }


        /// <summary>
        /// A test method to verify whether it is possible to clear road and picture from grid (Clear Grid)
        /// </summary>

        [TestMethod]

        public void ResetLayout_Test()
        {
            //Arrange
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });
            PictureBox pb_b = new PictureBox();

            //Act
            f.InitializeGridTesting();
            f.ReturnGrid().AddRoad(new Road(new Point(5, 3), pb_b), 1);
            f.ReturnPictureBoxes().Add(pb_b);

            f.ClearGrid();

            //Assert
            Assert.AreEqual(0, f.ReturnGrid().Roads.Count);
            Assert.AreEqual(0, f.ReturnPictureBoxes().Count);
        }


        /// <summary>
        /// A test method to verify whether it is possible to add roads in simulation
        /// </summary>

        [TestMethod]

        public void Add_Road_Test()
        {
            //Arrange
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });
            PictureBox pb_b = new PictureBox();

            //Act
            f.InitializeGridTesting();
            f.ReturnGrid().AddRoad(new Road(new Point(4, 5), pb_b), 1);
            f.ReturnGrid().AddRoad(new Road(new Point(5, 6), pb_b), 2);

            //Assert
            Assert.AreEqual(2, f.ReturnGrid().Roads.Count);

        }

        /// <summary>
        /// A test method to verify whether removing a non-existing road works
        /// </summary>

        [TestMethod]

        public void Remove_Road_Test()
        {
            //Arrange
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });
            PictureBox pb_b = new PictureBox();

            //Act
            f.InitializeGridTesting();

            //Assert
            Assert.IsFalse(f.ReturnGrid().RemoveCrossing(2));
        }

        /// <summary>
        /// a test methods to verify that roads are created with no cars 
        /// </summary>

        [TestMethod]
        public void NumbersOfCarsGenerated_ShouldBeZero()
        {
            //Arrange
            Road road = new RoadType1(new Point(0, 0), new PictureBox());


            //Act
            // Nothing to do here 

            //Assert
            Assert.AreEqual(0, road.CarsOnRoad);
        }

        /// <summary>
        /// a test methods to verify that roads have the proper amount of cars generated on them 
        /// </summary>

        [TestMethod]
        public void NumbersOfCarsGenerated_ShouldBeEqualToAmount()
        {
            //Arrange
            Road road = new RoadType1(new Point(0, 0), new PictureBox());
            int amount = 5;

            //Act
            road.AddCars(road.Lanes[0], amount);
            int sumOfCarsOnThisRoadLanes = 0;
            foreach (Lane L in road.Lanes)
            {
                sumOfCarsOnThisRoadLanes += L.Cars.Count;
            }

            //Assert
            // Roads should have the amount specified, and the sum of all cars on that road lanes should be that amount too

            Assert.AreEqual(amount, road.CarsOnRoad);
            Assert.AreEqual(amount, sumOfCarsOnThisRoadLanes);

        }




        [TestMethod]
        public void Change_Simulation_Speed_Should_Change()
        {
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });
            PictureBox pb_b = new PictureBox();

            f.InitializeGridTesting();
            f.ReturnGrid().AddRoad(new Road(new Point(5, 3), pb_b), 1);
            f.ReturnPictureBoxes().Add(pb_b);

            f.TrackBarSpeed.Value = 5;
            Assert.AreEqual(5, f.ReturnGrid().SimSpeed);

            f.TrackBarSpeed.Value = 10;
            Assert.AreEqual(10, f.ReturnGrid().SimSpeed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Change_Trackbar_Simulation_Speed_Invalid_Value_Should_Throw_Exception()
        {
            var f = new Form1();
            f.TrackBarSpeed.Value = 0;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Invalid simulation speed value.")]
        public void Change_Grid_Simulation_Speed_Invalid_Value_Should_Throw_Exception()
        {
            var f = new Form1();
            f.InitializeGridTesting();
            f.ReturnGrid().SimSpeed = 0;
        }


        [TestMethod]
        public void Save_Test()
        {
            //Arrange
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });

            //Act
            f.InitializeGridTesting();
            string path = @"D:\TestExample1";
            // FileStream fs = File.Create(path);

            //Assert
            Assert.IsTrue(f.Save(@"D:\TestExample1"));
        }

        [TestMethod]

        public void Load_Test()
        {
            //Arrange
            var f = new Form1();
            var createControl = f.GetType().GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.NonPublic);
            createControl.Invoke(f, new object[] { true });

            //Act
            string path = @"D:\TestExample1";
            f.loadFile(@"D:\TestExample1");

            //Assert
            Assert.IsNotNull(f.ReturnGrid());
            Assert.IsNotNull(f.ReturnGrid().Roads);
            Assert.IsNotNull(f.ReturnPictureBoxes());
        }


        /// <summary>
        /// Algorithm components testing summary  
        /// </summary>
        /// 


        // Test if the Dijkstra algorithm actually gives out the shortest path from start to target
        [TestMethod]
        public void CheckIfDijkstraShortestPathIsCorrect_ShouldGiveCorrectNodesAndLength()
        {
            // Arrange

            // ----- Creating the points ------
            Point start = new Point(0, 0);
            Point p2 = new Point(200, 300);
            Point p3 = new Point(150, 200);
            Point p4 = new Point(500, 100);
            Point p5 = new Point(50, 500);
            Point target = new Point(200, 200);

            // ----------- Adding the points to nodes --------------
            Node startNode = new Node(start);
            Node endNode = new Node(target);
            Node n2 = new Node(p2);
            Node n3 = new Node(p3);
            Node n4 = new Node(p4);
            Node n5 = new Node(p5);

            //------ Adding nodes to the tracker -------
            GPSTracker tracker = new GPSTracker();

            tracker.End = endNode;
            tracker.Start = startNode;
            tracker.Nodes.Add(startNode);
            tracker.Nodes.Add(endNode);
            tracker.Nodes.Add(n2);
            tracker.Nodes.Add(n3);
            tracker.Nodes.Add(n4);
            tracker.Nodes.Add(n5);

            // -------- Connecting the nodes -------------
            foreach (Node N in tracker.Nodes)
            {
                N.ConnectClosestNodes(tracker.Nodes, 1);
            }


            // Act 

            List<Node> shortestPath = tracker.GetShortesPathDijkstra();
            List<Node> actualShortestPath = new List<Node>();
            actualShortestPath.Add(startNode);
            actualShortestPath.Add(n3);
            actualShortestPath.Add(endNode);

            // Assert 

            Assert.AreEqual(actualShortestPath.Count, shortestPath.Count);
            Assert.AreEqual(actualShortestPath[0], shortestPath[0]);
            Assert.AreEqual(actualShortestPath[1], shortestPath[1]);
            Assert.AreEqual(actualShortestPath[2], shortestPath[2]);

        }


        // Check if we're getting all the possible points and lanes on the grid 
        [TestMethod]
        public void CheckIfAllThePointsOnGridAreReturned_ShouldGiveAllLanesPairedWithPoints()
        {
            // Arrange
            Grid myGrid = new Grid(3, 3);
            Road testRoad = new RoadType1(new Point(0, 0), new PictureBox());
            myGrid.AddRoad(testRoad, 0);

            // Act 
            Dictionary<Lane, List<Point>> allPoints = myGrid.GetAllPointsOnGrid();
            int pointSum = 0;
            foreach (KeyValuePair<Lane, List<Point>> kvp in allPoints)
            {
                foreach (Point P in kvp.Value)
                {
                    pointSum += 1;
                }
            }
            //Assert 
            // Note: RoadType 1 have 12 lanes, with 1 starting point each
            Assert.AreEqual(12, allPoints.Count);
            Assert.AreEqual(12, pointSum);
        }

        // Clear default values test 
        [TestMethod]
        public void ClearDefaultValues_ShouldClear()
        {

            // Arrange
            var form = new Form1();
            form.tbVehicelsAmount.Text = "1";
            form.pdstrlower.Text = "2";
            form.pdstrupper.Text = "3";
          


            // Act 
            form.ResetValues();
        

            //Assert 
            Assert.AreEqual("", form.tbVehicelsAmount.Text);
            Assert.AreEqual("", form.pdstrlower.Text);
            Assert.AreEqual("", form.pdstrupper.Text);
            
        }

        [TestMethod]
        public void CheckNightMode_ShouldChangeBacgroundColor()
        {
            // Arrange
            var frm = new Form1();

            // Act 
            frm.envtwobtn_Click(this,null);

            //Assert 
            Assert.AreEqual(Color.DarkSlateBlue, frm.BackColor);
        }

        [TestMethod]
        public void CheckDayMode_ShouldChangeBacgroundColor()
        {
            // Arrange
            var frm = new Form1();

            // Act 
            frm.envonebtn_Click(this, null);

            //Assert 
            Assert.AreEqual(Color.White, frm.BackColor);
        }

        [TestMethod]
        public void OverallInsights_ShouldShowGraphs()
        {
            // Arrange
            var frm = new StatisticsView();
            
            // Act 
            frm.btnOverall_Click(this, null);

            //Assert 
            Assert.AreEqual(14,frm.panel1.Controls.Count);
        }
        [TestMethod]
        public void Check_StatisticsView_Loading_Should_Load()
        {
            var frm = new Form1();
            var stFrm = new StatisticsView();
            frm.TestStatisticsFormLoading(ref stFrm);
            Assert.AreEqual(true, stFrm.Enabled);
            stFrm.Close();
        }



        /// <summary>
        /// Algorithm components testing summary  
        /// </summary>
        /// 


        // Test if the AStart algorithm actually gives out the shortest path from start to target
        [TestMethod]
        public void CheckIfAStartShortestPathIsCorrect_ShouldGiveCorrectNodesAndLength()
        {
            // Arrange

            // ----- Creating the points ------
            Point start = new Point(0, 0);
            Point p2 = new Point(200, 300);
            Point p3 = new Point(150, 200);
            Point p4 = new Point(500, 100);
            Point p5 = new Point(50, 500);
            Point target = new Point(200, 200);

            // ----------- Adding the points to nodes --------------
            Node startNode = new Node(start);
            Node endNode = new Node(target);
            Node n2 = new Node(p2);
            Node n3 = new Node(p3);
            Node n4 = new Node(p4);
            Node n5 = new Node(p5);

            //------ Adding nodes to the tracker -------
            GPSTracker tracker = new GPSTracker();

            tracker.End = endNode;
            tracker.Start = startNode;
            tracker.Nodes.Add(startNode);
            tracker.Nodes.Add(endNode);
            tracker.Nodes.Add(n2);
            tracker.Nodes.Add(n3);
            tracker.Nodes.Add(n4);
            tracker.Nodes.Add(n5);

            // -------- Connecting the nodes -------------
            foreach (Node N in tracker.Nodes)
            {
                N.ConnectClosestNodes(tracker.Nodes, 1);
            }


            // Act 

            List<Node> shortestPath = tracker.GetShortestPathAstar();
            List<Node> actualShortestPath = new List<Node>();
            actualShortestPath.Add(startNode);
            actualShortestPath.Add(n3);
            actualShortestPath.Add(endNode);

            // Assert 

            Assert.AreEqual(actualShortestPath.Count, shortestPath.Count);
            Assert.AreEqual(actualShortestPath[0], shortestPath[0]);
            Assert.AreEqual(actualShortestPath[1], shortestPath[1]);
            Assert.AreEqual(actualShortestPath[2], shortestPath[2]);

        }
        [TestMethod]
        public void Check_ShouldSaveAsPDF()
        {
            //var statsView = new StatisticsView();
            //string filename = "fileTest.pdf";
            //statsView.Export(filename);
            //Assert.AreEqual(filename,)
        }

    }
}
