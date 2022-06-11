using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using Pract3.Windows;

namespace Pract3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Admin_Window2 admin_Window2 = new Admin_Window2(this);
            Hide();
            admin_Window2.Show();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            User_Window3 user_Window3 = new User_Window3(this);
            Hide();
            user_Window3.Show();
        }

        private void DeveloperInfoButton_Click(object sender, RoutedEventArgs e)
        {
            DeveloverInfo_Window4 develoverInfo_Window4 = new DeveloverInfo_Window4(this);
            Hide();
            develoverInfo_Window4.Show();
        }
    }
}
