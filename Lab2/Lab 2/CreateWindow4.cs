using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab2
{
    class CreateWindow4
    {
        private static CommonMethodsForCreatingWindows commonMethods = new CommonMethodsForCreatingWindows();
        private static Window mainWindow;
        private static Grid myGrid = new Grid();

        // Elements of pannel:
        private static Window win4 = new Window();

        public CreateWindow4(Window myMainWindow)
        {
            mainWindow = myMainWindow;
            win4.Name = "Window3";
            win4.Title = "Window3";
            win4.ResizeMode = ResizeMode.NoResize;
            win4.Height = 450; win4.Width = 800;
            win4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            commonMethods.SetColor(myGrid, "#FFE0DEE7");

            Label myLabel = new Label();
            myLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            myLabel.VerticalContentAlignment = VerticalAlignment.Center;
            myLabel.Content = "Розробник:\nЗащик Іван Олександрович\nКП-11\nДата створення - 16.03.2022";
            myLabel.FontStyle = FontStyles.Italic;
            myLabel.FontSize = 24;
            commonMethods.SetMargin(myLabel, 0, 0, 0, 50);

            Button myButton = new Button();
            myButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            myButton.VerticalContentAlignment = VerticalAlignment.Center;
            myButton.HorizontalAlignment = HorizontalAlignment.Center;
            myButton.VerticalAlignment = VerticalAlignment.Bottom;
            commonMethods.SetMargin(myButton, 0, 0, 0, 25);
            myButton.Content = "Exit";
            myButton.FontSize = 24;
            myButton.Height = 50;
            myButton.Width = 250;
            myButton.Click += Method_ToMainWindow;

            myGrid.Children.Add(myLabel);
            myGrid.Children.Add(myButton);

            win4.Content = myGrid;
            win4.Show();
        }
        private void Method_ToMainWindow(object sender, RoutedEventArgs e)
        {
            win4.Hide();
            mainWindow.Show();
        }
        public void Show()
        {
            win4.Show();
        }
    }
}
