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
    public partial class Window4 : Window
    {
        string connectionString = MainWindow.connectionString;
        SqlConnection connection = MainWindow.connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        static DataTable table = new DataTable();

        static int id = 1;
        static int card_id = 1;
        static string date = "01.01.2000";
        static string complains = "головний біль";
        static string diagnosis = "мігрень";
        static int dep_id = 12;
        static string has_sickleave = "Так";
        static int sickleave_term = 3;
        static int doctor_id = 1;

        public Window4()
        {
            InitializeComponent();
            GetVisitsData();
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

        void GetVisitsData()
        {
            string query = "select dbo.Visits.id as [№]," +
                "       dbo.Visits.card_id as [№ картки]," +
                "	   dbo.Visits.date as [Дата відвідування]," +
                "	   dbo.Visits.complaints as [Скарги]," +
                "	   dbo.Visits.diagnosis as [Діагноз]," +
                "	   dbo.Visits.department_id as [Направлення(№ відділення)]," +
                "	   dbo.Visits.has_sickleave as [Має лікарняний лист ?]," +
                "	   dbo.Visits.sickleave_term_days as [На скільки виписаний лікарняний лист(дні)]," +
                "	   dbo.Visits.doctor_id as [№ лікаря, який виписав лікарняний лист]" +
                "from dbo.Visits";
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
            string query = $"update dbo.Visits " +
                           $"set card_id = {card_id}," +
                           $"    date = '{date}'," +
                           $"    complaints = '{complains}'," +
                           $"    diagnosis = '{diagnosis}'," +
                           $"    department_id = {dep_id}," +
                           $"    has_sickleave = '{has_sickleave}'," +
                           $"    sickleave_term_days = {sickleave_term}," +
                           $"    doctor_id = {doctor_id}" +
                           $"    where id = {id}";
            try
            {
                GetAndShowData(query);
                GetVisitsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            GetVisitsData();
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
            if (TextBox2.Text.Length > 0)
            {
                try
                {
                    card_id = int.Parse(TextBox2.Text);
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
                    date = TextBox3.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox4.Text.Length < 1000 && TextBox4.Text.Length > 0)
            {
                try
                {
                    complains = TextBox4.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox5.Text.Length < 100 && TextBox5.Text.Length > 0)
            {
                try
                {
                    diagnosis = TextBox5.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox6.Text.Length > 0)
            {
                try
                {
                    dep_id = int.Parse(TextBox6.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox7_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox7.Text.Length > 0 && TextBox7.Text.Length < 10)
            {
                try
                {
                    has_sickleave = TextBox7.Text;
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
                    sickleave_term = int.Parse(TextBox8.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TextBox9_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox9.Text.Length > 0)
            {
                try
                {
                    doctor_id = int.Parse(TextBox9.Text);
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
            command = new SqlCommand($"select * from dbo.Visits where id = {table.Rows.Count}", connection);
            var dr = command.ExecuteReader();
            dr.Read();
            id = dr.GetInt32(0);
            dr.Close();
            connection.Close();
            string query = $"insert into dbo.Visits values({id + 1}, '{card_id}', '{date}', '{complains}', '{diagnosis}', '{dep_id}', '{has_sickleave}', '{sickleave_term}', '{doctor_id}')";
            try
            {
                GetAndShowData(query);
                GetVisitsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetVisitsData();
            /*MessageBox.Show("1. You DON'T NEED to input ID if you are INSERTING line.\n" +
                "2. You NEED to input ID if you are UPDATING line.\n" +
                "3. You NEED to input ONLY ID if you are DELETING line.");*/
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string query = $"delete from dbo.Visits where id = {id}";
            try
            {
                GetAndShowData(query);
                GetVisitsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
