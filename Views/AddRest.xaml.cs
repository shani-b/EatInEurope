using EatInEurope.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for AddRest.xaml
    /// </summary>
    public partial class AddRest : Window
    {
        private bool flagStyleSelected;
        private bool flagFromPriceSelected;
        private string owner;
        private string name;
        private string country;
        private string city;
        private List<string> styles;
        private string price;
        private string url;
        private List<Restaurant> myRest;

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
            DependencyProperty.Register("MyRestaurants", typeof(List<Restaurant>), typeof(AddRest));

        public string CountryFilter
        {
            get { return (string)GetValue(CountryFilterProperty); }
            set
            {
                SetValue(CountryFilterProperty, value);
            }
        }

        public static readonly DependencyProperty CountryFilterProperty =
            DependencyProperty.Register("CountryFilter", typeof(string), typeof(AddRest));


        public bool IsAddRest
        {
            get { return (bool)GetValue(IsAddRestProperty); }
            set
            {
                SetValue(IsAddRestProperty, value);
            }
        }

        public static readonly DependencyProperty IsAddRestProperty =
            DependencyProperty.Register("IsAddRest", typeof(bool), typeof(AddRest));


        public AddRest(string ownerName)
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelAddRest(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            var VMCountriesFilters = "VM_CountryFilter";
            var bindingCountries = new Binding(VMCountriesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CountryFilterProperty, bindingCountries);

            var VMIsAddRest = "VM_IsAddRest";
            var bindingIsAddRest = new Binding(VMIsAddRest) { Mode = BindingMode.TwoWay };
            this.SetBinding(IsAddRestProperty, bindingIsAddRest);

            InitializeComponent();
            // Hides these objects until the visibility changes.
            styleChoice.Visibility = Visibility.Collapsed;
            errorName.Visibility = Visibility.Collapsed;
            errorCountry.Visibility = Visibility.Collapsed;
            errorCity.Visibility = Visibility.Collapsed;
            errorText.Visibility = Visibility.Collapsed;
            cities.IsEnabled = false;

            // Initialize the fileds.
            owner = ownerName;
            styles = new List<string>();
            myRest = MyRestaurants;
            flagStyleSelected = false;
            flagFromPriceSelected = false;
            name = "";
            country = "";
            city = "";
        }

        private void CountryChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update country filed to be the selected country.
            country = (sender as ComboBox).SelectedItem as string;
            // Update visbility.
            countries.IsEnabled = false;
            cities.IsEnabled = true;
            errorCountry.Visibility = Visibility.Collapsed;
            CountryFilter = country;

        }

        private void CityChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update city filed to be the selected city.
            city = (sender as ComboBox).SelectedItem as string;
            
            // Update visbility.
            errorCity.Visibility = Visibility.Collapsed;
        }

        private void StylesChanged(object sender, SelectionChangedEventArgs e)
        {
            string newVal = (sender as ComboBox).SelectedItem as string;
            var exist = styles.Find(val => val.Equals(newVal));
            if (exist == null)
            {
                if (newVal == null)
                {
                    // Error handling.
                    return;
                }

                // Add the new selected style to the styles list filed.
                styles.Add(newVal);

                // Add the name of the chosen style to the view list. 
                if (!flagStyleSelected)
                {
                    styleChoice.Visibility = Visibility.Visible;
                    flagStyleSelected = true;
                }
                TextBlock filter = new TextBlock
                {
                    Text = newVal,
                    FontSize = 16
                };
                choises.Children.Add(filter);
            }
        }

        private void LowPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update price filed to be the selected price.
            ComboBoxItem priceItem = (ComboBoxItem)lowPriceVal.SelectedItem;
            if (priceItem == null)
            {
               lowPriceVal.IsEnabled = true;
               topPriceVal.IsEnabled = false;
            }
            else
            {
                price = priceItem.Content.ToString();

                // Enabled 'To' view.
                topPriceVal.IsEnabled = true;
                if (!flagFromPriceSelected)
                {
                    lowPriceVal.IsEnabled = false;
                    flagFromPriceSelected = true;
                }

                // Update combo item view for fit the low price.
                switch (priceItem.Name[0])
                {
                    case 'a':
                        top1.Visibility = Visibility.Collapsed;
                        break;
                    case 'b':
                        top1.Visibility = Visibility.Collapsed;
                        top2.Visibility = Visibility.Collapsed;
                        break;
                    case 'c':
                        top1.Visibility = Visibility.Collapsed;
                        top2.Visibility = Visibility.Collapsed;
                        top3.Visibility = Visibility.Collapsed;
                        break;
                    case 'd':
                        top1.Visibility = Visibility.Collapsed;
                        top2.Visibility = Visibility.Collapsed;
                        top3.Visibility = Visibility.Collapsed;
                        top4.Visibility = Visibility.Collapsed;
                        break;
                    case 'e':
                        topPriceVal.IsEnabled = false;
                        break;
                }
            }            
        }

        private void TopPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update price filed to be 'the prev price  - the selected price'.
            ComboBoxItem priceItem = (ComboBoxItem)topPriceVal.SelectedItem;
            if (priceItem == null)
            {
                topPriceVal.IsEnabled = false;
            }
            else
            {
                string topPrice = priceItem.Content.ToString();
                price += " - " + topPrice;
                topPriceVal.IsEnabled = false;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Clean choise.
            price = "";
            lowPriceVal.SelectedIndex = -1;
            topPriceVal.SelectedIndex = -1; 
            flagFromPriceSelected = false;
            top1.Visibility = Visibility.Visible;
            top2.Visibility = Visibility.Visible;
            top3.Visibility = Visibility.Visible;
            top4.Visibility = Visibility.Visible;
            top5.Visibility = Visibility.Visible;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            // Update name & url filed to be the inserted name & url.
            url = newurltAdd.Text;
            name = newrestName.Text;

            // Update visbility.
            if (!name.Equals(""))
            {
                errorName.Visibility = Visibility.Collapsed;
            }

            // Checks if all mandatory details have been entered.
            if (name.Equals("") || city.Equals("") || country.Equals(""))
            {
                string errorMessage = " You must enter ";
                if (name.Equals(""))
                {
                    errorName.Visibility = Visibility.Visible;
                    errorMessage += "name, ";
                }
                if (city.Equals(""))
                {
                    errorCity.Visibility = Visibility.Visible;
                    errorMessage += "city, ";
                }
                if (country.Equals(""))
                {
                    errorCountry.Visibility = Visibility.Visible;
                    errorMessage += "country, ";
                }
                errorText.Visibility = Visibility.Visible;
                errorText.Text = errorMessage;

            }
            else
            {
                // All mandatory details have been entered.
                // Create new Restaurant object and add it to the rest list.
                Restaurant newRest = new Restaurant("", name, country, city, styles, 0, price, 0, new List<UserReview>(), url, owner);
                myRest.Add(newRest);

                // Insert to DB the new rest.
                MyRestaurants = myRest;

                // Show the Restaurant Owner Window view. (with the new rest)
                RestaurantOwnerWindow row = new RestaurantOwnerWindow(owner);
                row.Show();
                this.Close();
            }
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Go Back - show the Restaurant Owner Window view. (without add new rest)
            IsAddRest = false;
            RestaurantOwnerWindow row = new RestaurantOwnerWindow(owner);
            row.Show();
            this.Close();
        }

    }
}
