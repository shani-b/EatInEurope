using EatInEurope.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for RestaurantOwnerWindow.xaml
    /// </summary>
    public partial class RestaurantOwnerWindow : Window
    {
        public StackPanel stp;
        private string ownerName;


        public List<Restaurant> MyRestaurants
        {
            get { return (List<Restaurant>)GetValue(MyRestaurantsProperty); }
            set
            {
                SetValue(MyRestaurantsProperty, value);
            }
        }

        public static readonly DependencyProperty MyRestaurantsProperty =
            DependencyProperty.Register("MyRestaurants", typeof(List<Restaurant>), typeof(RestaurantOwnerWindow));

        public string CurrentRestID
        {
            get { return (string)GetValue(CurrentRestIDProperty); }
            set
            {
                SetValue(CurrentRestIDProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentRestIDProperty =
            DependencyProperty.Register("CurrentRestID", typeof(string), typeof(RestaurantOwnerWindow));

        public string Order
        {
            get { return (string)GetValue(OrderProperty); }
            set
            {
                SetValue(OrderProperty, value);
            }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(string), typeof(RestaurantOwnerWindow));


        public bool Asc
        {
            get { return (bool)GetValue(AscProperty); }
            set
            {
                SetValue(AscProperty, value);
            }
        }

        public static readonly DependencyProperty AscProperty =
            DependencyProperty.Register("Asc", typeof(bool), typeof(RestaurantOwnerWindow));


        public RestaurantOwnerWindow(string username)
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantOwner(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(CurrentRestIDProperty, bindingRestID);

            var VMOrder = "VM_Order";
            var bindingOrder = new Binding(VMOrder) { Mode = BindingMode.TwoWay };
            this.SetBinding(OrderProperty, bindingOrder);

            var VMAsc = "VM_Asc";
            var bindingAsc = new Binding(VMAsc) { Mode = BindingMode.TwoWay };
            this.SetBinding(AscProperty, bindingAsc);

            InitializeComponent();
            ownerName = username;
            usernameValue.Content = "Hello " + ownerName;

            noRest.Visibility = Visibility.Collapsed;
            comboSort.IsEnabled = true;

            stp = (StackPanel)FindName("restList");

            fillRestStackPanel();


        }

        public void fillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum = MyRestaurants.Count;
            if (MyRestaurants == null || restNum == 0)
            {
                noRest.Visibility = Visibility.Visible;
                myRest.Visibility = Visibility.Collapsed;
                comboSort.IsEnabled = false;
                // Clean the combo if used.
                if(comboSort.SelectedItem != null)
                {
                    comboSort.SelectedItem = null;
                }
                // TODO: Change color.
                addRest.Background = Brushes.Yellow;
            }
            else
            {
                // There are exists restuarents.
                for (int i = 0; i < restNum; i++)
                {
                    string restID = MyRestaurants[i].ID;
                    CurrentRestID = MyRestaurants[i].ID;
                    // BINDING RAITING


                    Rest rest = new Rest(this, restID);

                    rest.restName.Content = MyRestaurants[i].Name;
                    if(MyRestaurants[i].Types.Count == 0)
                    {
                        rest.styleName.Content = "None";
                    } 
                    else
                    {
                        rest.styleName.Content = MyRestaurants[i].Types[0] + " |";
                    }

                    rest.locationValue.Content = MyRestaurants[i].City + ", " + MyRestaurants[i].Country;
                    
                    stp.Children.Add(rest);
                }
            }
        }

        private void sortChanged(object sender, SelectionChangedEventArgs e)
        {
            var filterChoice = (sender as ComboBox).SelectedItem;
            if(filterChoice == null)
            {
                return;
            }
            string filterChoiceValue = filterChoice.ToString();
            if (filterChoiceValue.Contains("A-Z"))
            {
                Order = "name";
                Asc = true;
            }
            else if (filterChoiceValue.Contains("Z-A"))
            {
                Order = "name";
                Asc = false;
            }
            else if (filterChoiceValue.Contains("Raiting low to height"))
            {
                Order = "raiting";
                Asc = true;
            }
            else
            {
                Order = "raiting";
                Asc = false;
            }


            // TODO: get correct filter by choice - clear restview and Re-insert 'fillRest..'  
            stp.Children.Clear();
            // correct filter 
            fillRestStackPanel();
        }

        private void addRest_Click(object sender, RoutedEventArgs e)
        {
            AddRest ar = new AddRest(ownerName);
            ar.Show();
            this.Close();
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            // Logout button - Go back to Manager window.
            Manager manager = new Manager();
            manager.Show();
            this.Close();
        }
    }
}
