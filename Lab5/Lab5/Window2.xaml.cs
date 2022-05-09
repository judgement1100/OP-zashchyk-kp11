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
    public partial class Window2 : Window
    {
        string connectionString = MainWindow.connectionString;
        SqlConnection connection = MainWindow.connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        static DataTable table = new DataTable();

        static int id = 1;
        static string departmentName = "defaultDepartmentNname";

        public Window2()
        {
            InitializeComponent();
            GetDepartmentsData();
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

        void GetDepartmentsData()
        {
            string query = "select dbo.Departments.id as [№], dbo.Departments.department_name as [Назва відділення]" +
                "from dbo.Departments";
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
            string query = "update dbo.Departments " +
                $"set department_name = '{departmentName}' " +
                $"where id = {id}";
            try
            {
                GetAndShowData(query);
                GetDepartmentsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            GetDepartmentsData();
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox1.Text.Length > 0)
            {
                try
                {
                    id = int.Parse(TextBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox2.Text.Length < 200 && TextBox2.Text.Length > 0)
            {
                try
                {
                    departmentName = TextBox2.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            command = new SqlCommand($"select * from dbo.Departments where id = {table.Rows.Count}", connection);
            var dr = command.ExecuteReader();
            dr.Read();
            id = dr.GetInt32(0);
            dr.Close();
            connection.Close();
            string query = $"insert into dbo.Departments values({id + 1}, '{departmentName}')";
            try
            {
                GetAndShowData(query);
                GetDepartmentsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetDepartmentsData();
            /*MessageBox.Show("1. You DON'T NEED to input ID if you are INSERTING line.\n" +
                "2. You NEED to input ID if you are UPDATING line.\n" +
                "3. You NEED to input ONLY ID if you are DELETING line.");*/
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string query = $"delete from dbo.Departments where id = {id}";
            try
            {
                GetAndShowData(query);
                GetDepartmentsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
