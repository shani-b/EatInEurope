using EatInEurope.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {

        private Button btn_login;
        private Button btn_singUp;
        private bool cleanUser;
        private bool cleanPassword;
        private bool cleanConfirm;
        private bool removeError1;
        private bool removeError2;
        private bool choiceOpition; // login = 1, sing up = 0

        // Properties.
        public bool LoginOK
        {
            get { return (bool)GetValue(LoginOKProperty); }
            set
            {
                SetValue(LoginOKProperty, value);
            }
        }

        public static readonly DependencyProperty LoginOKProperty =
            DependencyProperty.Register("LoginOK", typeof(bool), typeof(Manager));


        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set
            {
                SetValue(PasswordProperty, value);
            }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(Manager));


        public string NewPassword
        {
            get { return (string)GetValue(NewPasswordProperty); }
            set
            {
                SetValue(NewPasswordProperty, value);
            }
        }

        public static readonly DependencyProperty NewPasswordProperty =
            DependencyProperty.Register("NewPassword", typeof(string), typeof(Manager));


        public bool UsernameFree
        {
            get { return (bool)GetValue(UsernameFreeProperty); }
            set
            {
                SetValue(UsernameFreeProperty, value);
            }
        }

        public static readonly DependencyProperty UsernameFreeProperty =
            DependencyProperty.Register("UsernameFree", typeof(bool), typeof(Manager));


        public Manager()
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelManager(model);

            var VMLoginOK = "VM_LoginOK";
            var binding = new Binding(VMLoginOK) { Mode = BindingMode.TwoWay };
            this.SetBinding(LoginOKProperty, binding);

            var VMPassword = "VM_Password";
            var bindingPassword = new Binding(VMPassword) { Mode = BindingMode.TwoWay };
            this.SetBinding(PasswordProperty, bindingPassword);

            var VMNewPassword = "VM_NewPassword";
            var bindingNewPassword = new Binding(VMNewPassword) { Mode = BindingMode.TwoWay };
            this.SetBinding(NewPasswordProperty, bindingNewPassword);

            var VMUsernameFree = "VM_UsernameFree";
            var bindingUsernameFree = new Binding(VMUsernameFree) { Mode = BindingMode.TwoWay };
            this.SetBinding(UsernameFreeProperty, bindingUsernameFree);

            InitializeComponent();

            // Hides these objects until the visibility changes.
            password.Visibility = Visibility.Collapsed;
            passwordValue.Visibility = Visibility.Collapsed;
            username.Visibility = Visibility.Collapsed;
            usernameValue.Visibility = Visibility.Collapsed;
            passwordConfirm.Visibility = Visibility.Collapsed;
            passwordConfirmValue.Visibility = Visibility.Collapsed;
            login.Visibility = Visibility.Collapsed;
            signUp.Visibility = Visibility.Collapsed;
            errorUsername.Visibility = Visibility.Collapsed;
            errorPassword.Visibility = Visibility.Collapsed;
            errorConfirm.Visibility = Visibility.Collapsed;
            errorText.Visibility = Visibility.Collapsed;

            // Initialize the fileds.
            cleanUser = false;
            cleanPassword = false;
            cleanConfirm = false;
            removeError1 = false;
            removeError2 = false;
        }

        private void Login_Option_Click(object sender, RoutedEventArgs e)
        {
            // Switch button to choose between login and sing up.
            // Login option.
            choiceOpition = true;

            btn_login = sender as Button;
            username.Visibility = Visibility.Visible;
            usernameValue.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            passwordValue.Visibility = Visibility.Visible;
            login.Visibility = Visibility.Visible;

            errorText.Visibility = Visibility.Collapsed;
            errorUsername.Visibility = Visibility.Collapsed;
            errorPassword.Visibility = Visibility.Collapsed;
            errorConfirm.Visibility = Visibility.Collapsed;

            cleanUser = false;
            cleanPassword = false;
            removeError1 = false;
            removeError2 = false;

            usernameValue.Text = "";
            passwordValue.Password = "";

            if (btn_singUp == null)
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
            // Switch button to choose between login and sing up.
            // Sing-up option.
            choiceOpition = false;

            btn_singUp = sender as Button;
            username.Visibility = Visibility.Visible;
            usernameValue.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            passwordValue.Visibility = Visibility.Visible;
            passwordConfirm.Visibility = Visibility.Visible;
            passwordConfirmValue.Visibility = Visibility.Visible;
            signUp.Visibility = Visibility.Visible;

            cleanUser = false;
            cleanPassword = false;
            cleanConfirm = false;
            removeError1 = false;
            removeError2 = false;

            errorText.Visibility = Visibility.Collapsed;
            errorUsername.Visibility = Visibility.Collapsed;
            errorPassword.Visibility = Visibility.Collapsed;
            errorConfirm.Visibility = Visibility.Collapsed;

            usernameValue.Text = "";
            passwordValue.Password = "";
            passwordConfirmValue.Password = "";

            if (btn_login == null)
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

        private void UsernameValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Checks choise in switch button.
            if (choiceOpition)
            {
                // Login option.
                if (cleanUser)
                {
                    usernameValue.Clear();
                    errorUsername.Visibility = Visibility.Collapsed;
                    usernameValue.Foreground = Brushes.Black;
                    cleanUser = false;
                    removeError1 = true;
                }

                if (cleanPassword)
                {
                    errorPassword.Visibility = Visibility.Collapsed;
                    cleanPassword = true;
                }

                usernameValue.Foreground = Brushes.Black;

                if (!usernameValue.Text.Equals(""))
                {
                    removeError1 = true;
                }

                if (removeError1 && removeError2)
                {
                    errorText.Visibility = Visibility.Collapsed;
                    removeError1 = false;
                }
            }
            else
            {
                // Sing up option.
                if (cleanUser)
                {
                    usernameValue.Clear();
                    errorUsername.Visibility = Visibility.Collapsed;
                    usernameValue.Foreground = Brushes.Black;
                    cleanUser = false;
                }

                if (usernameValue.Text.Equals(""))
                {
                    errorText.Visibility = Visibility.Collapsed;
                }

                if (cleanPassword)
                {
                    errorPassword.Visibility = Visibility.Collapsed;
                    errorConfirm.Visibility = Visibility.Collapsed;
                    cleanPassword = false;
                    cleanConfirm = false;
                }

            }
        }

        private void PasswordValue_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Checks choise in switch button.
            if (choiceOpition)
            {
                // Login option.
                if (cleanPassword)
                {
                    passwordValue.Clear();
                    errorPassword.Visibility = Visibility.Collapsed;
                    cleanPassword = false;
                    removeError2 = true;
                }

                if (cleanUser)
                {
                    errorUsername.Visibility = Visibility.Collapsed;
                    cleanUser = true;
                }

                if (!passwordValue.Password.Equals(""))
                {
                    removeError2 = true;
                }

                if (removeError1 && removeError2)
                {
                    errorText.Visibility = Visibility.Collapsed;
                    removeError2 = false;
                }
            }
            else
            {
                // Sing up option.
                if (cleanPassword)
                {
                    passwordValue.Clear();
                    errorPassword.Visibility = Visibility.Collapsed;
                    cleanPassword = false;
                }

                if (cleanConfirm)
                {
                    errorConfirm.Visibility = Visibility.Collapsed;
                    cleanConfirm = true;
                }

                if (cleanUser)
                {
                    errorUsername.Visibility = Visibility.Collapsed;
                    cleanUser = false;
                }

                if (passwordValue.Password.Equals(""))
                {
                    errorText.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordConfirmValue_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (cleanConfirm)
            {
                passwordConfirmValue.Clear();
                errorConfirm.Visibility = Visibility.Collapsed;
                cleanConfirm = false;
            }

            if (cleanPassword)
            {
                errorPassword.Visibility = Visibility.Collapsed;
                cleanPassword = true;
            }

            if (cleanUser)
            {
                errorUsername.Visibility = Visibility.Collapsed;
                cleanUser = false;
            }

            if (passwordConfirmValue.Password.Equals(""))
            {
                errorText.Visibility = Visibility.Collapsed;
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            usernameValue.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Password = passwordValue.Password;

            // Check if the username & password that entered are correct (found in our DB).
            if (!LoginOK)
            {
                // Displays an error indication to the user.
                errorText.Visibility = Visibility.Visible;
                usernameValue.Text = "wrong";
                usernameValue.Foreground = Brushes.LightGray;
                errorUsername.Visibility = Visibility.Visible;
                errorPassword.Visibility = Visibility.Visible;
                cleanUser = true;
                cleanPassword = true;
                removeError1 = false;
                removeError2 = false;
            }
            else
            {
                // User exists => login successful.
                // Show user's restaurant view.
                RestaurantOwnerWindow rest = new RestaurantOwnerWindow(usernameValue.Text);
                rest.Show();
                this.Close();
            }
        }

        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameValue.Text;
            string password = passwordValue.Password;
            string passwordConfirm = passwordConfirmValue.Password;

            // Checks if the password for the confirmation password is the same.
            if (!password.Equals(passwordConfirm))
            {
                // Displays an error indication to the user.
                errorText.Visibility = Visibility.Visible;
                errorText.Text = " The passwords are not the same.\n Try again!";
                errorPassword.Visibility = Visibility.Visible;
                errorConfirm.Visibility = Visibility.Visible;
                cleanUser = false;
                cleanConfirm = true;
                cleanPassword = true;
            }
            else
            {
                // The passwords are the same.

                // Checks if the username is available.
                if (!UsernameFree)
                {
                    // Displays an error indication to the user.
                    errorText.Visibility = Visibility.Visible;
                    errorText.Text = " Username is already taken.\n Try differnet username!";
                    usernameValue.Text = "wrong";
                    usernameValue.Foreground = Brushes.LightGray;
                    errorUsername.Visibility = Visibility.Visible;
                    cleanUser = true;
                    cleanPassword = false;
                    cleanConfirm = false;
                }
                else
                {
                    // Username free (not exsits in DB) => sing up successful.

                    // Checks if the input is incorrect.
                    if (password.Equals("") || username.Equals("wrong") || username.Equals(""))
                    {
                        // Displays an error indication to the user.
                        errorText.Visibility = Visibility.Visible;
                        errorText.Text = " The password or username are\n not valid. Try again!";
                        usernameValue.Text = "wrong";
                        usernameValue.Foreground = Brushes.LightGray;
                        errorUsername.Visibility = Visibility.Visible;
                        errorPassword.Visibility = Visibility.Visible;
                        errorConfirm.Visibility = Visibility.Visible;
                        cleanUser = true;
                        cleanPassword = true;
                        cleanConfirm = true;
                    }
                    else
                    {
                        // All input is correct for a new user.
                        // Insert the new restaurant owner to the DB.
                        usernameValue.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        NewPassword = passwordConfirm;

                        // Show user's restaurant view.
                        RestaurantOwnerWindow rest = new RestaurantOwnerWindow(username);
                        rest.Show();
                        this.Close();
                    }
                }
            }
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Back button - Main window.
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
