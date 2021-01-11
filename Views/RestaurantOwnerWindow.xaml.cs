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

        private string ownerName;
        public StackPanel stp;

        // Properties.
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


        public bool OLoadMoreRests
        {
            get { return (bool)GetValue(OLoadMoreRestsProperty); }
            set
            {
                SetValue(OLoadMoreRestsProperty, value);
            }
        }
        public static readonly DependencyProperty OLoadMoreRestsProperty =
            DependencyProperty.Register("OLoadMoreRests", typeof(bool), typeof(RestaurantOwnerWindow));



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

        public bool OEndOfRests
        {
            get { return (bool)GetValue(OEndOfRestsProperty); }
            set
            {
                SetValue(OEndOfRestsProperty, value);
            }
        }
        public static readonly DependencyProperty OEndOfRestsProperty =
            DependencyProperty.Register("OEndOfRests", typeof(bool), typeof(RestaurantOwnerWindow));



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

            var VMLoadMoreRests = "VM_LoadMoreRests";
            var bindingLoadMoreRests = new Binding(VMLoadMoreRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(OLoadMoreRestsProperty, bindingLoadMoreRests);

            var VMEndOfRests = "VM_EndOfRests";
            var bindingEndOfRests = new Binding(VMEndOfRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(OEndOfRestsProperty, bindingEndOfRests);

            // Initialize the view.
            InitializeComponent();
            noRest.Visibility = Visibility.Collapsed;
            if (OEndOfRests == true)
            {
                moreRests.IsEnabled = false;
            }
            comboSort.IsEnabled = true;

            // Fill the list of rest view accordint to DB list of rest. 
            stp = (StackPanel)FindName("restList");
            FillRestStackPanel();

            // Initialize the fileds.
            ownerName = username;
            usernameValue.Content = "Hello " + ownerName;

        }

        public void FillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum;
            if (MyRestaurants == null || (restNum=MyRestaurants.Count) == 0)
            {
                // The owner does not own any restaurants.
                // Show view changes accordingly.
                noRest.Visibility = Visibility.Visible;
                myRest.Visibility = Visibility.Collapsed;
                moreRests.Visibility = Visibility.Collapsed;
                addRest.Background = Brushes.Yellow;
                comboSort.IsEnabled = false;
                // Clean the combo if used.
                comboSort.SelectedIndex = -1;
            }
            else
            {
                // There are exists restuarents.
                for (int i = 0; i < restNum; i++)
                {
                    // Fill id view.
                    string restID = MyRestaurants[i].ID;

                    // Binding for stars raiting view.
                    CurrentRestID = restID;

                    // Create new Rest view.
                    Rest rest = new Rest(this, restID);

                    // Fill name view.
                    if (MyRestaurants[i].Name == null || MyRestaurants[i].Name.Equals(""))
                    {
                        rest.restName.Content = "None";
                    }
                    else
                    {
                        rest.restName.Content = MyRestaurants[i].Name;
                    }

                    // Fill style view.
                    if (MyRestaurants[i].Types == null || MyRestaurants[i].Types.Count == 0)
                    {
                        rest.styleName.Content = "None |";
                    } 
                    else
                    {
                        rest.styleName.Content = MyRestaurants[i].Types[0] + " |";
                    }

                    // Fill location view.
                    if (MyRestaurants[i].City != null && !MyRestaurants[i].City.Equals("") 
                        && MyRestaurants[i].Country != null && !MyRestaurants[i].Country.Equals(""))
                    {
                        rest.locationValue.Content = MyRestaurants[i].City + ", " + MyRestaurants[i].Country;
                    }
                    else if ((MyRestaurants[i].City == null || MyRestaurants[i].City.Equals("")) 
                        && MyRestaurants[i].Country != null && !MyRestaurants[i].Country.Equals(""))
                    {
                        rest.locationValue.Content = "None, " + MyRestaurants[i].Country;
                    }
                    else if (MyRestaurants[i].City != null && !MyRestaurants[i].City.Equals("")
                        && (MyRestaurants[i].Country == null || !MyRestaurants[i].Country.Equals("")))
                    {
                        rest.locationValue.Content =  MyRestaurants[i].City + ", None";
                    }
                    else
                    {
                        rest.locationValue.Content = "None, None";
                    }

                    // Add rest detailes to Rest view.
                    stp.Children.Add(rest);
                }
            }
        }

        private void SortChanged(object sender, SelectionChangedEventArgs e)
        {
            var filterChoice = (sender as ComboBox).SelectedItem;
            if(filterChoice == null) // Error handling.
            {
                return;
            }
            string filterChoiceValue = filterChoice.ToString();
            
            // Update the choice in the DB.
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
            else if (filterChoiceValue.Contains("Rating low to height"))
            {
                Order = "rating";
                Asc = true;
            }
            else
            {
                Order = "rating";
                Asc = false;
            }

            // Clear the List of rest view.
            stp.Children.Clear();
            // Fill the List of rest view by the selected sort filter.
            FillRestStackPanel();
        }

        private void AddRest_Click(object sender, RoutedEventArgs e)
        {
            // Show add restaurant view.
            AddRest ar = new AddRest(ownerName);
            ar.Show();
            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Clean rest results in Model.
            MyRestaurants.Clear();
            
            // Logout button - Go back to Manager window.
            Manager manager = new Manager();
            manager.Show();
            this.Close();
        }

        private void moreRests_Click(object sender, RoutedEventArgs e)
        {
            //loading.Visibility = Visibility.Visible;
            OLoadMoreRests = true;
            // Clear the List of rest view.
            stp = (StackPanel)FindName("restList");
            stp.Children.Clear();

            // Fill the List of rest view by the selected sort filter.
            FillRestStackPanel();
            if (OEndOfRests == true)
            {
                moreRests.IsEnabled = false;
                //moreRests.Visibility = Visibility.Collapsed;
            }

        }
    }
}
