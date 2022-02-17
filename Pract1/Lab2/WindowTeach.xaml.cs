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
    /// Interaction logic for WindowTeach.xaml
    /// </summary>
    public partial class WindowTeach : Window
    {
        static int numOfAtttempts = 0;
        
        static AlgorithmsClass algorithms = new AlgorithmsClass();
        public WindowTeach()
        {
            InitializeComponent();
            numOfAtttempts = GetNumberOfAttempts();
            TextBlock1.Text = numOfAtttempts.ToString();
            algorithms.WriteInFile("__Data 1__.txt", "", false);
        }
        private void TimeService()
        {
            int localCounter = 0;
            int[,] arr = new int[GetNumberOfAttempts(), TheWord.Text.Length - 1];
            StreamReader read = new StreamReader("__Data 1__.txt");
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
            bool hasValuableLine = algorithms.GetValuableArray(arr, "__Еталонні значення 1__.txt", "__Мат дисперсія та сподівання 1__.txt");
            if (!hasValuableLine)
            {
                WindowForNotifications windowForNotifications = new WindowForNotifications();
                windowForNotifications.Show();
            }
        }
        private int GetNumberOfAttempts()
        {
            int numOfAttempts = 1;
            if (Combobox1 != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)Combobox1.SelectedItem;
                string selectedText = comboBoxItem.Content.ToString();
                numOfAttempts = int.Parse(selectedText);
            }
            return numOfAttempts;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }

        private void InputTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InputTextBox.Text = "";
            counter = 0;
            algorithms.WriteInFile("__Data 1__.txt", "", false);
        }

        private void Combobox1_DropDownClosed(object sender, EventArgs e)
        {
            numOfAtttempts = GetNumberOfAttempts();
            TextBlock1.Text = numOfAtttempts.ToString();
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
            TextBlock1.Text = numOfAtttempts.ToString();
        }
        private void DeleteLineInFile()
        {
            StreamReader streamReader = new StreamReader("__Data 1__.txt");
            string[] splittedText = streamReader.ReadToEnd().Split("\n");
            string editedData = "";
            for (int i = 0; i < splittedText.Length - 1; i++)
            {
                editedData += splittedText[i];
                editedData += "\n";
            }
            streamReader.Close();
            algorithms.WriteInFile("__Data 1__.txt", editedData, false);
        }

        private void InputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputTextBox.Text.Length >= TheWord.Text.Length)
            {
                if (InputTextBox.Text == TheWord.Text)
                {
                    DecreaseAttempts();
                    algorithms.WriteInFile("__Data 1__.txt", "\n", true);
                    InputTextBox.Text = "";
                }
                else
                {
                    DeleteLineInFile();
                    InputTextBox.Text = "";
                }
                counter = 0;
            }
        }
        static int counter = 0;
        static Stopwatch stopwatch = new Stopwatch();
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
                algorithms.WriteInFile("__Data 1__.txt", time.Milliseconds.ToString() + "\t", true);
            }
            else
            {
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                algorithms.WriteInFile("__Data 1__.txt", time.Milliseconds.ToString() + "\t", true);
                stopwatch.Restart();
            }
            counter++;
        }
    }
}
