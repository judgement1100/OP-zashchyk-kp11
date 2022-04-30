using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Math;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polygon myPolygon = new Polygon();
        static int Radius = 30;
        static DispatcherTimer dT = new DispatcherTimer();
        static int PointCount = 10;
        static PointCollection collection_points = new PointCollection();
        static List<Ellipse> list_ellipses = new List<Ellipse>();
        static int maxIeration = 100;
        static int curIteration = 0;
        static int iterationStep = 10;
        static int NPopulation = 5;
        static int[][] waysArray = new int[2 * NPopulation][];
        static double[] distancesArray = new double[2 * NPopulation];
        static double mutationChance = 0.7;
        static bool needRestart = false;
        static Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();

            InitPoints();
            InitPolygon();
            InitWaysArr();
            
            PlotPoints();

            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void OneStep(object sender, EventArgs e)
        {
            for (int i = 0; i < iterationStep; i++)
            {
                MyCanvas.Children.Clear();
                CrossPopulations();
                curIteration++;
                if (curIteration >= maxIeration)
                {
                    dT.Stop();
                    TextBox2.IsEnabled = true;
                    TextBox3.IsEnabled = true;
                    TextBox4.IsEnabled = true;
                    TextBox5.IsEnabled = true;
                    TextBox6.IsEnabled = true;
                    needRestart = true;
                }
                PlotPoints();
                PlotWay(GetBestWay());
                TextBox1.Text = curIteration.ToString();
                TextBox7.Text = Round(GetDistance(GetBestWay()), 2).ToString();
            }
        }

        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();

            for (int i = 0; i < BestWayIndex.Length; i++)
            {
                Points.Add(collection_points[BestWayIndex[i]]);
            }

            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }

        private void CrossPopulations()
        {
            for (int i = 0; i < NPopulation; i++)
            {
                int population1Index = rnd.Next(NPopulation);
                int population2Index = rnd.Next(NPopulation);
                int crossPoint = rnd.Next(1, PointCount);

                int[] child = GetChild(crossPoint, waysArray[population1Index], waysArray[population2Index]);
                waysArray[i + NPopulation] = child;
                if (rnd.NextDouble() < mutationChance)
                {
                    MutateElem(ref waysArray[i + NPopulation]);
                }
            }
            FillDistancesArray();
            (waysArray, distancesArray) = SortDistancesWays(waysArray, distancesArray);
        }

        private int[] GetBestWay()
        {
            return waysArray[0];
        }

        private (int[][], double[]) SortDistancesWays(int[][] ways, double[] distances)
        {
            for (int i = 1; i < distances.Length; i++)
            {
                for (int j = 0; j < distances.Length - 1; j++)
                {
                    if (distances[j] > distances[j + 1])
                    {
                        double tmpDist = distances[j];
                        distances[j] = distances[j + 1];
                        distances[j + 1] = tmpDist;

                        int[] tmpWay = ways[j];
                        ways[j] = ways[j + 1];
                        ways[j + 1] = tmpWay;
                    }
                }
            }
            return (ways, distances);
        }

        private void MutateElem(ref int[] elem)
        {
            int i1 = rnd.Next(PointCount);
            int i2 = rnd.Next(PointCount);
            if (i1 < i2)
            {
                for (int i = 0; i <= (i2 - i1) / 2; i++)
                {
                    int tmp = elem[i1 + i];
                    elem[i1 + i] = elem[i2 - i];
                    elem[i2 - i] = tmp;
                }
            }
            else
                for (int i = 0; i <= (i1 - i2) / 2; i++)
                {
                    int tmp = elem[i2 + i];
                    elem[i2 + i] = elem[i1 - i];
                    elem[i1 - i] = tmp;
                }
        }

        private int[] GetChild(int crossPoint, int[] population1, int[] population2)
        {
            int[] part1Pop1 = new int[crossPoint];
            int[] part2Pop1 = new int[PointCount - crossPoint];
            Array.Copy(population1, part1Pop1, crossPoint);
            Array.Copy(population1, crossPoint, part2Pop1, 0, population1.Length - crossPoint);

            int[] part1Pop2 = new int[crossPoint];
            int[] part2Pop2 = new int[PointCount - crossPoint];
            Array.Copy(population2, part1Pop2, crossPoint);
            Array.Copy(population2, crossPoint, part2Pop2, 0, population1.Length - crossPoint);

            int[] temp1 = new int[population1.Length];
            Array.Copy(part1Pop1, temp1, crossPoint);
            Array.Copy(part2Pop2, 0, temp1, crossPoint, part2Pop1.Length);

            int[] temp2 = new int[population2.Length];
            Array.Copy(part1Pop2, temp2, crossPoint);
            Array.Copy(part2Pop1, 0, temp2, crossPoint, part2Pop1.Length);

            List<int> childList = new List<int>();
            List<int> oddValues = new List<int>();

            if (rnd.NextDouble() > 0.5)
            {
                for (int i = 0; i < PointCount; i++)
                {
                    if (!oddValues.Contains(temp1[i]))
                    {
                        childList.Add(temp1[i]);
                        oddValues.Add(temp1[i]);
                    }
                }
                for (int i = 0; i < PointCount; i++)
                {
                    if (!oddValues.Contains(temp2[i]))
                    {
                        childList.Add(temp2[i]);
                        oddValues.Add(temp2[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < PointCount; i++)
                {
                    if (!oddValues.Contains(temp2[i]))
                    {
                        childList.Add(temp2[i]);
                        oddValues.Add(temp2[i]);
                    }
                }
                for (int i = 0; i < PointCount; i++)
                {
                    if (!oddValues.Contains(temp1[i]))
                    {
                        childList.Add(temp1[i]);
                        oddValues.Add(temp1[i]);
                    }
                }
            }
            
            int[] child = childList.ToArray();
            return child;
        }

        private void FillDistancesArray()
        {
            for (int i = 0; i < 2 * NPopulation; i++)
            {
                double dist = GetDistance(waysArray[i]);
                distancesArray[i] = dist;
            }
        }

        private double GetDistance(int[] way)
        {
            double distance = 0;
            Point p1, p2;
            for (int i = 0; i < way.Length - 1; i++)
            {
                p1 = collection_points[way[i]];
                p2 = collection_points[way[i + 1]];
                distance += Sqrt(Pow(p1.X - p2.X, 2) + Pow(p1.Y - p2.Y, 2));
            }
            p1 = collection_points[way[way.Length - 1]];
            p2 = collection_points[way[0]];
            distance += Sqrt(Pow(p1.X - p2.X, 2) + Pow(p1.Y - p2.Y, 2));
            return distance;
        }

        private void InitWaysArr()
        {
            List<int> oddValues = new List<int>();
            for (int i = 0; i < NPopulation; i++)
            {
                waysArray[i] = new int[PointCount];
                for (int j = 0; j < PointCount; j++)
                {
                    int num = 0;
                    while (true)
                    {
                        num = rnd.Next(PointCount);
                        if (!oddValues.Contains(num))
                        {
                            break;
                        }
                    }
                    oddValues.Add(num);
                    waysArray[i][j] = num;
                }
                oddValues.Clear();
            }
        }

        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Red;
            myPolygon.StrokeThickness = 1;
        }

        private void InitPoints()
        {
            collection_points.Clear();
            list_ellipses.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(MainWin.Width * 0.60 - 3 * Radius));
                p.Y = rnd.Next(Radius, (int)(MainWin.Height * 0.98 - 3 * Radius));
                collection_points.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            { 
                Ellipse el = new Ellipse();

                el.StrokeThickness = 1;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightGreen;
                list_ellipses.Add(el); 
            }            
        }

        private void PlotPoints()
        {
            MyCanvas.Children.Clear();
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(list_ellipses[i], collection_points[i].X - Radius / 2);
                Canvas.SetTop(list_ellipses[i], collection_points[i].Y - Radius / 2);
                MyCanvas.Children.Add(list_ellipses[i]);
            }
        }

        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                if (curIteration == maxIeration)
                {
                    curIteration = 0;
                    needRestart = true;
                }
                TextBox2.IsEnabled = true;
                TextBox3.IsEnabled = true;
                TextBox4.IsEnabled = true;
                TextBox5.IsEnabled = true;
                TextBox6.IsEnabled = true;
                dT.Stop();
            }
            else
            {
                if (waysArray.Contains(null))
                {
                    InitWaysArr();
                }
                if (needRestart)
                {
                    curIteration = 0;
                    TextBox2.IsEnabled = false;
                    TextBox3.IsEnabled = false;
                    TextBox4.IsEnabled = false;
                    TextBox5.IsEnabled = false;
                    TextBox6.IsEnabled = false;
                    InitPoints();
                    InitPolygon();
                    PlotPoints();
                    InitWaysArr();
                }
                needRestart = false;
                dT.Start();
            }
        }

        private void TextBox2_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    PointCount = Convert.ToInt32(TextBox2.Text);
                    if (PointCount > 500)
                    {
                        PointCount = 500;
                    }
                    InitPoints();
                    InitPolygon();
                    PlotPoints();
                    InitWaysArr();
                }
                catch
                {

                }
            }
        }

        private void TextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    maxIeration = int.Parse(TextBox5.Text);
                    if (maxIeration > 10000)
                    {
                        maxIeration = 10000;
                    }
                }
                catch
                {

                }
            }
        }

        private void TextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    iterationStep = int.Parse(TextBox6.Text);
                    if (iterationStep > 1000)
                    {
                        iterationStep = 1000;
                    }
                }
                catch
                {

                }
            }
        }

        private void TextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    mutationChance = double.Parse(TextBox4.Text);
                    if (mutationChance > 1)
                    {
                        mutationChance = 1;
                    }
                }
                catch
                {

                }
            }
        }

        private void TextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    NPopulation = int.Parse(TextBox3.Text);
                    if (NPopulation > 100)
                    {
                        NPopulation = 100;
                    }
                    waysArray = new int[2 * NPopulation][];
                    distancesArray = new double[2 * NPopulation];
                }
                catch
                {

                }
            }
        }
    }
}