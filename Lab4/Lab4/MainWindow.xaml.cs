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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // MessageBox.Show(connectionString);
        }
        void ShowData()
        {
            if (Combobox1 != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)Combobox1.SelectedItem;
                string selectedText = comboBoxItem.Content.ToString();
                if (selectedText == "Cards")
                {
                    GetCardsData();
                }
                else if (selectedText == "Departments")
                {
                    GetDepartmentsData();
                }
                else if (selectedText == "Doctors")
                {
                    GetDoctorsData();
                }
                else if (selectedText == "Visits")
                {
                    GetVisitsData();
                }
                else if (selectedText == "DepartmentToDoctor")
                {
                    GetVisitsData();
                }
            }
        }

        void GetAndShowData(string query)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(query, connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGrid1.ItemsSource = table.DefaultView;
            connection.Close();
        }

        void GetCardsData()
        {
            string query = "select dbo.Cards.id as [№], dbo.Cards.surname as [Прізвище]," +
                "dbo.Cards.name as [Ім'я], dbo.Cards.secname as [Ім'я по батькові]," +
                "dbo.Cards.address as [Адреса], dbo.Cards.sex as [Стать]," +
                "dbo.Cards.age as [Вік], dbo.Cards.insurance_policy_number as [Номер страхового полісу]," +
                "dbo.Cards.date_of_card_creation as [Дата створення карти]" +
                "from dbo.Cards";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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

        void GetVisitsData()
        {
            string query = "select dbo.Cards.surname as [Прізвище], dbo.Cards.name as[Ім'я], dbo.Visits.date as[Дата], dbo.Visits.complaints as[Скарги], dbo.Visits.diagnosis as [Діагноз], dbo.Departments.department_name as [Направлення], dbo.Departments.id as [id направлення], dbo.Visits.has_sickleave as [Чи виписаний лікарняний лист], dbo.Visits.sickleave_term_days as [На скільки днів виписаний лікарняний лист], dbo.Doctors.surname as [Лікар, який виписав лист], dbo.Visits.doctor_id as [id лікаря, що виписав лист] from dbo.Visits inner join dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join	dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join	dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void GetDepartmentToDoctorData()
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ShowData();
        }
    }
}
