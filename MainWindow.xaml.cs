using EatInEurope.ViewModels;
using EatInEurope.Views;
using System.Windows;
using System.Windows.Data;

namespace EatInEurope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties.
        public bool IsClient
        {
            get { return (bool)GetValue(IsClientProperty); }
            set
            {
                SetValue(IsClientProperty, value);
            }
        }

        public static readonly DependencyProperty IsClientProperty =
            DependencyProperty.Register("IsClient", typeof(bool), typeof(MainWindow));


        public MainWindow()
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelMain(model);

            var VM_IsClient = "VM_IsClient";
            var binding = new Binding(VM_IsClient) { Mode = BindingMode.TwoWay };
            this.SetBinding(IsClientProperty, binding);

            InitializeComponent();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            // Go to Client Options view.
            IsClient = true;
            ClientOptionsView client = new ClientOptionsView();
            client.Show();
            this.Close();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            // Go to Manager view.
            IsClient = false;
            Manager manager = new Manager();
            manager.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Exit from the project.
            this.Close();
        }
    }
}
