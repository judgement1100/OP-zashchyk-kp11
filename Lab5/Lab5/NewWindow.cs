using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab5
{
    class NewWindow
    {
        static Window win1 = new Window();
        static Window mainWindow = null;
        private static Grid myGrid = new Grid();

        public static void CreateNewWindow1(Window win)
        {
            mainWindow = win;

            // Win1 properties
            win1.Name = "Window1";
            win1.Title = "Window1";
            win1.ResizeMode = ResizeMode.NoResize;
            win1.Height = 450; win1.Width = 800;
            win1.HorizontalAlignment = HorizontalAlignment.Center; win1.VerticalAlignment = VerticalAlignment.Center;
            win1.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Button1 (to main window)
            Button button1 = new Button();
            button1.Content = "To main window";
            button1.Width = 170; button1.Height = 50;
            button1.Click += ToMainWindow;
            SetColor(button1, "#FFFF9494");
            button1.FontSize = 20;
            button1.HorizontalAlignment = HorizontalAlignment.Right;
            button1.VerticalAlignment = VerticalAlignment.Bottom;
            SetMargin(button1, bottom: 10, right: 10);

            myGrid.Children.Add(button1);

            win1.Content = myGrid;
            win1.Show();
        }

        public static void ShowWin1()
        {
            win1.Show();
        }

        private static void ToMainWindow(object sender, RoutedEventArgs e)
        {
            win1.Hide();
            mainWindow.Show();
        }

        private static void SetColor(Control o, string color)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(color);
            o.Background = brush;
        }

        private static void SetMargin(Control o, double left = 0, double top = 0, double right = 0, double bottom = 0)
        {
            Thickness margin = o.Margin;
            margin.Left = left;
            margin.Right = right;
            margin.Top = top;
            margin.Bottom = bottom;
            o.Margin = margin;
        }
    }
}
