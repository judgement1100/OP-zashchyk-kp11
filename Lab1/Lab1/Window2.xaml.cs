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

namespace Lab1
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void ButtonToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Combobox1.SelectedValue = null;
            Combobox2.SelectedValue = null;
            Combobox3.SelectedValue = null;
            Combobox4.SelectedValue = null;
            Combobox5.SelectedValue = null;
            Combobox6.SelectedValue = null;
            Combobox7.SelectedValue = null;
            Combobox8.SelectedValue = null;
            Combobox9.SelectedValue = null;
        }
    }
}
