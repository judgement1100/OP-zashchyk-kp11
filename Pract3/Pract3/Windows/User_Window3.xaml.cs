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
using System.Windows.Shapes;
using System.Data.SqlClient;
using static Pract3.MainWindow;

namespace Pract3.Windows
{
    /// <summary>
    /// Interaction logic for User_Window3.xaml
    /// </summary>
    public partial class User_Window3 : Window
    {
        MainWindow mainWindow = null;

        public User_Window3(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        private bool CheckLogPass(string login, string password)
        {
            bool res = false;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"select Password from dbo.MainTable where Login = '{login}'", con);
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            try
            {
                if (da.GetValue(0).ToString().Trim() == password)
                {
                    res = true;
                }
                else
                {
                    res = false;
                    MessageBox.Show("Неправильний пароль");
                }
                con.Close();
                return res;
            }
            catch
            {
                MessageBox.Show("Неправильний логін");
                return false;
            }
        }

        private void Button_Authorise_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text;
            string pass = passwordField.Text;
            if (CheckLogPass(login, pass))
            {
                changeNameField.IsEnabled = true;
                changeSurnameField.IsEnabled = true;
                changePasswordField.IsEnabled = true;
                changePassword2Field.IsEnabled = true;
                UpdateDataButton.IsEnabled = true;

                MessageBox.Show("Авторизація успішна");
            }
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

        private void UpdatePass()
        {
            string newPass = changePasswordField.Text;
            string newPass2 = changePassword2Field.Text;
            string login = loginField.Text;
            string name = changeNameField.Text;
            string surname = changeSurnameField.Text;
            string truePassword = "";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"select Password from dbo.MainTable where Login = '{login}'", con);
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            truePassword = da.GetValue(0).ToString().Trim();
            da.Close();
            con.Close();
            if (newPass == newPass2)
            {
                con.Open();
                cmd = new SqlCommand($"update dbo.MainTable set Password = '{newPass}', Name = '{name}', Surname = '{surname}' where Login = '{login}'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Зміни внесено успішно!");
            }
            else
            {
                MessageBox.Show("Неправильне повторення нового пароля. Спробуйте ще.");
            }
        }

        private void Button_UpdateData_Click(object sender, RoutedEventArgs e)
        {
            string name, surname, password, password2;
            name = surname = password = password2 = "";
            name = changeNameField.Text;
            surname = changeSurnameField.Text;
            password = changePasswordField.Text;
            password2 = changePassword2Field.Text;

            if (IsRestrictedLogin($"{loginField.Text}"))
            {
                if (!isCorrectPassword(password))
                {
                    changePasswordField.Text = "";
                    changePasswordField.Text = "";
                    changePassword2Field.Text = "";
                    password = password2 = "";
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

        private void InsertUser(string name, string surname, string login, string password)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into dbo.MainTable values('{name}', '{surname}', '{login}', '{password}', 'true', 'true')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void RegestrateButton_Click(object sender, RoutedEventArgs e)
        {
            string name, surname, login, password, password2;
            name = surname = login = password = password2 = "";
            name = regNameField.Text;
            surname = regSurnameField.Text;
            login = regLoginField.Text;
            password = regPassField.Text;
            password2 = regRepeatPassField.Text;
            try
            {
                if (password == password2)
                {
                    if (!isCorrectPassword(password))
                    {
                        regPassField.Text = "";
                        regRepeatPassField.Text = "";
                        password = password2 = "";
                        MessageBox.Show("Новий пароль не підходить під обмеження");
                    }
                    else
                    {
                        InsertUser(name, surname, login, password);
                        MessageBox.Show("Дані зареєстровано успішно!");
                    }
                }
                else
                {
                    regPassField.Text = "";
                    regRepeatPassField.Text = "";
                    MessageBox.Show("Введено різні паролі");
                }
            }
            catch
            {
                MessageBox.Show("Такий логін вже існує. Спробуйте інший!");
            }
        }
    }
}
