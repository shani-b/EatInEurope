using EatInEurope.Views;
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

namespace EatInEurope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            InitializeComponent();
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelMain(model);

            var VM_IsClient = "VM_IsClient";
            var binding = new Binding(VM_IsClient) { Mode = BindingMode.TwoWay };
            this.SetBinding(IsClientProperty, binding);
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            IsClient = true;
            Client c = new Client();
            c.Show();
            this.Close();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            IsClient = false;
             Manager m = new Manager();
            m.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
