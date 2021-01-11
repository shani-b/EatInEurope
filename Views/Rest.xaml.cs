using EatInEurope.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Rest.xaml
    /// </summary>
    public partial class Rest : UserControl
    {

        string restID;
        RestaurantOwnerWindow restow;

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

        public string IDToRemove
        {
            get { return (string)GetValue(IDToRemoveProperty); }
            set
            {
                SetValue(IDToRemoveProperty, value);
            }
        }

        public static readonly DependencyProperty IDToRemoveProperty =
            DependencyProperty.Register("IDToRemove", typeof(string), typeof(RestaurantOwnerWindow));

        public Rest(RestaurantOwnerWindow rest, string idRest)
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantOwner(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestIDProperty, bindingRestID);

            var VMIDToRemove = "VM_IDToRemove";
            var bindingIDToRemove = new Binding(VMIDToRemove) { Mode = BindingMode.TwoWay };
            this.SetBinding(IDToRemoveProperty, bindingIDToRemove);

            InitializeComponent();

            // Initialize the fileds.
            restID = idRest;
            restow = rest;

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            // Show this 'restID' Rest Detailes view -
            // without add review & raiting, and with edit options.
            RestaurantDetails restD = new RestaurantDetails(restID);
            restD.Show();
            restow.Close();
        }

        private void DeleteRest_Click(object sender, RoutedEventArgs e)
        {
            // Delete from the DB this rest('restID').
            IDToRemove = restID;
            MyRestaurantsList.RemoveAt(MyRestaurantsList.FindIndex(x => x.ID == restID));

            // Delete from RestaurantOwnerWindow view this rest.
            restow.stp.Children.Clear();
            restow.FillRestStackPanel();
        }
    }
}
