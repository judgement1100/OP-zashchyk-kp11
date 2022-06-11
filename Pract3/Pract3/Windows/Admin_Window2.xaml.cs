using System;
using System.Collections.Generic;
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
using static Pract3.MainWindow;

namespace Pract3.Windows
{
    /// <summary>
    /// Interaction logic for Admin_Window2.xaml
    /// </summary>
    public partial class Admin_Window2 : Window
    {
        MainWindow mainWindow = null;
        public static int mainEnteringNum = 3;
        public static int userIndex = 0;
        public static int minUserIndex = 0;
        public static int maxUserIndex = 0;

        public Admin_Window2(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            mainEnteringNum = 3;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.Show();
        }

        private void passwordField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string truePassword = GetTrueAdminPassword();
                if (passwordField.Text == truePassword)
                {
                    ShowMainTable();
                    UnlockElems();
                    UpdateCheckBox();
                    userIndex = 0;
                    minUserIndex = 0;
                    maxUserIndex = loginsComboBox.Items.Count - 1;
                    ShowEachUserData();
                    MessageBox.Show("Перевірку пройдено успішно!");
                }
                else
                {
                    if (mainEnteringNum > 1)
                    {
                        mainEnteringNum--;
                        passwordField.Text = "";
                        MessageBox.Show($"Неправильний пароль. Залишилось спроб: {mainEnteringNum}");
                    }
                    else
                    {
                        passwordField.IsEnabled = false;
                        MessageBox.Show("Поле для введення заблоковано. Перезайдіть у вікно для отримання нових спроб.");
                    }
                }
            }
        }

        private void UnlockElems()
        {
            currentPasswordField.IsEnabled = true;
            newPasswordField.IsEnabled = true;
            newPasswordField2.IsEnabled = true;
            updatePasswordButton.IsEnabled = true;
            prevUserButton.IsEnabled = true;
            nextUserButton.IsEnabled = true;
            addUserField.IsEnabled = true;
            setActivityButton.IsEnabled = true;
            setRestrictionButton.IsEnabled = true;
            CheckBox1.IsEnabled = true;
            CheckBox2.IsEnabled = true;
            addUserField.IsEnabled = true;
            addUserButton.IsEnabled = true;
        }

        private string GetTrueAdminPassword()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select dbo.MainTable.Password from dbo.MainTable where login = 'ADMIN'";
            SqlCommand cmd = new SqlCommand(query, con);
            string truePassword = "";
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            truePassword = da.GetValue(0).ToString().Trim();
            con.Close();
            return truePassword;
        }

        private void ShowMainTable()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select * from dbo.MainTable order by dbo.MainTable.Name desc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGrid1.ItemsSource = table.DefaultView;
            con.Close();
        }

        private bool IsRestrictedLogin(string login)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"select Restriction from dbo.MainTable where login = '{login}'", con);
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            bool isRestrincted = da.GetBoolean(0);
            da.Close();
            con.Close();
            return isRestrincted;
        }

        private void UpdatePass()
        {
            string curPass = currentPasswordField.Text;
            string newPass = newPasswordField.Text;
            string newPass2 = newPasswordField2.Text;
            if (curPass == GetTrueAdminPassword())
            {
                if (newPass == newPass2)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"update dbo.MainTable set Password = '{newPass}' where Login = 'ADMIN'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ShowMainTable();
                    MessageBox.Show("Поточний пароль змінено!");
                }
                else
                {
                    MessageBox.Show("Неправильне повторення нового пароля. Спробуйте ще.");
                }
            }
            else
            {
                MessageBox.Show("Неправильний поточний пароль. Спробуйте ще.");
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string curPass = currentPasswordField.Text;
            string newPass = newPasswordField.Text;
            string newPass2 = newPasswordField2.Text;
            if (IsRestrictedLogin("ADMIN"))
            {
                if (!isCorrectPassword(newPass))
                {
                    currentPasswordField.Text = "";
                    newPasswordField.Text = "";
                    newPasswordField2.Text = "";
                    curPass = newPass = newPass2 = "";
                    MessageBox.Show("Новий пароль не підходить під обмеження");
                }
                else
                {
                    UpdatePass();
                }
            }
            else
            {
                UpdatePass();
            }
        }

        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            string login = addUserField.Text;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into dbo.MainTable (Login, Status, Restriction) values ('{login}', 'true', 'true')", con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Введений логін вже існує. Спробуйте інший");
            }
            con.Close();
            ShowMainTable();
            UpdateCheckBox();
        }

        int GetNumOfLogins()
        {
            int numOfNames = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(login) from dbo.MainTable)", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfNames = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfNames;
        }

        string[] GetMainTableData()
        {
            int numOfNames = GetNumOfLogins() + 1;
            string[] line = new string[numOfNames];
            SqlConnection connection = new SqlConnection(connectionString);
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select login from dbo.MainTable order by dbo.MainTable.Name desc", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                line[i] = table.Rows[i].Field<string>(0).Trim();
            }
            connection.Close();
            return line;
        }

        private void UpdateCheckBox()
        {
            string[] mainTableData = GetMainTableData();
            for (int i = 0; i < mainTableData.Length; i++)
            {
                if (!loginsComboBox.Items.Contains(mainTableData[i]))
                {
                    loginsComboBox.Items.Add(mainTableData[i]);
                }
            }
        }

        private void Button_SetStatus_Click(object sender, RoutedEventArgs e)
        {
            string login = loginsComboBox.SelectedItem.ToString();
            if (login != "ADMIN")
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = null;
                if (CheckBox1.IsChecked == true)
                {
                    cmd = new SqlCommand($"update dbo.MainTable set Status = '{true}' where Login = '{login}'", con);
                }
                else
                {
                    cmd = new SqlCommand($"update dbo.MainTable set Status = '{false}' where Login = '{login}'", con);
                }
                cmd.ExecuteNonQuery();
                con.Close();
                ShowMainTable();
            }
            else
            {
                MessageBox.Show("Статус адміністратора неможливо змінити");
            }
        }

        private void Button_SetRestriction_Click(object sender, RoutedEventArgs e)
        {
            string login = loginsComboBox.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = null;
            if (CheckBox2.IsChecked == true)
            {
                cmd = new SqlCommand($"update dbo.MainTable set Restriction = '{true}' where Login = '{login}'", con);
            }
            else
            {
                cmd = new SqlCommand($"update dbo.MainTable set Restriction = '{false}' where Login = '{login}'", con);
            }
            cmd.ExecuteNonQuery();
            con.Close();
            ShowMainTable();
        }

        private void ShowEachUserData()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string login = loginsComboBox.Items[userIndex].ToString();
            SqlCommand cmd = new SqlCommand($"select Name, Surname, Status, Restriction from MainTable where Login = '{login}'", con);
            string name, surname, status, restriction;
            name = surname = status = restriction = null;
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                name = da.GetValue(0).ToString().Trim();
                surname = da.GetValue(1).ToString().Trim();
                status = da.GetValue(2).ToString().Trim();
                restriction = da.GetValue(3).ToString().Trim();
            }
            con.Close();
            nameField.Text = name;
            surnameField.Text = surname;
            statusField.Text = status;
            restrictionField.Text = restriction;
            loginField.Text = login;
        }

        private void Button_ShowPrev_Click(object sender, RoutedEventArgs e)
        {
            userIndex--;
            if (userIndex == -1)
            {
                userIndex = maxUserIndex - 1;
            }
            ShowEachUserData();
        }

        private void Button_ShowNext_Click(object sender, RoutedEventArgs e)
        {
            userIndex++;
            if (userIndex == maxUserIndex)
            {
                userIndex = minUserIndex;
            }
            ShowEachUserData();
        }

        private bool isCorrectPassword(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsLetter(password[i]) || char.IsNumber(password[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
