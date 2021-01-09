using EatInEurope.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Search : Window
    {
        StackPanel stackPanel;
        List<string> countryFilters;
        List<string> citiesFilters;
        List<string> stylesFilters;
        List<string> priceFilter;
        List<string> rateFilter;
        string fromP;
        string toP;
        double fromR;
        double toR;

        // Properties.
        public List<Restaurant> RestaurantsResult
        {
            get { return (List<Restaurant>)GetValue(RestaurantsResultProperty); }
            set
            {
                SetValue(RestaurantsResultProperty, value);
            }
        }

        public static readonly DependencyProperty RestaurantsResultProperty =
            DependencyProperty.Register("RestaurantsResult", typeof(List<Restaurant>), typeof(Search));

        public string NameFilter
        {
            get { return (string)GetValue(NameFilterProperty); }
            set
            {
                SetValue(NameFilterProperty, value);
            }
        }

        public static readonly DependencyProperty NameFilterProperty =
            DependencyProperty.Register("NameFilter", typeof(string), typeof(Search));

        public string CountryFilter
        {
            get { return (string)GetValue(CountryFilterProperty); }
            set
            {
                SetValue(CountryFilterProperty, value);
            }
        }

        public static readonly DependencyProperty CountryFilterProperty =
            DependencyProperty.Register("CountryFilter", typeof(string), typeof(Search));


        public List<string> CitiesFilters
        {
            get { return (List<string>)GetValue(CitiesFiltersProperty); }
            set
            {
                SetValue(CitiesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty CitiesFiltersProperty =
            DependencyProperty.Register("CitiesFilters", typeof(List<string>), typeof(Search));


        public List<string> StylesFilters
        {
            get { return (List<string>)GetValue(StylesFiltersProperty); }
            set
            {
                SetValue(StylesFiltersProperty, value);
            }
        }

        public static readonly DependencyProperty StylesFiltersProperty =
            DependencyProperty.Register("StylesFilters", typeof(List<string>), typeof(Search));


        public List<string> PriceFilter
        {
            get { return (List<string>)GetValue(PriceFilterProperty); }
            set
            {
                SetValue(PriceFilterProperty, value);
            }
        }

        public static readonly DependencyProperty PriceFilterProperty =
            DependencyProperty.Register("PriceFilter", typeof(List<string>), typeof(Search));


        public List<double> RateFilter
        {
            get { return (List<double>)GetValue(RateFilterProperty); }
            set
            {
                SetValue(RateFilterProperty, value);
            }
        }

        public static readonly DependencyProperty RateFilterProperty =
            DependencyProperty.Register("RateFilter", typeof(List<double>), typeof(Search));


        public string COrder
        {
            get { return (string)GetValue(COrderProperty); }
            set
            {
                SetValue(COrderProperty, value);
            }
        }

        public static readonly DependencyProperty COrderProperty =
            DependencyProperty.Register("COrder", typeof(string), typeof(RestaurantOwnerWindow));


        public bool CAsc
        {
            get { return (bool)GetValue(CAscProperty); }
            set
            {
                SetValue(CAscProperty, value);
            }
        }

        public static readonly DependencyProperty CAscProperty =
            DependencyProperty.Register("CAsc", typeof(bool), typeof(RestaurantOwnerWindow));


        public Search()
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelSearch(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestaurantsResultProperty, bindingMyRests);

            var VMNameFilter = "VM_RestName";
            var bindingNameFilter = new Binding(VMNameFilter) { Mode = BindingMode.TwoWay };
            this.SetBinding(NameFilterProperty, bindingNameFilter);

            var VMCountriesFilters = "VM_CountryFilter";
            var bindingCountries = new Binding(VMCountriesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CountryFilterProperty, bindingCountries);

            var VMCitiesFilters = "VM_CitiesFilter";
            var bindingCities = new Binding(VMCitiesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(CitiesFiltersProperty, bindingCities);

            var VMTypesFilters = "VM_TypesFilter";
            var bindingTypes = new Binding(VMTypesFilters) { Mode = BindingMode.TwoWay };
            this.SetBinding(StylesFiltersProperty, bindingTypes);

            var VMPriceFilter = "VM_PriceFilter";
            var bindingPriceFilter = new Binding(VMPriceFilter) { Mode = BindingMode.TwoWay };
            this.SetBinding(PriceFilterProperty, bindingPriceFilter);

            var VMRateFilter = "VM_RateFilter";
            var bindingRateFilter = new Binding(VMRateFilter) { Mode = BindingMode.TwoWay };
            this.SetBinding(RateFilterProperty, bindingRateFilter);

            var VMOrder = "VM_Order";
            var bindingOrder = new Binding(VMOrder) { Mode = BindingMode.TwoWay };
            this.SetBinding(COrderProperty, bindingOrder);

            var VMAsc = "VM_Asc";
            var bindingAsc = new Binding(VMAsc) { Mode = BindingMode.TwoWay };
            this.SetBinding(CAscProperty, bindingAsc);

            InitializeComponent();

            // Hides these objects until the visibility changes.
            choices.Visibility = Visibility.Collapsed;
            sortIcon.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            comboSort.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            searchResults.Visibility = Visibility.Collapsed;
            noRest.Visibility = Visibility.Collapsed;

            // Initialize the fileds.
            stackPanel = (StackPanel)FindName("choises");
            countryFilters = new List<string>();
            citiesFilters = new List<string>();
            stylesFilters = new List<string>();
            priceFilter = new List<string>();
            rateFilter = new List<string>();
            fromP = "";
            toP = "";
            fromR = 0;
            toR = 0;
        }

        public void GenerateControls(string newVal, List<string> list, int flag)
        {
            var exist = list.Find(val => val.Equals(newVal));
            if (exist == null)
            {
                if(newVal == null)
                {
                    // Error handling.
                    return;
                }
                list.Add(newVal);
                choices.Visibility = Visibility.Visible;

                // Fill choice view.
                TextBlock filter = new TextBlock();
                switch (flag)
                {
                    case 1:
                        stackPanel = (StackPanel)FindName("generiSTP");
                        filter.Name = newVal;
                        filter.Text = "Country:    " + newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 2:
                        stackPanel = (StackPanel)FindName("citiesSTP");
                        filter.Name = newVal;
                        filter.Text = newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 3:
                        stackPanel = (StackPanel)FindName("stylesSTP");
                        filter.Name = newVal;
                        filter.Text = newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 4:
                        stackPanel = (StackPanel)FindName("generiSTP");
                        filter.Text = "From Price:  " + newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 5:
                        stackPanel = (StackPanel)FindName("generiSTP");
                        filter.Text = "To Price:  " + newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 6:
                        stackPanel = (StackPanel)FindName("generiSTP");
                        filter.Text = "From Rate:  " + newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 7:
                        stackPanel = (StackPanel)FindName("generiSTP");
                        filter.Text = "To Rate:  " + newVal;
                        stackPanel.Children.Add(filter);
                        break;
                }
                

            }
        }

        private void CountryChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this country.
            string country = (sender as ComboBox).SelectedItem as string;
            GenerateControls(country, countryFilters, 1);
            // Update country filed.
            CountryFilter = country;

            countryCombo.IsEnabled = false;
        }

        private void CitiesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this city.
            string city = (sender as ComboBox).SelectedItem as string;
            GenerateControls(city, citiesFilters, 2);
        }

        private void StylesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this style.
            string style = (sender as ComboBox).SelectedItem as string;
            GenerateControls(style, stylesFilters, 3);

        }

        private void FromPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this from price.
            ComboBoxItem priceItem = (ComboBoxItem)fromPriceVal.SelectedItem;
            string fromPrice = priceItem.Content.ToString();
            if (priceFilter.Count > 0)
            {
                priceFilter.Clear();
            }
            GenerateControls(fromPrice, priceFilter, 4);
            fromP = fromPrice;
        }

        private void ToPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this to price.
            ComboBoxItem priceItem = (ComboBoxItem)toPriceVal.SelectedItem;
            string toPrice = priceItem.Content.ToString();
            if (priceFilter.Count > 0)
            {
                priceFilter.Clear();
            }
            GenerateControls(toPrice, priceFilter, 5);
            toP = toPrice;
        }

        private void deleteChoises_Click(object sender, RoutedEventArgs e)
        {
            //stackPanel = (StackPanel)FindName("choises");
            //stackPanel.Children.Clear();
            //stackPanel.Children.Add(titleChoices);

            //countries.SelectedItem = null;
            //cities.SelectedItem = null;
            //styles.SelectedItem = null;

            //CountryFilter = "";

            //countries.IsEnabled = true;

            //if (CitiesFilters != null)
            //{
            //    citiesFilters.Clear();
            //    CitiesFilters = citiesFilters;
            //}
            //if (StylesFilters != null)
            //{
            //    stylesFilters.Clear();
            //    StylesFilters = stylesFilters;
            //}


        }

        private void FromRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this from price.
            ComboBoxItem rateItem = (ComboBoxItem)fromRateVal.SelectedItem;
            string fromRate = rateItem.Content.ToString();
            if (rateFilter.Count > 0)
            {
                rateFilter.Clear();
            }
            GenerateControls(fromRate, rateFilter, 0);
            fromR = Double.Parse(fromRate);
        }

        private void ToRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this to price.
            ComboBoxItem rateItem = (ComboBoxItem)toRateVal.SelectedItem;
            string toRate = rateItem.Content.ToString();
            if (rateFilter.Count > 0)
            {
                rateFilter.Clear();
            }
            GenerateControls(toRate, rateFilter, 0);
            toR = Double.Parse(toRate);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CitiesFilters = citiesFilters;
            StylesFilters = stylesFilters;
            if (priceFilter.Count > 0)
            {
                priceFilter.Clear();
            }
            if (fromP.Equals(""))
            {
                priceFilter.Add("$");
            }
            else
            {
                priceFilter.Add(fromP);
            }
            if (toP.Equals(""))
            {
                priceFilter.Add("$$$$$");
            }
            else
            {
                priceFilter.Add(toP);
            }
            PriceFilter = priceFilter;


            if (rateFilter.Count > 0)
            {
                rateFilter.Clear();
            }
            if (toR == 0)
            {
                toR = 5;
            }
            
            RateFilter = new List<double> { fromR, toR };

            fromP = "";
            toP = "";
            fromR = 0;
            toR = 0;

            //stackPanel = (StackPanel)FindName("restList");
            //title.Visibility = Visibility.Visible;
            //sort.Visibility = Visibility.Visible;
            //comboSort.Visibility = Visibility.Visible;
            //fillRestStackPanel();
        }

        private void fillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum = RestaurantsResult.Count;
            if (RestaurantsResult == null || restNum == 0)
            {
                //noRest.Visibility = Visibility.Visible;
                //searchResults.Visibility = Visibility.Collapsed;
               
            }
            else
            {
                //searchResults.Visibility = Visibility.Visible;
                // There are exists restuarents.
                for (int i = 0; i < restNum; i++)
                {
                    // TODO : change restID to accept id.
                    //string restID = "";
                    string restID = RestaurantsResult[i].ID; // DELETE
                    
                    RestaurantView rest = new RestaurantView(this, restID);

                    rest.restName.Content = RestaurantsResult[i].Name;
                    if(RestaurantsResult[i].Types.Count != 0) {
                        rest.styleName.Content = RestaurantsResult[i].Types[0] + " | ";
                    }
                    else 
                    {
                        rest.styleName.Content = "None";
                    }
                   
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

        private void filtersChanged(object sender, SelectionChangedEventArgs e)
        {
            var filterChoice = (sender as ComboBox).SelectedItem;
            string filterChoiceValue = filterChoice.ToString();
            if (filterChoiceValue.Contains("A-Z"))
            {
                COrder = "name";
                CAsc = true;
            }
            else if (filterChoiceValue.Contains("Z-A"))
            {
                COrder = "name";
                CAsc = false;
            }
            else if (filterChoiceValue.Contains("Raiting low to height"))
            {
                COrder = "raiting";
                CAsc = true;
            }
            else
            {
                COrder = "raiting";
                CAsc = false;
            }


            // TODO: get correct filter by choice - clear restview and Re-insert 'fillRest..'  
            stackPanel = (StackPanel)FindName("restList");
            stackPanel.Children.Clear();
            // correct filter 
            fillRestStackPanel();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Clean rest results in Model.
            RestaurantsResult.Clear();

            // Go Back button - show client option view.
            ClientOptionsView clientOption = new ClientOptionsView();
            clientOption.Show();
            this.Close();
        }

        // TODO:
        private void SearchName_Click(object sender, RoutedEventArgs e)
        {
            NameFilter = name.Text;
            
            // TODO : Show Rest results.
        }

    }
}