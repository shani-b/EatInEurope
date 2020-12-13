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

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        private Button btn_login;
        private Button btn_singUp;
        public Manager()
        {
            InitializeComponent();
            password.Visibility = Visibility.Collapsed;
            passwordValue.Visibility = Visibility.Collapsed;
            username.Visibility = Visibility.Collapsed;
            usernameValue.Visibility = Visibility.Collapsed;
            passwordConfirm.Visibility = Visibility.Collapsed;
            passwordConfirmValue.Visibility = Visibility.Collapsed;
            login.Visibility = Visibility.Collapsed;
            signUp.Visibility = Visibility.Collapsed;

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            String userName = usernameValue.Text;
            String password = passwordValue.Text;
            // TODO: Check if username correct & password
            if (userName == "1" || password == "1")
            {
                usernameValue.Text = "worng";
                usernameValue.BorderBrush = Brushes.Red;
                passwordValue.Text = "worng";
                passwordValue.BorderBrush = Brushes.Red;
            }

            RestaurantOwnerWindow rest = new RestaurantOwnerWindow(userName);
            rest.Show();
            this.Close();
        }

        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {

            String userName = usernameValue.Text;
            String password = passwordValue.Text;
            String passwordConfirm = passwordConfirmValue.Text;
            RestaurantOwnerWindow rest = new RestaurantOwnerWindow(userName);
            rest.Show();
            this.Close();

        }

        private void Login_Option_Click(object sender, RoutedEventArgs e)
        {
            btn_login = sender as Button;
            username.Visibility = Visibility.Visible;
            usernameValue.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            passwordValue.Visibility = Visibility.Visible;
            login.Visibility = Visibility.Visible;
            if (btn_singUp == null /*|| btn_singUp.Background == Brushes.LightGray*/)
            {
                btn_login.Background = Brushes.Orange;
            }

            if (passwordConfirm.Visibility == Visibility.Visible)
            {
                passwordConfirm.Visibility = Visibility.Collapsed;
                passwordConfirmValue.Visibility = Visibility.Collapsed;
                signUp.Visibility = Visibility.Collapsed;


                btn_singUp.Background = Brushes.LightGray;
                btn_login.Background = Brushes.Orange;
            }


        }

        private void Sign_up_Option_Click(object sender, RoutedEventArgs e)
        {
            btn_singUp = sender as Button;
            username.Visibility = Visibility.Visible;
            usernameValue.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            passwordValue.Visibility = Visibility.Visible;
            passwordConfirm.Visibility = Visibility.Visible;
            passwordConfirmValue.Visibility = Visibility.Visible;
            signUp.Visibility = Visibility.Visible;
            if(btn_login == null /*|| btn_login.Background == Brushes.LightGray*/)
            {
                btn_singUp.Background = Brushes.Orange;
            }
            if (login.Visibility == Visibility.Visible)
            {
                login.Visibility = Visibility.Collapsed;

                btn_login.Background = Brushes.LightGray;
                btn_singUp.Background = Brushes.Orange;
            }
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
