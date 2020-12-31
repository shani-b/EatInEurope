using EatInEurope.views;
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
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        StackPanel stackPanel;
        public List<Restaurant> RestaurantsResult
        {
            get { return (List<Restaurant>)GetValue(RestaurantsResultProperty); }
            set
            {
                SetValue(RestaurantsResultProperty, value);
            }
        }

        public static readonly DependencyProperty RestaurantsResultProperty =
            DependencyProperty.Register("RestaurantsResult", typeof(List<Restaurant>), typeof(Client));

        List<string> countriesFilters;

        public string CountryFilter
        {
            get { return (string)GetValue(CountryFilterProperty); }
            set
            {
                SetValue(CountryFilterProperty, value);
            }
        }

        public static readonly DependencyProperty CountryFilterProperty =
            DependencyProperty.Register("CountryFilter", typeof(string), typeof(Client));


        List<string> citiesFilters;
        public List<string> CitiesFilters
        {
            get { return (List<string>)GetValue(CitiesFiltersProperty); }
            set
            {
                SetValue(CitiesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty CitiesFiltersProperty =
            DependencyProperty.Register("CitiesFilters", typeof(List<string>), typeof(Client));
        List<string> stylesFilters;
        public List<string> StylesFilters
        {
            get { return (List<string>)GetValue(StylesFiltersProperty); }
            set
            {
                SetValue(StylesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty StylesFiltersProperty =
            DependencyProperty.Register("StylesFilters", typeof(List<string>), typeof(Client));

        public Client()
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelSearch(model);
            InitializeComponent();
            search.Visibility = Visibility.Collapsed;
            choises.Visibility = Visibility.Collapsed;
            searchResults.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            noRest.Visibility = Visibility.Collapsed;


            stackPanel = (StackPanel)FindName("choises");
            countriesFilters = new List<string>();
            citiesFilters = new List<string>();
            stylesFilters = new List<string>();

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestaurantsResultProperty, bindingMyRests);

            var VMCountriesFilters = "VM_CountryFilter";
            var bindingCountries = new Binding(VMCountriesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CountryFilterProperty, bindingCountries);

            var VMCitiesFilters = "VM_CitiesFilter";
            var bindingCities = new Binding(VMCitiesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CitiesFiltersProperty, bindingCities);

            var VMTypesFilters = "VM_TypesFilter";
            var bindingTypes = new Binding(VMTypesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(StylesFiltersProperty, bindingTypes);
        }

        private void Search_Trip_Click(object sender, RoutedEventArgs e)
        {
            TripSearch ts = new TripSearch();
            ts.Show();
            this.Close();
        }


        private void Search_Restaurant_Click(object sender, RoutedEventArgs e)
        {
            search.Visibility = Visibility.Visible;
            choises.Visibility = Visibility.Visible;
            searchTrip.Visibility = Visibility.Collapsed;
            searchRest.Visibility = Visibility.Collapsed;
        }


        public void GenerateControls(string newVal, List<string> list)
        {
            var exist = list.Find(val => val.Equals(newVal));
            if (exist == null)
            {
                if(newVal == null)
                {
                    return;
                }

                list.Add(newVal);
                TextBlock filter = new TextBlock();
                filter.Name = newVal;
                filter.Text = newVal;
                stackPanel.Children.Add(filter);
                //stackPanel.RegisterName(filter.Name, filter);

            }
        }


        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (sender as ComboBox).SelectedItem as string;
            GenerateControls(country, countriesFilters);
            CountryFilter = country;

            countries.IsEnabled = false;
        }

        private void citiesChanged(object sender, SelectionChangedEventArgs e)
        {
            string city = (sender as ComboBox).SelectedItem as string;
            GenerateControls(city, citiesFilters);
        }

        private void typesChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = (sender as ComboBox).SelectedItem as string;
            GenerateControls(type, stylesFilters);

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CitiesFilters = citiesFilters;
            StylesFilters = stylesFilters;

            stackPanel = (StackPanel)FindName("restList");
            title.Visibility = Visibility.Visible;
            fillRestStackPanel();
        }


        private void fillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum = RestaurantsResult.Count;
            if (RestaurantsResult == null || restNum == 0)
            {
                noRest.Visibility = Visibility.Visible;
                searchResults.Visibility = Visibility.Collapsed;
               
            }
            else
            {
                searchResults.Visibility = Visibility.Visible;
                // There are exists restuarents.
                for (int i = 0; i < restNum; i++)
                {
                    // TODO : change restID to accept id.
                    //string restID = "";
                    string restID = RestaurantsResult[i].ID; // DELETE
                    
                    RestaurantView rest = new RestaurantView(this, restID);

                    rest.restName.Content = RestaurantsResult[i].Name;
                    rest.styleName.Content = RestaurantsResult[i].Types + " | ";
                    rest.locationValue.Content = RestaurantsResult[i].City + ", " + RestaurantsResult[i].Country;

                    // TODO: FUNC ???
                    if (RestaurantsResult[i].Rate == 5)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                        rest.star5.Fill = Brushes.Yellow;
                    }
                    else if (RestaurantsResult[i].Rate == 4)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                    }
                    else if (RestaurantsResult[i].Rate == 3)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                    }
                    else if (RestaurantsResult[i].Rate == 2)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                    }
                    else if (RestaurantsResult[i].Rate == 1)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                    }
                    else
                    {
                        // half- .5 -think what to do?????
                    }

                    stackPanel.Children.Add(rest);

                }

            }
        }



        // TODO : TEMP FUNC - DELETE
        public Restaurant getDetailsByID(string id)
        {
            for (int i = 0; i < RestaurantsResult.Count; i++)
            {
                if (RestaurantsResult[i].ID == id)
                {
                    return RestaurantsResult[i];
                }
            }
            return null;
        }

        private void deleteChoises_Click(object sender, RoutedEventArgs e)
        {
            stackPanel = (StackPanel)FindName("choises");
            stackPanel.Children.Clear();
            stackPanel.Children.Add(titleChoices);

            countries.SelectedItem = null;
            cities.SelectedItem = null;
            styles.SelectedItem = null;

            countries.IsEnabled = true;

            if (CitiesFilters != null)
            {
                CitiesFilters.Clear();
            }
            if (StylesFilters != null)
            {
                StylesFilters.Clear();
            }
            

        }
    }
}