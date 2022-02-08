using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonToW1_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            Hide();
            window1.Show();
        }

        private void ExitButtnon_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonToW2_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            Hide();
            window2.Show();
        }

        private void ButtonToW3_Click(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            Hide();
            window3.Show();
        }

        private void ButtonToW4_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            Hide();
            window4.Show();
        }
    }
}
