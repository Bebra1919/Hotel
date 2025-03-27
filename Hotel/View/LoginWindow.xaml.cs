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
using Hotel.Model;

namespace Hotel.View
{
    public partial class LoginWindow : Window
    {
        private int failedAttempts = 0;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var user = new User("pupkin", "vasia", "123", "123", "Администратор");

            if (failedAttempts >= 3)
            {
                MessageBox.Show("Вы заблокированы. Обратитесь к администратору", "Доступ ограничен" ,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (LoginTextBox.Text.Equals("") || LoginPasswordBox.Password.Equals(""))
            {
                MessageBox.Show("Не все поля заполненны!", "Предупреждение!" ,
                    MessageBoxButton.OK, MessageBoxImage.Warning
                    );
            }
            else if (LoginTextBox.Text.Equals(user.GetLogin()) && LoginPasswordBox.Password.Equals(user.GetPassword()))
            {
                MessageBox.Show("Вы успешно авторизовались", "Успех" ,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                AdminWindow window = new AdminWindow();
                window.Show();
                this.Close();
            }
            else {
                failedAttempts++;
                MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные", "Предупреждение!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
