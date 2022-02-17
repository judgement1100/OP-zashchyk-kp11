using Lab2;
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

namespace Pract2
{
    /// <summary>
    /// Interaction logic for WindowForNotifications.xaml
    /// </summary>
    public partial class WindowForNotifications : Window
    {
        public WindowForNotifications()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
