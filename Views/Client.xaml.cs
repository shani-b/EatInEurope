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
        List<List<string>> RestsResults; // DELETE - BINDING
        StackPanel stackPanel;
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
            RestsResults = new List<List<string>> // DELEETE - PROPRETY!!!
            {
                new List<string> {"Martine of Martine's Table","Amsterdam", "French", "Dutch", "European"
                    , "5", "$$ - $$$", "136", "Just like home", "A Warm Welcome to Wintry Amsterdam", "/Restaurant_Review-g188590-d11752080-Reviews-Martine_of_Martine_s_Table-Amsterdam_North_Holland_Province.html" },
                new List<string> {"De Silveren Spiegel" ,"Amsterdam","Dutch", "European", "Vegetarian Friendly",
                    "4.5","$$$$", "812", "Great food and staff", "just perfect","/Restaurant_Review-g188590-d693419-Reviews-De_Silveren_Spiegel-Amsterdam_North_Holland_Province.html" },
                 new List<string> {"La Rive" ,"Amsterdam","Mediterranean", "French", "International",
                    "4.5","$$$$", "567", "Satisfaction", "Delicious old school restaurant","/Restaurant_Review-g188590-d696959-Reviews-La_Rive-Amsterdam_North_Holland_Province.html"}
            };

            stackPanel = (StackPanel)FindName("choises");
            countriesFilters = new List<string>();
            citiesFilters = new List<string>();
            stylesFilters = new List<string>();

            var VMCountriesFilters = "VM_CountriesFilter";
            var bindingCountries = new Binding(VMCountriesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CountriesFiltersProperty, bindingCountries);

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

            CountriesFilters = countriesFilters;
            CitiesFilters = citiesFilters;
            StylesFilters = stylesFilters;

            stackPanel = (StackPanel)FindName("restList");
            title.Visibility = Visibility.Visible;
            fillRestStackPanel();
        }


        private void fillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum = RestsResults.Count;
            if (RestsResults == null || restNum == 0)
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
                    string restID = RestsResults[i][0]; // DELETE
                    
                    RestaurantView rest = new RestaurantView(this, restID);

                    rest.restName.Content = RestsResults[i][0];
                    rest.styleName.Content = RestsResults[i][3] + " | ";
                    rest.locationValue.Content = RestsResults[i][1] + ", " + RestsResults[i][2];

                    // TODO: FUNC ???
                    if (RestsResults[i][5] == "5")
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                        rest.star5.Fill = Brushes.Yellow;
                    }
                    else if (RestsResults[i][5] == "4")
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                    }
                    else if (RestsResults[i][5] == "3")
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                    }
                    else if (RestsResults[i][5] == "2")
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                    }
                    else if (RestsResults[i][5] == "1")
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
        public List<string> getDetailsByID(string id)
        {
            for (int i = 0; i < RestsResults.Count; i++)
            {
                if (RestsResults[i][0] == id)
                {
                    return RestsResults[i];
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

            CitiesFilters.Clear();
            CountriesFilters.Clear();
            StylesFilters.Clear();
        }
    }
}