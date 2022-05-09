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

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = null;
        public static SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public static Window mainWindow = null;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
                    GetDepartmentToDoctorData();
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
            string query = "select dbo.Cards.id as [№ візиту], dbo.Cards.surname as [Прізвище], dbo.Cards.name as[Ім'я], dbo.Visits.date as[Дата], dbo.Visits.complaints as[Скарги], dbo.Visits.diagnosis as [Діагноз], dbo.Departments.department_name as [Направлення], dbo.Visits.has_sickleave as [Чи виписаний лікарняний лист], dbo.Visits.sickleave_term_days as [На скільки днів виписаний лікарняний лист], dbo.Doctors.surname as [Лікар, який виписав лист] from dbo.Visits inner join dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join	dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join	dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id";
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
            string query = "select dbo.Departments.department_name as [Назва відділення],dbo.Doctors.surname as [Прізвище], dbo.Doctors.name as [Ім'я] from dbo.DepatrmentToDoctor inner join dbo.Doctors on dbo.Doctors.id = dbo.DepatrmentToDoctor.doctor_id inner join dbo.Departments on dbo.Departments.id = dbo.DepatrmentToDoctor.department_id";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Combobox1_DropDownClosed(object sender, EventArgs e)
        {
            ShowData();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ShowEditingWindow();
        }

        void ShowEditingWindow()
        {
            if (Combobox1 != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)Combobox1.SelectedItem;
                string selectedText = comboBoxItem.Content.ToString();
                if (selectedText == "Cards")
                {
                    Window1 win1 = new Window1();
                    Hide();
                    win1.Show();
                }
                else if (selectedText == "Departments")
                {
                    Window2 win2 = new Window2();
                    Hide();
                    win2.Show();
                }
                else if (selectedText == "Doctors")
                {
                    Window3 win3 = new Window3();
                    Hide();
                    win3.Show();
                }
                else if (selectedText == "Visits")
                {
                    Window4 win4 = new Window4();
                    Hide();
                    win4.Show();
                }
                else if (selectedText == "DepartmentToDoctor")
                {
                    Window5 win5 = new Window5();
                    Hide();
                    win5.Show();
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowData();
        }
    }
}
