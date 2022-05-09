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
    public partial class Window3 : Window
    {
        string connectionString = MainWindow.connectionString;
        SqlConnection connection = MainWindow.connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        static DataTable table = new DataTable();

        static int id = 1;
        static string surname = "defaultSurname";
        static string name = "defaultNname";
        static string secname = "defaultSecname";
        static string daysOfReception = "defDays";
        static string timeStart = "8:30";
        static string timeEnd = "17:00";
        static int cabinet = 404;

        public Window3()
        {
            InitializeComponent();
            GetDoctorsData();
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

        void GetDoctorsData()
        {
            string query = "select dbo.Doctors.id as [№], dbo.Doctors.surname as [Прізвище]," +
                "dbo.Doctors.name as [Ім'я], dbo.Doctors.secname as [Ім'я по батькові]," +
                "dbo.Doctors.days_of_reception as [Дні прийому], dbo.Doctors.time_start as [Початок прийому]," +
                "dbo.Doctors.time_end as [Кінець прийому], dbo.Doctors.cabinet as [Кабінет]" +
                "from dbo.Doctors";
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
            string query = "update dbo.Doctors " +
                $"set surname = '{surname}'," +
                $"    name = '{name}'," +
                $"    secname = '{secname}'," +
                $"    days_of_reception = '{daysOfReception}'," +
                $"    time_start = '{timeStart}'," +
                $"    time_end = '{timeEnd}'," +
                $"    cabinet = {cabinet} " +
                $"where id = {id}";
            try
            {
                GetAndShowData(query);
                GetDoctorsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            GetDoctorsData();
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
            if (TextBox2.Text.Length < 20 && TextBox2.Text.Length > 0)
            {
                try
                {
                    name = TextBox2.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox3.Text.Length < 20 && TextBox3.Text.Length > 0)
            {
                try
                {
                    surname = TextBox3.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox4.Text.Length < 20 && TextBox4.Text.Length > 0)
            {
                try
                {
                    secname = TextBox4.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox5.Text.Length < 20 && TextBox5.Text.Length > 0)
            {
                try
                {
                    daysOfReception = TextBox5.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox6.Text.Length < 6 && TextBox6.Text.Length > 0)
            {
                try
                {
                    timeStart = TextBox6.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox7_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox7.Text.Length < 6 && TextBox7.Text.Length > 0)
            {
                try
                {
                    timeEnd = TextBox7.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox8.Text.Length > 0)
            {
                try
                {
                    cabinet = int.Parse(TextBox8.Text);
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
            command = new SqlCommand($"select * from dbo.Doctors where id = {table.Rows.Count}", connection);
            var dr = command.ExecuteReader();
            dr.Read();
            id = dr.GetInt32(0);
            dr.Close();
            connection.Close();
            string query = $"insert into dbo.Doctors " +
                $"values({id + 1}, '{surname}', '{name}', '{secname}', '{daysOfReception}', '{timeStart}', '{timeEnd}', {cabinet})";
            try
            {
                GetAndShowData(query);
                GetDoctorsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            id = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetDoctorsData();
            /*MessageBox.Show("1. You DON'T NEED to input ID if you are INSERTING line.\n" +
                "2. You NEED to input ID if you are UPDATING line.\n" +
                "3. You NEED to input ONLY ID if you are DELETING line.");*/
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string query = $"delete from dbo.Doctors where id = {id}";
            try
            {
                GetAndShowData(query);
                GetDoctorsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
