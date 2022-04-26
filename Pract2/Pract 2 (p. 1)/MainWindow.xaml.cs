using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Math;

namespace Lab_2_First_App
{
    public partial class MainWindow : Window
    {
        List<Polygon> PolygonList = new List<Polygon>();
        static int Radius = 30;
        static DispatcherTimer dT = new DispatcherTimer();
        static int PointCount = 5;
        static PointCollection collection_points = new PointCollection();
        static List<Ellipse> list_ellipses = new List<Ellipse>();
        static int[] oddPointsIndex = new int[PointCount];
        static int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            InitPoints();
            InitPolygons();
            PlotPoints();

            dT = new DispatcherTimer();

            int[] bestWayIndexes = GetBestWay();
            PutPointPairInPolygonList(bestWayIndexes);

            dT.Tick += new EventHandler(PlotCut);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void PlotCut(object sender, EventArgs e)
        {
            if (counter == PointCount)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;
                return;
            }
            MyCanvas.Children.Add(PolygonList[counter]);
            counter++;
        }

        private void PutPointPairInPolygonList(int[] bestWayIndexes)
        {
            PointCollection curPoints = new PointCollection();
            Point p1, p2;
            for (int i = 0; i < bestWayIndexes.Length; i++)
            {
                curPoints.Clear();
                if (i == bestWayIndexes.Length - 1)
                {
                    p1 = collection_points[bestWayIndexes[i]];
                    curPoints.Add(p1);
                    p2 = collection_points[bestWayIndexes[0]];
                    curPoints.Add(p2);
                    PointCollection copiedPoints = new PointCollection(curPoints);
                    PolygonList[i].Points = copiedPoints;
                }
                else
                {
                    p1 = collection_points[bestWayIndexes[i]];
                    curPoints.Add(p1);
                    p2 = collection_points[bestWayIndexes[i + 1]];
                    curPoints.Add(p2);
                    PointCollection copiedPoints = new PointCollection(curPoints);
                    PolygonList[i].Points = copiedPoints;
                }
            }
        }

        private void InitPolygons()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Polygon myPolygon = new Polygon();
                myPolygon.Stroke = System.Windows.Media.Brushes.Red;
                myPolygon.StrokeThickness = 1;
                PolygonList.Add(myPolygon);
            }
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            collection_points.Clear();
            list_ellipses.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
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
                dT.Stop();
                NumElemCB.IsEnabled = true;
            }
            else
            {
                ListBoxItem item = (ListBoxItem)VelCB.SelectedItem;
                dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
                NumElemCB.IsEnabled = false;
                dT.Start();
            }
        }

        private int[] GetBestWay()
        {
            int minIndex = 0;
            int[] way = new int[PointCount];
            for (int i = 0; i < PointCount; i++)
            {
                minIndex = GetMinIndex(collection_points, minIndex);
                if (i == PointCount - 1)
                {
                    break;
                }
                way[i + 1] = minIndex;
                oddPointsIndex[i] = minIndex;
            }
            return way;
        }

        private int GetMinIndex(PointCollection points, int index)  //  get index of the nearest available point
        {
            Point p1 = collection_points[index];
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;
            int x2, y2 = 0;
            double min = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < PointCount; i++)
            {
                if (!oddPointsIndex.Contains(i))
                {
                    Point p2 = collection_points[i];
                    x2 = (int)p2.X;
                    y2 = (int)p2.Y;
                    double len = Sqrt(Pow(x1 - x2, 2) + Pow(y1 - y2, 2));
                    if (len < min)
                    {
                        min = len;
                        minIndex = i;
                    }
                }
            }
            return minIndex;
        }

        private void NumElemCB_DropDownClosed(object sender, EventArgs e)
        {
            ListBoxItem item = (ListBoxItem)NumElemCB.SelectedItem;
            PointCount = Convert.ToInt32(item.Content);

            MyCanvas.Children.Clear();
            oddPointsIndex = new int[PointCount];
            counter = 0;
            Start();
        }

        private void VelCB_DropDownClosed(object sender, EventArgs e)
        {
            dT.Stop();
            ListBoxItem item = (ListBoxItem)VelCB.SelectedItem;
            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
            dT.Start();
        }
    }
}