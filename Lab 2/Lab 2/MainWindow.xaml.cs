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

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreateWindow1 win1 = null;
        private CreateWindow2 win2 = null;
        private CreateWindow3 win3 = null;
        private CreateWindow4 win4 = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButtnon_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonToW1_Click_1(object sender, RoutedEventArgs e)
        {
            if (win1 is null)
            {
                win1 = new CreateWindow1(this);
            }
            else
            {
                win1.Show();
            }
            Hide();
        }

        private void ButtonToW2_Click(object sender, RoutedEventArgs e)
        {
            if (win2 is null)
            {
                win2 = new CreateWindow2(this);
            }
            else
            {
                win2.Show();
            }
            Hide();
        }
        private void ButtonToW3_Click(object sender, RoutedEventArgs e)
        {
            if (win3 is null)
            {
                win3 = new CreateWindow3(this);
            }
            else
            {
                win3.Show();
            }
            Hide();
        }

        private void ButtonToW4_Click(object sender, RoutedEventArgs e)
        {
            if (win4 is null)
            {
                win4 = new CreateWindow4(this);
            }
            else
            {
                win4.Show();
            }
            Hide();
        }
    }
}
