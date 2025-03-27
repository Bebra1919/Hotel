using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hotel.Model;

namespace Hotel.View
{
    public partial class RegistrationWindow : Window
    {
        bool success = false;
        Database database = new Database();

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            success = RegistrationSuccess() && CheckData() && CheckDatabase();
            if (success)
            {
                AddUserToDatebase();

                MessageBox.Show("Регистрация успешна!");
                this.Close();
            }
        }

        private void AddUserToDatebase()
        {
            var surname = SurnameTextBox.Text;
            var name = NameTextBox.Text;
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Password;
            var role = RoleComboBox.SelectedIndex == 0 ? "Пользователь" : "Администратор";
            var date = DateTime.Now;

            string qwery = $"insert into users(surname, name, login, password, role , is_blocked, last_enter_date)" +
                $"values('{surname}', '{name}', '{login}', '{password}', '{role}', 'false', '{date}')";

            try
            {
                SqlCommand command = new SqlCommand(qwery, database.GetSqlConnection());
                database.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные успешно добавлены в БД!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool RegistrationSuccess()
        {
            var formFullness = SurnameTextBox.Text.Equals("") || 
                !NameTextBox.Text.Equals("") ||
                !LoginTextBox.Text.Equals("") ||
                !PasswordTextBox.Password.Equals("");
            if (formFullness) {
                return true;
            }
            else {
                MessageBox.Show("Не все поля заполненны!", "Предупреждение!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return false;
        }

        private bool CheckData()
        {
            var forbiddenSymbols = !Regex.IsMatch(SurnameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(NameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(LoginTextBox.Text, @"^[a-zA-Z\s]*$");
            if (forbiddenSymbols)
            {
                MessageBox.Show("Поля содержат запрещённые символы!", "Ошибка" , 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckDatabase()
        {
            var login = LoginTextBox.Text;
            string qwery = $"Select login From users Where login = '{login}'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            
            try
            {
                SqlCommand command = new SqlCommand(qwery, database.GetSqlConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
