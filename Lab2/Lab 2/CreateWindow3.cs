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
    class CreateWindow3
    {
        private static CommonMethodsForCreatingWindows commonMethods = new CommonMethodsForCreatingWindows();
        private static Window mainWindow;
        private static Grid myGrid = new Grid();

        // Elements of pannel:
        private static Window win3 = new Window();
        private static TextBox MyTextBlock = new TextBox();

        public CreateWindow3(Window myMainWindow)
        {
            mainWindow = myMainWindow;
            win3.Name = "Window3";
            win3.Title = "Window3";
            win3.ResizeMode = ResizeMode.NoResize;
            win3.Height = 795; win3.Width = 565;
            win3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            commonMethods.SetColor(myGrid, "#FFE0DEE7");

            MyTextBlock.FontSize = 70;
            MyTextBlock.HorizontalContentAlignment = HorizontalAlignment.Right;
            MyTextBlock.VerticalContentAlignment = VerticalAlignment.Center;

            // Buttons: creating & settings
            Button[,] array_buttons = new Button[5, 4];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button currentButton = new Button();
                    currentButton.Click += Button_Click;
                    currentButton.FontSize = 46;
                    currentButton.Content = "Button";
                    currentButton.HorizontalContentAlignment = HorizontalAlignment.Center;
                    currentButton.VerticalContentAlignment = VerticalAlignment.Center;
                    array_buttons[i, j] = currentButton;
                    myGrid.Children.Add(array_buttons[i, j]);
                }
            }
            SetButtonsContent(array_buttons);
            commonMethods.SetColor(array_buttons[0, 1], "#FFF19518");

            // Rows & Cols creating:
            int int_numOfRows = 6;
            int int_numOfCols = 4;
            RowDefinition[] array_RowDefinitions = new RowDefinition[int_numOfRows];
            ColumnDefinition[] array_ColumnDefinitions = new ColumnDefinition[int_numOfCols];
            for (int i = 0; i < int_numOfRows; i++)
            {
                array_RowDefinitions[i] = new RowDefinition();
                myGrid.RowDefinitions.Add(array_RowDefinitions[i]);
            }
            for (int i = 0; i < int_numOfCols; i++)
            {
                array_ColumnDefinitions[i] = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(array_ColumnDefinitions[i]);
            }

            // Putting objects into Rows & Cols
            for (int i = 1; i < int_numOfRows; i++)
            {
                for (int j = 0; j < int_numOfCols; j++)
                {
                    Grid.SetRow(array_buttons[i - 1, j], i);
                    Grid.SetColumn(array_buttons[i - 1, j], j);
                }
            }
            Grid.SetRow(MyTextBlock, 0);
            Grid.SetColumnSpan(MyTextBlock, 4);
            myGrid.Children.Add(MyTextBlock);

            win3.Content = myGrid;
            win3.Show();
        }
        private void SetButtonsContent(Button[,] array_buttons)
        {
            array_buttons[0, 0].Content = "Exit";
            array_buttons[0, 1].Content = "=";
            array_buttons[0, 2].Content = "C";
            array_buttons[0, 3].Content = "b";
            array_buttons[1, 0].Content = "7";
            array_buttons[1, 1].Content = "8";
            array_buttons[1, 2].Content = "9";
            array_buttons[1, 3].Content = "/";
            array_buttons[2, 0].Content = "4";
            array_buttons[2, 1].Content = "5";
            array_buttons[2, 2].Content = "6";
            array_buttons[2, 3].Content = "*";
            array_buttons[3, 0].Content = "1";
            array_buttons[3, 1].Content = "2";
            array_buttons[3, 2].Content = "3";
            array_buttons[3, 3].Content = "-";
            array_buttons[4, 0].Content = "+/-";
            array_buttons[4, 1].Content = "0";
            array_buttons[4, 2].Content = ",";
            array_buttons[4, 3].Content = "+";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content;
                if (str == "Exit")
                {
                    MyTextBlock.Text = "";
                    win3.Hide();
                    mainWindow.Show();
                }
                else if (str == "C")
                {
                    MyTextBlock.Text = "";
                }
                else if (str == "=")
                {
                    DataTable dataTable = new DataTable();
                    string result = dataTable.Compute(MyTextBlock.Text, null).ToString();
                    MyTextBlock.Text = result;
                }
                else if (str == "+/-")
                {
                    MyTextBlock.Text += "*-1";
                }
                else if (str == "b")
                {
                    if (MyTextBlock.Text != "")
                    {
                        MyTextBlock.Text = MyTextBlock.Text.Remove(MyTextBlock.Text.Length - 1, 1);
                    }
                }
                else
                {
                    MyTextBlock.Text += str;
                }
            }
            catch (Exception)
            {
                MyTextBlock.Text = "Error. Try again!";
            }
        }
        private void Method_ToMainWindow(object sender, RoutedEventArgs e)
        {
            win3.Hide();
            mainWindow.Show();
        }
        public void Show()
        {
            win3.Show();
        }
    }
}
