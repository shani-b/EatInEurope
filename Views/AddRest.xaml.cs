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
    /// Interaction logic for AddRest.xaml
    /// </summary>
    public partial class AddRest : Window
    {
        private string owner;
        public AddRest(string ownerName)
        {
            InitializeComponent();
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelAddRest(model);
            owner = ownerName;
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            // TODO: fter quere INSERT. (we exept to see the new rest there)
            RestaurantOwnerWindow row = new RestaurantOwnerWindow(owner);
            row.Show();
            this.Close();
        }

        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void citiesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
