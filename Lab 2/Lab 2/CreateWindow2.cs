using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab2
{
    class CreateWindow2
    {
        private static CommonMethodsForCreatingWindows commonMethods = new CommonMethodsForCreatingWindows();
        private static Window mainWindow;
        private static Grid myGrid = new Grid();

        // Elements of pannel:
        private static Window win2 = new Window();
        private static ComboBox[] array_ComboBoxes = new ComboBox[9];
        private static Button[] array_Buttons = new Button[3];
        private static int int_numOfRows = 3;
        private static int int_numOfCols = 4;
        private static RowDefinition[] array_RowDefinitions = new RowDefinition[int_numOfRows];
        private static ColumnDefinition[] array_ColumnDefinitions = new ColumnDefinition[int_numOfCols];

        public CreateWindow2(Window myMainWindow)
        {
            mainWindow = myMainWindow;
            win2.Name = "Window2";
            win2.Title = "Window2";
            win2.ResizeMode = ResizeMode.NoResize;
            win2.Height = 450; win2.Width = 800;
            win2.HorizontalAlignment = HorizontalAlignment.Center;
            win2.VerticalAlignment = VerticalAlignment.Center;
            win2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            commonMethods.SetColor(myGrid, "#FFE0DEE7");

            // Buttons settings:
            for (int i = 0; i < 3; i++)
            {
                array_Buttons[i] = new Button();
                array_Buttons[i].FontFamily = new FontFamily("Algerian");
                array_Buttons[i].FontSize = 24;
                myGrid.Children.Add(array_Buttons[i]);
            }
            array_Buttons[0].Content = "Exit";
            array_Buttons[0].Click += commonMethods.ExitButtnon_Click;
            commonMethods.SetColor(array_Buttons[0], "#FF13ACE5");

            array_Buttons[1].Content = "Restart";
            array_Buttons[1].Click += Method_ToRestart;
            commonMethods.SetColor(array_Buttons[1], "#FF1DDDB0");

            array_Buttons[2].Content = "To main\nwindow";
            array_Buttons[2].Click += Method_ToMainWindow;
            commonMethods.SetColor(array_Buttons[2], "#FFE5DF13");

            // Comboboxes creating:
            for (int i = 0; i < 9; i++)
            {
                array_ComboBoxes[i] = CreateCombobox();
                myGrid.Children.Add(array_ComboBoxes[i]);
            }

            // Rows & Cols creating:
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
            int int_counter = 0;
            for (int i = 0; i < int_numOfRows; i++)
            {
                for (int j = 0; j < int_numOfCols - 1; j++)
                {
                    Grid.SetRow(array_ComboBoxes[int_counter], i);
                    Grid.SetColumn(array_ComboBoxes[int_counter], j);
                    int_counter++;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Grid.SetRow(array_Buttons[i], i);
                Grid.SetColumn(array_Buttons[i], 4);
            }

            win2.Content = myGrid;
            win2.Show();
        }
        private ComboBox CreateCombobox()
        {
            ComboBox myComboBox = new ComboBox();
            myComboBox.FontSize = 40;
            myComboBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            myComboBox.VerticalContentAlignment = VerticalAlignment.Center;
            commonMethods.SetColor(myComboBox, "#FFF0F0F0");
            ComboBoxItem comboBoxItem1 = new ComboBoxItem();
            ComboBoxItem comboBoxItem2 = new ComboBoxItem();
            comboBoxItem1.FontSize = 20;
            comboBoxItem2.FontSize = 20;
            comboBoxItem1.Content = "X";
            comboBoxItem2.Content = "O";
            myComboBox.Items.Add(comboBoxItem1);
            myComboBox.Items.Add(comboBoxItem2);
            return myComboBox;
        }
        private void Method_ToRestart(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < array_ComboBoxes.Length; i++)
            {
                array_ComboBoxes[i].SelectedValue = null;
            }
        }
        private void Method_ToMainWindow(object sender, RoutedEventArgs e)
        {
            Method_ToRestart(sender, e);
            win2.Hide();
            mainWindow.Show();
        }
        public void Show()
        {
            win2.Show();
        }
    }
}
