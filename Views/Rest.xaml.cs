using EatInEurope.ViewModels;
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

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Rest.xaml
    /// </summary>
    public partial class Rest : UserControl
    {
        string restID;
        RestaurantOwnerWindow restow;
        bool isClient;

        public List<Restaurant> MyRestaurantsList
        {
            get { return (List<Restaurant>)GetValue(MyRestaurantsProperty); }
            set
            {
                SetValue(MyRestaurantsProperty, value);
            }
        }

        public static readonly DependencyProperty MyRestaurantsProperty =
            DependencyProperty.Register("MyRestaurantsList", typeof(List<Restaurant>), typeof(RestaurantOwnerWindow));

        public string RestID
        {
            get { return (string)GetValue(RestIDProperty); }
            set
            {
                SetValue(RestIDProperty, value);
            }
        }

        public static readonly DependencyProperty RestIDProperty =
            DependencyProperty.Register("RestID", typeof(string), typeof(RestaurantOwnerWindow));

        public Rest(RestaurantOwnerWindow rest, string idRest)
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantOwner(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestIDProperty, bindingRestID);

            InitializeComponent();
            restID = idRest;
            restow = rest;
            isClient = false;
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            // detaile window without recomends and raiting 
            // with add and edit options.
            // TODO: THIS THE CORRECT LINE : string showThisRestDetailes = restID;
           // Restaurant showThisRestDetailes = restow.getDetailsByID(restID);
            RestaurantDetails restD = new RestaurantDetails(restID, isClient);
            restD.Show();
            restow.Close();
        }

        private void deleteRest_Click(object sender, RoutedEventArgs e)
        {
            //  Delete in DB this rest.
            RestID = restID;
            MyRestaurantsList.RemoveAt(MyRestaurantsList.FindIndex(x => x.ID == restID));

            // remove from RestaurantOwnerWindow
            restow.stp.Children.Clear();
            restow.fillRestStackPanel();
        }
    }
}
