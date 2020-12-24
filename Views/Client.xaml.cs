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
        public List<string> CountriesFilters
        {
            get { return (List<string>)GetValue(CountriesFiltersProperty); }
            set
            {
                SetValue(CountriesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty CountriesFiltersProperty =
            DependencyProperty.Register("CountriesFilters", typeof(List<string>), typeof(Client));


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
        List<string> typesFilters;
        public List<string> TypesFilters
        {
            get { return (List<string>)GetValue(TypesFiltersProperty); }
            set
            {
                SetValue(TypesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty TypesFiltersProperty =
            DependencyProperty.Register("TypesFilters", typeof(List<string>), typeof(Client));

        public Client()
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelSearch(model);
            InitializeComponent();
            search.Visibility = Visibility.Collapsed;
            rest1.Visibility = Visibility.Collapsed;
            rest2.Visibility = Visibility.Collapsed;


            stackPanel = (StackPanel)FindName("choises");
            countriesFilters = new List<string>();
            citiesFilters = new List<string>();
            typesFilters = new List<string>();

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestaurantsResultProperty, bindingMyRests);

            var VMCountriesFilters = "VM_CountriesFilter";
            var bindingCountries = new Binding(VMCountriesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CountriesFiltersProperty, bindingCountries);

            var VMCitiesFilters = "VM_CitiesFilter";
            var bindingCities = new Binding(VMCitiesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CitiesFiltersProperty, bindingCities);

            var VMTypesFilters = "VM_TypesFilter";
            var bindingTypes = new Binding(VMTypesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(TypesFiltersProperty, bindingTypes);
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
            searchTrip.Visibility = Visibility.Collapsed;
            searchRest.Visibility = Visibility.Collapsed;
        }


        public void GenerateControls(string newVal, List<string> list)
        {
            var exist = list.Find(val => val.Equals(newVal));
            if (exist == null)
            {
                list.Add(newVal);
                TextBlock filter = new TextBlock();
                filter.Name = newVal;
                filter.Text = newVal;
                stackPanel.Children.Add(filter);
                stackPanel.RegisterName(filter.Name, filter);
            }
        }


        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (sender as ComboBox).SelectedItem as string;
            GenerateControls(country, countriesFilters);
        }

        private void citiesChanged(object sender, SelectionChangedEventArgs e)
        {
            string city = (sender as ComboBox).SelectedItem as string;
            GenerateControls(city, citiesFilters);
        }

        private void typesChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = (sender as ComboBox).SelectedItem as string;
            GenerateControls(type, typesFilters);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            /*RestaurantView rest1 = new RestaurantView();
            rest1.Visibility = Visibility.Visible;*/

            CountriesFilters = countriesFilters;
            CitiesFilters = citiesFilters;
            TypesFilters = typesFilters;


            rest1.Visibility = Visibility.Visible;
            rest2.Visibility = Visibility.Visible;
        }
    }
}