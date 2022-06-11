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
using System.Windows.Shapes;

namespace Pract3.Windows
{
    /// <summary>
    /// Interaction logic for DeveloverInfo_Window4.xaml
    /// </summary>
    public partial class DeveloverInfo_Window4 : Window
    {
        MainWindow mainWindow = null;

        public DeveloverInfo_Window4(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }
    }
}
