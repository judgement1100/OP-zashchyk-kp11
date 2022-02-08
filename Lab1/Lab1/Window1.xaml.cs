using System;
using System.IO;
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

namespace Lab1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ButtonToMain1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }

        private void ButtonToClear_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriterToClear = new StreamWriter("info.txt", false);
            streamWriterToClear.Write("");
            streamWriterToClear.Close();
            showFileContent();
        }
        private void ButtonToRead_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("info.txt", true);
            string text = TextBox1.Text;
            writer.WriteLine(text);
            writer.Close();
            TextBox1.Text = "";
            showFileContent();
        }
        private void showFileContent()
        {
            StreamReader reader = new StreamReader("info.txt");
            string fileText = reader.ReadToEnd();
            reader.Close();
            TextBox2.Text = fileText;
        }

        private void ButtonToDeleteSudent_Click(object sender, RoutedEventArgs e)
        {
            string ID = TextBox3.Text;
            string resText = "";
            StreamReader read = new StreamReader("info.txt");
            while (!read.EndOfStream)
            {
                string line = read.ReadLine();
                string[] currentIDArr = line.Split(", ");
                string currentID = currentIDArr[0];
                if (currentID != ID)
                {
                    resText += $"{line}\n";
                }
            }
            read.Close();
            StreamWriter write = new StreamWriter("info.txt");
            write.Write(resText);
            write.Close();
            showFileContent();
            TextBox3.Text = "";
        }

        private void TextBox3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox3.Text = "";
        }
    }
}
