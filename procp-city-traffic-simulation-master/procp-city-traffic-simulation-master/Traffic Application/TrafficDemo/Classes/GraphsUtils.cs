
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace TrafficDemo.Classes
{
    public class GraphsUtils
    {
        Dictionary<string, string> months = new Dictionary<string, string>()
        {
            { "1", "JAN"},
            { "2", "FEB" },
            { "3", "MAR"},
            { "4", "APR"},
            { "5", "MAY"},
            { "6", "JUN"},
            { "7", "JUL"},
            { "8", "AUG"},
            { "9", "SEP"},
            { "10", "OCT"},
            { "11", "NOV"},
            { "12", "DEC"},
        };
        private ReportsData data;
        private Panel graphsPanel;
        public GraphsUtils(ReportsData data, Panel graphsPanel)
        {
            this.data = data;
            this.graphsPanel = graphsPanel;
        }

        private void AddChartToForm(Control chart, Point location)
        {
            chart.Location = location;
            chart.AutoSize = false;
            chart.Size = new Size(309, 236);
            chart.BackColor = System.Drawing.Color.Transparent;
            chart.Parent = graphsPanel;
            graphsPanel.SendToBack();
            chart.BackgroundImageLayout = ImageLayout.None;
            graphsPanel.Controls.Add(chart);
            chart.BringToFront();
        }
        private void AddHeatMapToForm(LiveCharts.WinForms.CartesianChart chart, Point location)
        {
            chart.Location = location;
            chart.AutoSize = false;
            chart.Size = new Size(570, 480);
            chart.BackColor = System.Drawing.Color.Transparent;
            chart.Parent = graphsPanel;
            graphsPanel.SendToBack();
            chart.BackgroundImageLayout = ImageLayout.None;
            graphsPanel.Controls.Add(chart);
            chart.BringToFront();
        }
        private void GenerateChartLabel(string title, Point position)
        {
            Label chartTitle = new Label
            {
                Text = title
,
                Font = new Font("Calibri", 12, FontStyle.Bold),
                Location = position,
                AutoSize = false,
                Size = new Size(300, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            graphsPanel.Controls.Add(chartTitle);
        }
        public void SimulationsPerGrid()
        {
            GenerateChartLabel("Number of Simulations" + "\n" + "Per Grid Size", new Point(80, 10));

            LiveCharts.WinForms.PieChart pieChart1 = new LiveCharts.WinForms.PieChart();
            pieChart1.InnerRadius = 30;
            pieChart1.LegendLocation = LegendLocation.Right;
            AddChartToForm(pieChart1, new Point(100, 66));

            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.OnlySender
            };

            pieChart1.DataTooltip = tooltip;

            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Small",
                    Values = new ChartValues<double> {data.GetSmallGridSimulationsCount()},
                    PushOut = 5,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Medium",
                    Values = new ChartValues<double> {data.GetMediumGridSimulationsCount()},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Large",
                    Values = new ChartValues<double> {data.GetLargeGridSimulationsCount()},
                    DataLabels = true
                },
            };

        }
        private int FindMonthByValue(List<Dictionary<int, int>> records, int value)
        {
            int month = -1;
            foreach (var item in records)
            {
                if (month == -1)
                {
                    foreach (var rec in item)
                    {
                        if (rec.Value == value)
                        {
                            month = rec.Key - 1;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return month;
        }
        public LiveCharts.WinForms.CartesianChart SimulationsPerMonth()
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            GenerateChartLabel("Number of Simulations" + "\n" + "Per Month ", new Point(570, 10));
            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.OnlySender
            };
            AddChartToForm(cartesianChart1, new Point(550, 72));
            //lets take the first 15 records by default;
            var records = new Dictionary<string, Dictionary<int, int>>();
            months.Keys.ToList().ForEach(m =>
            {
                data.Reports.Keys.ToList().ForEach(k =>
                {
                    if (!records.ContainsKey(k))
                    {
                        records.Add(k, new Dictionary<int, int>());
                    }
                    records[k].Add(int.Parse(m), data.Reports[k].FindAll(r => r.Creation.Month.ToString() == m).Count());
                });
            });
            cartesianChart1.Series = new SeriesCollection();

            var lbls = new string[12];
            records.Keys.ToList().ForEach(y => {
                records[y].Keys.ToList().ForEach(m => {
                    if (records[y][m] > 0)
                    {
                        lbls[m - 1] = months[m.ToString()];
                    }
                    else
                    {
                        lbls[m - 1] = "";
                    }
                });
            });
            var mapper = Mappers.Xy<int>()
                .X(r => FindMonthByValue(records.Values.ToList(), r))
                .Y(r => r);

            records.Keys.ToList().ForEach(y =>
              lbls.ToList().ForEach(m =>
              {

                  int count = records[y].Values.ToList()[lbls.ToList().IndexOf(m)];
                  cartesianChart1.Series.Add(new ColumnSeries
                  {
                      Title = y,
                      Configuration = mapper,
                      Values = new ChartValues<int>() { count }
                  });
              })
            );

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = lbls.ToList(),
                DisableAnimations = false,
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            return cartesianChart1;
        }
        public void DistributionOfCarsAndAccidentsByNumberOfRoads()
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            GenerateChartLabel("Distrubution of Entered Cars & Accidents" + "\n" + "Per Number of Roads ", new Point(1000, 10));
            AddChartToForm(cartesianChart1, new Point(1000, 72));
            var lbls = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Dictionary<string, int> accidents = new Dictionary<string, int>();
            Dictionary<string, int> carsEntered = new Dictionary<string, int>();
            lbls.ToList().ForEach(l =>
            {
                data.Reports.Values.ToList().ForEach(y =>
                {
                    y.ForEach(r =>
                    {
                        if (r.NumberOfRoads.ToString() == l)
                        {
                            if (accidents.ContainsKey(l))
                            {
                                accidents[l] += r.Accidents;
                            }
                            else
                            {
                                accidents.Add(l, r.Accidents);
                            }

                            if (carsEntered.ContainsKey(l))
                            {
                                carsEntered[l] += r.CarsEntered;
                            }
                            else
                            {
                                carsEntered.Add(l, r.CarsEntered);
                            }
                        }
                    });
                });
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
            cartesianChart1.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Accidents",
                    Values = accidents.Values.AsChartValues(),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                },
                new StackedColumnSeries
                {
                    Title = "Cars Entered",
                    Values = carsEntered.Values.AsChartValues(),
                    StackMode = StackMode.Values,
                    DataLabels = true,
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Number of Roads",
                Labels = lbls.AsChartValues(),
                Separator = DefaultAxes.CleanSeparator,
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Usage",
            });

        }

        public void SimulationsCountCarsLeft()
        {
            double carsLeftPerc = Math.Round((data.GetCarsLeft() / data.GetCarsEntered()) * 100);
            LiveCharts.WinForms.SolidGauge solidGauge = new LiveCharts.WinForms.SolidGauge
            {
                From = 0,
                To = 100,
                Value = carsLeftPerc
            };
            solidGauge.LabelFormatter = val => val + " %";
            solidGauge.Base.LabelsVisibility = System.Windows.Visibility.Hidden;
            solidGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new System.Windows.Media.GradientStopCollection
                {
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Black, 0),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.DarkGreen, .5),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.LightGreen, 1)
                }
            };
            GenerateChartLabel("Total Cars Left", new Point(100, 340));
            AddChartToForm(solidGauge, new Point(100, 380));
        }
        public void SimulationsCountAccidents()
        {
            double accidentsPerc = Math.Round((data.GetSimulationsWithAccidentsCount() / data.GetSimulationsCount()) * 100);

            LiveCharts.WinForms.SolidGauge solidGauge = new LiveCharts.WinForms.SolidGauge
            {
                From = 0,
                To = 100,
                Value = accidentsPerc
            };
            solidGauge.LabelFormatter = val => val + " %";
            solidGauge.Base.LabelsVisibility = System.Windows.Visibility.Hidden;
            solidGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new System.Windows.Media.GradientStopCollection
                {
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Yellow, 0),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Orange, .5),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Red, 1)
                }
            };
            GenerateChartLabel("Total Simulations With Accidents", new Point(550, 340));
            AddChartToForm(solidGauge, new Point(550, 380));
        }

        public void HeatMapPerYearPerMonth(string year)
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            var labels = new string[] { "Roads", "Accidents", "Cars Entered", "Cars Left", "Pedestrians Entered", "Pedestrians Left",
                "Dijkstra", "A* Search" };
            ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();
            months.Keys.ToList().ForEach(month =>
            {

                int NumberOfRoads = 0;
                int Accidents = 0;
                int CarsEntered = 0;
                int CarsLeft = 0;
                int PedestriansEntered = 0;
                int PedestriansLeft = 0;
                int Dijkstra = 0;
                int AStar = 0;
                data.Reports[year].ForEach(v =>
                {
                    if (v.Creation.Month == int.Parse(month))
                    {
                        NumberOfRoads += v.NumberOfRoads;
                        Accidents += v.Accidents;
                        CarsEntered += v.CarsEntered;
                        CarsLeft += v.CarsLeft;
                        PedestriansEntered += v.PedestriansEntered;
                        PedestriansLeft += v.PedestriansLeft;
                        if (v.SPA == "Dijkstra")
                        {
                            Dijkstra += 1;
                        }
                        else
                        {
                            AStar += 1;
                        }
                    }
                });
                values.Add(new HeatPoint(int.Parse(month) - 1, 0, NumberOfRoads));
                values.Add(new HeatPoint(int.Parse(month) - 1, 1, Accidents));
                values.Add(new HeatPoint(int.Parse(month) - 1, 2, CarsEntered));
                values.Add(new HeatPoint(int.Parse(month) - 1, 3, CarsLeft));
                values.Add(new HeatPoint(int.Parse(month) - 1, 4, PedestriansEntered));
                values.Add(new HeatPoint(int.Parse(month) - 1, 5, PedestriansLeft));
                values.Add(new HeatPoint(int.Parse(month) - 1, 6, Dijkstra));
                values.Add(new HeatPoint(int.Parse(month) - 1, 7, AStar));
            });
            cartesianChart1.Series.Add(new HeatSeries
            {
                Values = values,
                DataLabels = true,

                //The GradientStopCollection is optional
                //If you do not set this property, LiveCharts will set a gradient
                GradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(136, 197, 252), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(90, 173, 250), 0.10),
                    new GradientStop(System.Windows.Media.Color.FromRgb(54, 156, 247), .5),
                    new GradientStop(System.Windows.Media.Color.FromRgb(6, 111, 204), .75),
                    new GradientStop(System.Windows.Media.Color.FromRgb(4, 79, 145), 1)
                }
            });
            cartesianChart1.AxisX.Add(new Axis
            {
                LabelsRotation = -15,
                Labels = months.Values.AsChartValues(),
                Separator = new Separator { Step = 1 }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Labels = labels.AsChartValues()
            });
            GenerateChartLabel("Distrubutions Per Month / Per Year", new Point(960, 340));
            AddHeatMapToForm(cartesianChart1, new Point(900, 420));

            ComboBox cbYears = new ComboBox();
            cbYears.Items.AddRange(data.Reports.Keys.ToArray());
            cbYears.Text = cbYears.Items[0].ToString();
            cbYears.FlatStyle = FlatStyle.Flat;
            cbYears.Location = new Point(1350, 360);
            graphsPanel.Controls.Add(cbYears);
            graphsPanel.SendToBack();
            cbYears.BringToFront();
        }

        public void TotalDrivingTimePerAlgorithm()
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            cartesianChart1.Series = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Time",
                    Values = new ChartValues<double> { data.GetAlgorithmsTiming()[0].TotalSeconds, data.GetAlgorithmsTiming()[1].TotalSeconds }
                }
            };

            cartesianChart1.AxisY.Add(new Axis
            {
                Labels = new[] { "Dijkstra ", "A* Search" }
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                LabelFormatter = value => value + " Seconds"
            });

            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };

            cartesianChart1.DataTooltip = tooltip;
            GenerateChartLabel("Total Driving Time Per Algorithm", new Point(100, 620));
            AddChartToForm(cartesianChart1, new Point(100, 660));
        }
        public void PlanedTimeAndActualTime()
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            cartesianChart1.Series = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Time",
                    Values = new ChartValues<double> { data.GetPlannedTiming().TotalSeconds, data.GetActualTiming().TotalSeconds }
                }
            };

            cartesianChart1.AxisY.Add(new Axis
            {
                Labels = new[] { "Planned Time ", "Actual Time" }
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                LabelFormatter = value => value + " Seconds"
            });

            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };

            cartesianChart1.DataTooltip = tooltip;
            GenerateChartLabel("Total Planned Time/ Actual Time", new Point(550, 620));
            AddChartToForm(cartesianChart1, new Point(550, 660));
        }
        public void CompareNumberOfRoads(List<Report> reports)
        {
            GenerateChartLabel("Number of Roads Comparison", new Point(80, 10));

            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            cartesianChart1.Series = new SeriesCollection();

            //adding series will update and animate the chart automatically
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Values = reports.Select(r => r.NumberOfRoads).AsChartValues()
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Labels = reports.Select(r => r.Name).AsChartValues(),
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Number of Roads",
            });

            AddChartToForm(cartesianChart1, new Point(100, 66));
        }
        public void CompareSimulationTimings(List<Report> reports)
        {
            GenerateChartLabel("Simulation Timings Comparison", new Point(530, 10));

            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            AddChartToForm(cartesianChart1, new Point(550, 66));
            cartesianChart1.Series = new SeriesCollection();
            cartesianChart1.Series.Add(
            new StackedColumnSeries
            {
                Title = "Actual Time",
                Values = reports.Select(r => r.ActualTIme.TotalSeconds).AsChartValues(),
                StackMode = StackMode.Values,
                DataLabels = true
            });
            cartesianChart1.Series.Add(
            new StackedColumnSeries
            {
                Title = "Planned Time",
                Values = reports.Select(r => r.PlannedTime.TotalSeconds).AsChartValues(),
                StackMode = StackMode.Values,
                DataLabels = true
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Report Name",
                Labels = reports.Select(r => r.Name).AsChartValues(),
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Time",
            });
        }
        public void CompareCarsEnteredCarsLeft(List<Report> reports)
        {
            GenerateChartLabel("Cars Entered/Cars Left Comparison", new Point(980, 10));

            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            cartesianChart1.Series = new SeriesCollection();

            //adding series will update and animate the chart automatically
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Cars Entered",
                Values = reports.Select(r => r.CarsEntered).AsChartValues()
            });
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Cars Left",
                Values = reports.Select(r => r.CarsLeft).AsChartValues()
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Report Name",
                Labels = reports.Select(r => r.Name).AsChartValues(),
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Number of Roads",
            });

            AddChartToForm(cartesianChart1, new Point(1000, 66));
        }

        public void HeatMapComparison(List<Report> reports)
        {
            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            var labels = new string[] { "Roads", "Accidents", "Cars Entered", "Cars Left", "Pedestrians Entered", "Pedestrians Left",
                "Dijkstra", "A* Search" };
            ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();
            for (int i = 0; i < reports.Count; i++)
            {
                int dijkstra = 0;
                int aStar = 0;
                if (reports[i].SPA.Equals("Dijkstra"))
                {
                    dijkstra++;
                }
                else
                {
                    aStar++;
                }
                values.Add(new HeatPoint(i, 0, reports[i].NumberOfRoads));
                values.Add(new HeatPoint(i, 1, reports[i].Accidents));
                values.Add(new HeatPoint(i, 2, reports[i].CarsEntered));
                values.Add(new HeatPoint(i, 3, reports[i].CarsLeft));
                values.Add(new HeatPoint(i, 4, reports[i].PedestriansEntered));
                values.Add(new HeatPoint(i, 5, reports[i].PedestriansLeft));
                values.Add(new HeatPoint(i, 6, dijkstra));
                values.Add(new HeatPoint(i, 7, aStar));
            }
            cartesianChart1.Series.Add(new HeatSeries
            {
                Values = values,
                DataLabels = true,
                GradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(237, 192, 102), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(245, 191, 83), 0.10),
                    new GradientStop(System.Windows.Media.Color.FromRgb(237, 167, 26), .5),
                    new GradientStop(System.Windows.Media.Color.FromRgb(191, 132, 11), .75),
                    new GradientStop(System.Windows.Media.Color.FromRgb(143, 99, 9), 1)
                }
            });
            cartesianChart1.AxisX.Add(new Axis
            {
                LabelsRotation = -15,
                Labels = reports.Select(r => r.Name).AsChartValues(),
                Separator = new Separator { Step = 1 },
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Labels = labels.AsChartValues()
            });
            GenerateChartLabel("Distrubutions Per Report", new Point(960, 340));
            AddHeatMapToForm(cartesianChart1, new Point(900, 420));

        }
        public void AccidentsComparison(List<Report> reports)
        {
            GenerateChartLabel("Accidents Comparison", new Point(80, 320));

            LiveCharts.WinForms.PieChart pieChart1 = new LiveCharts.WinForms.PieChart();
            pieChart1.InnerRadius = 20;
            pieChart1.LegendLocation = LegendLocation.Bottom;
            AddChartToForm(pieChart1, new Point(80, 360));

            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.OnlySender
            };
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            pieChart1.DataTooltip = tooltip;

            pieChart1.Series = new SeriesCollection();
            reports.ForEach(r =>
            {
                pieChart1.Series.Add(new PieSeries
                {
                    Title = r.Name,
                    Values = new ChartValues<double> { r.Accidents },
                    LabelPoint = labelPoint,
                    DataLabels = true
                });
            });
        }
        public void ComparePedestrians(List<Report> reports)
        {
            GenerateChartLabel("Pedestrians Comparison", new Point(510, 620));

            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            AddChartToForm(cartesianChart1, new Point(500, 660));
            cartesianChart1.Series = new SeriesCollection();
            cartesianChart1.Series.Add(
            new StackedRowSeries
            {
                Title = "Pedestrians Left",
                Values = reports.Select(r => r.PedestriansLeft).AsChartValues(),
                StackMode = StackMode.Values,
                DataLabels = true
            });
            cartesianChart1.Series.Add(
            new StackedRowSeries
            {
                Title = "Pedestrians Entered",
                Values = reports.Select(r => r.PedestriansEntered).AsChartValues(),
                StackMode = StackMode.Values,
                DataLabels = true
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Report Name",
                Labels = reports.Select(r => r.Name).AsChartValues(),
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Count",
            });
        }

        public void CompareAlgorithmsTiming(List<Report> reports)
        {
            GenerateChartLabel("Algorithms Timing Comparison", new Point(100, 620));

            LiveCharts.WinForms.CartesianChart cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            cartesianChart1.Series = new SeriesCollection();

            cartesianChart1.LegendLocation = LegendLocation.Bottom;

            List<double> dijkstraTimings = new List<double>(reports.Count);
            List<double> aStarTimings = new List<double>(reports.Count);
            reports.ForEach(r =>
            {
                if (r.SPA.Equals("Dijkstra"))
                {
                    dijkstraTimings.Add(r.EADrivingTime.TotalSeconds);
                    aStarTimings.Add(0);
                }
                else
                {
                    aStarTimings.Add(r.EADrivingTime.TotalSeconds);
                    dijkstraTimings.Add(0);
                }
            });
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Dijkstra",
                Values = dijkstraTimings.AsChartValues(),
            });
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "A* Search",
                Values = aStarTimings.AsChartValues()
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Report Name",
                Labels = reports.Select(r => r.Name).AsChartValues(),
                LabelsRotation = 20,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Emergency Car Driving Time",
                LabelFormatter = val => val + " Seconds"
            });

            AddChartToForm(cartesianChart1, new Point(100, 660));
        }

        public void CompareTotalAccidents(List<Report> reports)
        {
            double reportsWithAccidents = reports.FindAll(r => r.Accidents > 0).Count;


            double accidentsPerc = Math.Round((reportsWithAccidents / reports.Count) * 100);

            LiveCharts.WinForms.SolidGauge solidGauge = new LiveCharts.WinForms.SolidGauge
            {
                From = 0,
                To = 100,
                Value = accidentsPerc
            };
            solidGauge.LabelFormatter = val => val + " %";
            solidGauge.Base.LabelsVisibility = System.Windows.Visibility.Hidden;
            solidGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new System.Windows.Media.GradientStopCollection
                {
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Yellow, 0),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Orange, .5),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Red, 1)
                }
            };
            GenerateChartLabel("Total Simulations With Accidents", new Point(550, 320));
            AddChartToForm(solidGauge, new Point(550, 360));
        }
    }
}
