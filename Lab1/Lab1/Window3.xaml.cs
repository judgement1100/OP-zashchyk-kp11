using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            foreach (UIElement item in MyGrid.Children)
            {
                if (item is Button)
                {
                    ((Button)item).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content;
                if (str == "Exit")
                {
                    MyTextBox.Text = "";
                    MainWindow mainWindow = new MainWindow();
                    Hide();
                    mainWindow.Show();
                }
                else if (str == "C")
                {
                    MyTextBox.Text = "";
                }
                else if (str == "=")
                {
                    DataTable dataTable = new DataTable();
                    string result = dataTable.Compute(MyTextBox.Text, null).ToString();
                    MyTextBox.Text = result;
                }
                else if (str == "+/-")
                {
                    MyTextBox.Text += "*-1";
                }
                else if (str == "b")
                {
                    if (MyTextBox.Text != "")
                    {
                        MyTextBox.Text = MyTextBox.Text.Remove(MyTextBox.Text.Length - 1, 1);
                    }
                }
                else
                {
                    MyTextBox.Text += str;
                }
            }
            catch (Exception)
            {
                MyTextBox.Text = "Error. Try again!";
            }
        }
    }
}
