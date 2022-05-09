using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Shapes;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        string connectionString = MainWindow.connectionString;
        SqlConnection connection = MainWindow.connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        static DataTable table = new DataTable();

        static int old_department_id = 1;
        static int old_doctor_id = 1;
        static int new_department_id = 2;
        static int new_doctor_id = 2;

        public Window5()
        {
            InitializeComponent();
            GetDepartmentToDoctorData();
        }

        void GetAndShowData(string query)
        {
            table.Clear();
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DataGrid_Win1.ItemsSource = table.DefaultView;
            connection.Close();
        }

        void GetDepartmentToDoctorData()
        {
            string query = "select * from dbo.DepatrmentToDoctor";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow.mainWindow.Show();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = (TextBox)sender;
            obj.Text = "";
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string query = "update dbo.DepatrmentToDoctor " +
                $"set department_id = {new_department_id}, doctor_id = {new_doctor_id} " +
                $"where department_id = {old_department_id} and doctor_id = {old_doctor_id}";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            GetDepartmentToDoctorData();
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox1.Text.Length > 0)
            {
                try
                {
                    old_department_id = int.Parse(TextBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox2.Text.Length > 0)
            {
                try
                {
                    old_doctor_id = int.Parse(TextBox2.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox3.Text.Length > 0)
            {
                try
                {
                    new_department_id = int.Parse(TextBox3.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox4.Text.Length > 0)
            {
                try
                {
                    new_doctor_id = int.Parse(TextBox4.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string query = $"insert into dbo.DepatrmentToDoctor values({new_department_id}, {new_doctor_id})";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            GetDepartmentToDoctorData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetDepartmentToDoctorData();
            MessageBox.Show("1. You DON'T NEED to input ID if you are INSERTING line.\n" +
                "Also, if you want to insert values you must input NEW values \n" +
                "2. You NEED to input OLD and NEW IDs if you are UPDATING line.\n" +
                "3. You NEED to input ONLY OLD IDa if you are DELETING line.");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string query = $"delete from dbo.DepatrmentToDoctor " +
                $"where department_id = {old_department_id} and doctor_id = {old_doctor_id}";
            try
            {
                GetAndShowData(query);
                GetDepartmentToDoctorData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
