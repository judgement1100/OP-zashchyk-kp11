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
using System.Threading;
using System.IO;
using System.Diagnostics;
using Pract2;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for WindowCheck.xaml
    /// </summary>
    public partial class WindowCheck : Window
    {
        static int numOfAtttempts = 0;
        static int counter = 0;
        static Stopwatch stopwatch = new Stopwatch();
        static AlgorithmsClass algorithms = new AlgorithmsClass();
        public WindowCheck()
        {
            InitializeComponent();
            TextBlock1.Text = TheWord.Text.Length.ToString();
            numOfAtttempts = GetNumberOfAttempts();
            TextBlock1.Text = numOfAtttempts.ToString();
            TextBlock6.Text = numOfAtttempts.ToString();
        }

        private void InputTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputTextBox.Text = "";
            counter = 0;
            algorithms.WriteInFile("__Data 2__.txt", "", false);
        }

        private void Combobox1_DropDownClosed_1(object sender, EventArgs e)
        {
            numOfAtttempts = GetNumberOfAttempts();
            TextBlock6.Text = numOfAtttempts.ToString();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }

        private void TimeService()
        {
            int localCounter = 0;
            int[,] arr = new int[GetNumberOfAttempts(), TheWord.Text.Length - 1];
            StreamReader read = new StreamReader("__Data 2__.txt");
            while (!read.EndOfStream)
            {
                string line = read.ReadLine();
                string[] splittedLine = line.Split("\t");
                for (int j = 0; j < splittedLine.Length - 1; j++)
                {
                    arr[localCounter, j] = int.Parse(splittedLine[j]);
                }
                localCounter++;
            }
            read.Close();
            bool hasValuableLine = algorithms.GetValuableArray(arr, "__Еталонні значення 2__.txt", "__Мат дисперсія та сподівання 2__.txt");
            if (!hasValuableLine)
            {
                WindowForNotifications windowForNotifications = new WindowForNotifications();
                windowForNotifications.Show();
            }
            else
            {
                if (algorithms.AreSimilar())
                {
                    TextBlock2.Text = "однорідні";
                }
                else
                {
                    TextBlock2.Text = "неоднорідні";
                }
                double P = algorithms.CheckAuthentication();
                TextBlock3.Text = P.ToString();
                double P_1 = algorithms.Find_P1();
                TextBlock4.Text = P_1.ToString();
                double P_2 = algorithms.Find_P2();
                TextBlock5.Text = P_2.ToString();
            }
        }

        private int GetNumberOfAttempts()
        {
            int numOfAtttempts = 1;
            if (Combobox1 != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)Combobox1.SelectedItem;
                string selectedText = comboBoxItem.Content.ToString();
                numOfAtttempts = int.Parse(selectedText);
            }
            return numOfAtttempts;
        }

        private void DecreaseAttempts()
        {
            numOfAtttempts--;
            if (numOfAtttempts == 0)
            {
                InputTextBox.Background = Brushes.Red;
                InputTextBox.IsEnabled = false;
                TimeService();
            }
            TextBlock6.Text = numOfAtttempts.ToString();
        }

        private void DeleteLineInFile()
        {
            StreamReader streamReader = new StreamReader("__Data 2__.txt");
            string[] splittedText = streamReader.ReadToEnd().Split("\n");
            string editedData = "";
            for (int i = 0; i < splittedText.Length - 1; i++)
            {
                editedData += splittedText[i];
                editedData += "\n";
            }
            streamReader.Close();
            algorithms.WriteInFile("__Data 2__.txt", editedData, false);
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (counter == 0)
            {
                stopwatch.Start();
            }
            else if (counter == 12)
            {
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                algorithms.WriteInFile("__Data 2__.txt", time.Milliseconds.ToString() + "\t", true);
            }
            else
            {
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                algorithms.WriteInFile("__Data 2__.txt", time.Milliseconds.ToString() + "\t", true);
                stopwatch.Restart();
            }
            counter++;
        }

        private void InputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputTextBox.Text.Length >= TheWord.Text.Length)
            {
                if (InputTextBox.Text == TheWord.Text)
                {
                    DecreaseAttempts();
                    algorithms.WriteInFile("__Data 2__.txt", "\n", true);
                    InputTextBox.Text = "";
                }
                else
                {
                    DeleteLineInFile();
                    InputTextBox.Text = "";
                }
                counter = 0;
            }
            TextBlock1.Text = InputTextBox.Text.Length.ToString();
        }
        
    }
}
