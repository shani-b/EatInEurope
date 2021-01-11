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
        char numFP;
        char numTP;
        char numFR;
        char numTR;
        bool isBackFromDetails;

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

        public string CurrentRestID
        {
            get { return (string)GetValue(CurrentRestIDProperty); }
            set
            {
                SetValue(CurrentRestIDProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentRestIDProperty =
            DependencyProperty.Register("CurrentRestID", typeof(string), typeof(Search));


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



        public bool CLoadMoreRests
        {
            get { return (bool)GetValue(CLoadMoreRestsProperty); }
            set
            {
                SetValue(CLoadMoreRestsProperty, value);
            }
        }
        public static readonly DependencyProperty CLoadMoreRestsProperty =
            DependencyProperty.Register("CLoadMoreRests", typeof(bool), typeof(RestaurantOwnerWindow));



        public bool CEndOfRests
        {
            get { return (bool)GetValue(CEndOfRestsProperty); }
            set
            {
                SetValue(CEndOfRestsProperty, value);
            }
        }
        public static readonly DependencyProperty CEndOfRestsProperty =
            DependencyProperty.Register("CEndOfRests", typeof(bool), typeof(RestaurantOwnerWindow));






        public bool StartSearch
        {
            get { return (bool)GetValue(StartSearchProperty); }
            set
            {
                SetValue(StartSearchProperty, value);
            }
        }

        public static readonly DependencyProperty StartSearchProperty =
            DependencyProperty.Register("StartSearch", typeof(bool), typeof(RestaurantOwnerWindow));


        public Search(bool flag)
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

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(CurrentRestIDProperty, bindingRestID);

            var VMOrder = "VM_Order";
            var bindingOrder = new Binding(VMOrder) { Mode = BindingMode.TwoWay };
            this.SetBinding(COrderProperty, bindingOrder);

            var VMAsc = "VM_Asc";
            var bindingAsc = new Binding(VMAsc) { Mode = BindingMode.TwoWay };
            this.SetBinding(CAscProperty, bindingAsc);

            var VMStartSearch = "VM_StartSearch";
            var bindingStartSearch = new Binding(VMStartSearch) { Mode = BindingMode.TwoWay };
            this.SetBinding(StartSearchProperty, bindingStartSearch);


            var VMLoadMoreRests = "VM_LoadMoreRests";
            var bindingLoadMoreRests = new Binding(VMLoadMoreRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(CLoadMoreRestsProperty, bindingLoadMoreRests);

            var VMEndOfRests = "VM_EndOfRests";
            var bindingEndOfRests = new Binding(VMEndOfRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(CEndOfRestsProperty, bindingEndOfRests);





            InitializeComponent();

            // Hides these objects until the visibility changes.
            choices.Visibility = Visibility.Collapsed;
            nameChoice.Visibility = Visibility.Collapsed;
            generic.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Collapsed;
            style.Visibility = Visibility.Collapsed;
            sortIcon.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            comboSort.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            searchResults.Visibility = Visibility.Collapsed;
            noRest.Visibility = Visibility.Collapsed;
            moreRests.Visibility = Visibility.Collapsed;
            cities.IsEnabled = false;

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
            numFP = '0';
            numTP = '0';
            numFR = '0';
            numTR = '0';

            isBackFromDetails = flag;
            if (isBackFromDetails)
            {
                // No Enabled to serach again untul clear the last search filters.
                search.IsEnabled = false;
                searchName.IsEnabled = false;
                deleteChoises.Background = Brushes.Yellow;

                // Show the rest results view.
                title.Visibility = Visibility.Visible;
                sort.Visibility = Visibility.Visible;
                comboSort.Visibility = Visibility.Visible;
                sortIcon.Visibility = Visibility.Visible;
                searchResults.Visibility = Visibility.Visible;
                moreRests.Visibility = Visibility.Visible;
                stackPanel = (StackPanel)FindName("restList");
                FillRestStackPanel();
            }
        }

        private void GenerateControls(string newVal, List<string> list, int flag)
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

                // Add visibilty of choices.
                choices.Visibility = Visibility.Visible;
                generic.Visibility = Visibility.Visible;
                city.Visibility = Visibility.Visible;
                style.Visibility = Visibility.Visible;
                allChoice.Visibility = Visibility.Visible;

                // Fill choice view.
                TextBlock filter = new TextBlock();
                switch (flag)
                {
                    case 1:
                        // Country.
                        stackPanel = (StackPanel)FindName("countrySTP");
                        filter.Text = newVal;
                        stackPanel.Children.Clear();
                        stackPanel.Children.Add(filter);
                        break;
                    case 2:
                        // Cities.
                        stackPanel = (StackPanel)FindName("citiesSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 3:
                        // Styles.
                        stackPanel = (StackPanel)FindName("stylesSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Add(filter);
                        break;
                    case 4:
                        // Price - from.
                        stackPanel = (StackPanel)FindName("priceFSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Clear();
                        stackPanel.Children.Add(filter);
                        break;
                    case 5:
                        // Price - to.
                        stackPanel = (StackPanel)FindName("priceTSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Clear();
                        stackPanel.Children.Add(filter);
                        break;
                    case 6:
                        // Rate - from.
                        stackPanel = (StackPanel)FindName("rateFSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Clear();
                        stackPanel.Children.Add(filter);
                        break;
                    case 7:
                        // Rate - to.
                        stackPanel = (StackPanel)FindName("rateTSTP");
                        filter.Text = newVal;
                        stackPanel.Children.Clear();
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

            // Enabled just one time choose country, and just cities from this country.
            countryCombo.IsEnabled = false;
            cities.IsEnabled = true;
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
            if(priceItem != null)
            {
                string fromPrice = priceItem.Content.ToString();
                priceFilter.Clear();
                GenerateControls(fromPrice, priceFilter, 4);

                // Update fromP filed.
                fromP = fromPrice;

                // Enable in 'to price' only the possible prices depending on the selected price.
                RestartPriceCombo('F', numTP);
                switch (priceItem.Name[1])
                {
                    case '1':
                        t1.Visibility = Visibility.Collapsed;
                        break;
                    case '2':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        break;
                    case '3':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        break;
                    case '4':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        t4.Visibility = Visibility.Collapsed;
                        break;
                    case '5':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        t4.Visibility = Visibility.Collapsed;
                        t5.Visibility = Visibility.Collapsed;
                        break;
                }
                numFP = priceItem.Name[1];
            }
        }

        private void ToPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this to price.
            ComboBoxItem priceItem = (ComboBoxItem)toPriceVal.SelectedItem;
            if (priceItem != null)
            {
                string toPrice = priceItem.Content.ToString();
                priceFilter.Clear();
                GenerateControls(toPrice, priceFilter, 5);

                // Update toP filed.
                toP = toPrice;

                // Enable in 'from price' only the possible prices depending on the selected price.
                RestartPriceCombo('T', numFP);
                switch (priceItem.Name[1])
                {
                    case '5':
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '4':
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '3':
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '2':
                        f2.Visibility = Visibility.Collapsed;
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '1':
                        f1.Visibility = Visibility.Collapsed;
                        f2.Visibility = Visibility.Collapsed;
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                }
                numTP = priceItem.Name[1];
            }   
        }

        private void RestartPriceCombo(char flag, char num)
        {
            f1.Visibility = Visibility.Visible;
            f2.Visibility = Visibility.Visible;
            f3.Visibility = Visibility.Visible;
            f4.Visibility = Visibility.Visible;
            f5.Visibility = Visibility.Visible;

            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            t3.Visibility = Visibility.Visible;
            t4.Visibility = Visibility.Visible;
            t5.Visibility = Visibility.Visible;

            if (flag == 'F')
            {
                switch (num)
                { 
                    case '1':
                        f1.Visibility = Visibility.Collapsed;
                        f2.Visibility = Visibility.Collapsed;
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '2': 
                        f2.Visibility = Visibility.Collapsed;
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '3':
                        f3.Visibility = Visibility.Collapsed;
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '4':
                        f4.Visibility = Visibility.Collapsed;
                        f5.Visibility = Visibility.Collapsed;
                        break;
                    case '5':
                        f5.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            else // 'T'
            {
                switch (num)
                {
                    case '1':
                        t1.Visibility = Visibility.Collapsed;
                        break;
                    case '2':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        break;
                    case '3':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        break;
                    case '4':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        t4.Visibility = Visibility.Collapsed;
                        break;
                    case '5':
                        t1.Visibility = Visibility.Collapsed;
                        t2.Visibility = Visibility.Collapsed;
                        t3.Visibility = Visibility.Collapsed;
                        t4.Visibility = Visibility.Collapsed;
                        t5.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void FromRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this from price.
            ComboBoxItem rateItem = (ComboBoxItem)fromRateVal.SelectedItem;
            if (rateItem != null)
            {
                string fromRate = rateItem.Content.ToString();
                rateFilter.Clear();
                GenerateControls(fromRate, rateFilter, 6);

                // Update fromR filed.
                fromR = Double.Parse(fromRate);

                // Enable in 'to rate' only the possible ratings depending on the selected rate.
                RestartRateCombo('F', numTR);
                switch (rateItem.Name[1])
                {
                    case 'a':
                        ta.Visibility = Visibility.Collapsed;
                        break;
                    case 'b':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        break;
                    case 'c':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        break;
                    case 'd':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        break;
                    case 'e':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        break;
                    case 'f':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        break;
                    case 'g':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        break;
                    case 'h':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        break;
                    case 'i':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        ti.Visibility = Visibility.Collapsed;
                        break;
                    case 'j':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        ti.Visibility = Visibility.Collapsed;
                        tj.Visibility = Visibility.Collapsed;
                        break;
                }
                numFR = rateItem.Name[1];
            }
        }

        private void ToRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the Choice view with this to price.
            ComboBoxItem rateItem = (ComboBoxItem)toRateVal.SelectedItem;
            if (rateItem != null) {
                string toRate = rateItem.Content.ToString();
                rateFilter.Clear();
                GenerateControls(toRate, rateFilter, 7);

                // Update toR filed.
                toR = Double.Parse(toRate);

                // Enable in 'to rate' only the possible ratings depending on the selected rate.
                RestartRateCombo('T', numFR);
                switch (rateItem.Name[1])
                {
                    case 'j':
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'i':
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'h':
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'g':
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'f':
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'e':
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'd':
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'c':
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'b':
                        fb.Visibility = Visibility.Collapsed;
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'a':
                        fa.Visibility = Visibility.Collapsed;
                        fb.Visibility = Visibility.Collapsed;
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                }
                numTR = rateItem.Name[1];
            }
        }

        private void RestartRateCombo(char flag, char num)
        {
            fa.Visibility = Visibility.Visible;
            fb.Visibility = Visibility.Visible;
            fc.Visibility = Visibility.Visible;
            fd.Visibility = Visibility.Visible;
            fe.Visibility = Visibility.Visible;
            ff.Visibility = Visibility.Visible;
            fg.Visibility = Visibility.Visible;
            fh.Visibility = Visibility.Visible;
            fi.Visibility = Visibility.Visible;
            fj.Visibility = Visibility.Visible;

            ta.Visibility = Visibility.Visible;
            tb.Visibility = Visibility.Visible;
            tc.Visibility = Visibility.Visible;
            td.Visibility = Visibility.Visible;
            te.Visibility = Visibility.Visible;
            tf.Visibility = Visibility.Visible;
            tg.Visibility = Visibility.Visible;
            th.Visibility = Visibility.Visible;
            ti.Visibility = Visibility.Visible;
            tj.Visibility = Visibility.Visible;

            if (flag == 'F')
            {
                switch (num)
                {
                    case 'a':
                        fa.Visibility = Visibility.Collapsed;
                        fb.Visibility = Visibility.Collapsed;
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'b':
                        fb.Visibility = Visibility.Collapsed;
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'c':
                        fc.Visibility = Visibility.Collapsed;
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'd':
                        fd.Visibility = Visibility.Collapsed;
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'e': 
                        fe.Visibility = Visibility.Collapsed;
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'f':
                        ff.Visibility = Visibility.Collapsed;
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'g':
                        fg.Visibility = Visibility.Collapsed;
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'h':
                        fh.Visibility = Visibility.Collapsed;
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'i':
                        fi.Visibility = Visibility.Collapsed;
                        fj.Visibility = Visibility.Collapsed;
                        break;
                    case 'j':
                        fj.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            else // 'T'
            {
                switch (num)
                {
                    case 'a':
                        ta.Visibility = Visibility.Collapsed;
                        break;
                    case 'b':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        break;
                    case 'c':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        break;
                    case 'd':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        break;
                    case 'e':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        break;
                    case 'f':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        break;
                    case 'g':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        break;
                    case 'h':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        break;
                    case 'i':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        ti.Visibility = Visibility.Collapsed;
                        break;
                    case 'j':
                        ta.Visibility = Visibility.Collapsed;
                        tb.Visibility = Visibility.Collapsed;
                        tc.Visibility = Visibility.Collapsed;
                        td.Visibility = Visibility.Collapsed;
                        te.Visibility = Visibility.Collapsed;
                        tf.Visibility = Visibility.Collapsed;
                        tg.Visibility = Visibility.Collapsed;
                        th.Visibility = Visibility.Collapsed;
                        ti.Visibility = Visibility.Collapsed;
                        tj.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void DeleteChoises_Click(object sender, RoutedEventArgs e)
        {
            // Clean choice view.
            stackPanel = (StackPanel)FindName("countrySTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("priceFSTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("priceTSTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("rateFSTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("rateTSTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("citiesSTP");
            stackPanel.Children.Clear();
            stackPanel = (StackPanel)FindName("stylesSTP");
            stackPanel.Children.Clear();
            if (nameChoice.Visibility == Visibility.Visible)
            {
                stackPanel = (StackPanel)FindName("nameSTP");
                stackPanel.Children.Clear();
                nameChoice.Visibility = Visibility.Collapsed;
            }
            choices.Visibility = Visibility.Collapsed;

            // Clean the selected filters value in the comboBox.
            countryCombo.SelectedIndex = -1;
            countryCombo.IsEnabled = true;
            cities.IsEnabled = false;
            cities.SelectedIndex = -1;
            styles.SelectedIndex = -1;
            fromPriceVal.SelectedIndex = -1;
            toPriceVal.SelectedIndex = -1;
            fromRateVal.SelectedIndex = -1;
            toRateVal.SelectedIndex = -1;
            name.Text = "";
            // for the price
            f1.Visibility = Visibility.Visible;
            f2.Visibility = Visibility.Visible;
            f3.Visibility = Visibility.Visible;
            f4.Visibility = Visibility.Visible;
            f5.Visibility = Visibility.Visible;
            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            t3.Visibility = Visibility.Visible;
            t4.Visibility = Visibility.Visible;
            t5.Visibility = Visibility.Visible;
            // for the rate.
            fa.Visibility = Visibility.Visible;
            fb.Visibility = Visibility.Visible;
            fc.Visibility = Visibility.Visible;
            fd.Visibility = Visibility.Visible;
            fe.Visibility = Visibility.Visible;
            ff.Visibility = Visibility.Visible;
            fg.Visibility = Visibility.Visible;
            fh.Visibility = Visibility.Visible;
            fi.Visibility = Visibility.Visible;
            fj.Visibility = Visibility.Visible;
            ta.Visibility = Visibility.Visible;
            tb.Visibility = Visibility.Visible;
            tc.Visibility = Visibility.Visible;
            td.Visibility = Visibility.Visible;
            te.Visibility = Visibility.Visible;
            tf.Visibility = Visibility.Visible;
            tg.Visibility = Visibility.Visible;
            th.Visibility = Visibility.Visible;
            ti.Visibility = Visibility.Visible;
            tj.Visibility = Visibility.Visible;

            // Restart fileds value.
            countryFilters.Clear();
            citiesFilters.Clear();
            stylesFilters.Clear();
            priceFilter.Clear();
            rateFilter.Clear();
            NameFilter = null;
            fromP = "";
            toP = "";
            fromR = 0;
            toR = 0;
            numFP = '0';
            numTP = '0';
            numFR = '0';
            numTR = '0';
            fromPriceVal.SelectedItem = null;
            toPriceVal.SelectedItem = null;

            // Search buttons enables.
            search.IsEnabled = true;
            searchName.IsEnabled = true;
            deleteChoises.Background = Brushes.LightGray;

            // Hidden the rest results view.
            noRest.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            sort.Visibility = Visibility.Collapsed;
            noRest.Visibility = Visibility.Collapsed;
            comboSort.Visibility = Visibility.Collapsed;
            comboSort.IsEnabled = true;
            comboSort.SelectedIndex = -1;
            sortIcon.Visibility = Visibility.Collapsed;
            searchResults.Visibility = Visibility.Collapsed;
            moreRests.Visibility = Visibility.Collapsed;
            moreRests.IsEnabled = true;
            stackPanel = (StackPanel)FindName("restList");
            stackPanel.Children.Clear();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Update in the model the chosen country.
            if (countryFilters.Count > 0)
            {
                CountryFilter = countryFilters[0];
            }

            // Update in the model the chosen cities.
            CitiesFilters = citiesFilters;

            // Update in the model the chosen styles.
            StylesFilters = stylesFilters;

            // Update in the model the chosen price.
            priceFilter.Clear();
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

            // Update in the model the chosen rate.
            rateFilter.Clear();
            if (toR != 0 || fromR != 0)
            {
                RateFilter = new List<double> { fromR, toR };
            }
            StartSearch = true;

            // No Enabled to serach again until clear the last search filters.
            search.IsEnabled = false;
            searchName.IsEnabled = false;
            deleteChoises.Background = Brushes.Yellow;

            // Show the rest results view.
            title.Visibility = Visibility.Visible;
            sort.Visibility = Visibility.Visible;
            comboSort.Visibility = Visibility.Visible;
            sortIcon.Visibility = Visibility.Visible;
            searchResults.Visibility = Visibility.Visible;
            moreRests.Visibility = Visibility.Visible;
            stackPanel = (StackPanel)FindName("restList");
            FillRestStackPanel();


        }

        private void SearchName_Click(object sender, RoutedEventArgs e)
        {
            // Fill the Choice view with this name.
            if (!name.Text.Equals(""))
            {
                choices.Visibility = Visibility.Visible;
                nameChoice.Visibility = Visibility.Visible;
                TextBlock filter = new TextBlock();
                stackPanel = (StackPanel)FindName("nameSTP");
                filter.Text = name.Text;
                filter.FontSize = 15;
                stackPanel.Children.Add(filter);
            }
            allChoice.Visibility = Visibility.Collapsed;

            // No Enabled to serach again until clear the last search filters.
            search.IsEnabled = false;
            searchName.IsEnabled = false;
            deleteChoises.Background = Brushes.Yellow;

            // Update the in the model the chosen rest name.
            NameFilter = name.Text;

            // Show the rest results view.
            title.Visibility = Visibility.Visible;
            sort.Visibility = Visibility.Visible;
            comboSort.Visibility = Visibility.Visible;
            sortIcon.Visibility = Visibility.Visible;
            searchResults.Visibility = Visibility.Visible;
            moreRests.Visibility = Visibility.Visible;
            stackPanel = (StackPanel)FindName("restList");
            FillRestStackPanel();


            // Clean the selected filters value in the comboBox.
            countryCombo.SelectedIndex = -1;
            cities.SelectedIndex = -1;
            styles.SelectedIndex = -1;
            fromPriceVal.SelectedIndex = -1;
            toPriceVal.SelectedIndex = -1;
            fromRateVal.SelectedIndex = -1;
            toRateVal.SelectedIndex = -1;
            if (CEndOfRests == true)
            {
                moreRests.IsEnabled = false;
                //moreRests.Visibility = Visibility.Collapsed;
            }

        }

        public void FillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum;
            if (RestaurantsResult == null || (restNum = RestaurantsResult.Count) == 0 )
            {
                // To this serch filters the are not Restaurant results.
                // Show view changes accordingly.
                
                noRest.Visibility = Visibility.Visible;
                searchResults.Visibility = Visibility.Collapsed;
                moreRests.Visibility = Visibility.Collapsed;
                comboSort.IsEnabled = false;
                // Clean the combo if used.
                comboSort.SelectedIndex = -1;
            }
            else
            {
                // There are exists restuarents in this filters.
                for (int i = 0; i < restNum; i++)
                {
                    // Fill id view.
                    string restID = RestaurantsResult[i].ID;

                    // Binding for stars raiting view.
                    CurrentRestID = restID;

                    // Create new Rest view.
                    RestaurantView rest = new RestaurantView(this, restID);

                    // Fill name view.
                    if (RestaurantsResult[i].Name == null || RestaurantsResult[i].Name.Equals(""))
                    {
                        rest.restName.Content = "None";
                    }
                    else
                    {
                        rest.restName.Content = RestaurantsResult[i].Name;
                    }

                    // Fill style view.
                    if (RestaurantsResult[i].Types == null || RestaurantsResult[i].Types.Count == 0)
                    {
                        rest.styleName.Content = "None |";
                    }
                    else
                    {
                        rest.styleName.Content = RestaurantsResult[i].Types[0] + " |";
                    }

                    // Fill location view.
                    if (RestaurantsResult[i].City != null && !RestaurantsResult[i].City.Equals("")
                        && RestaurantsResult[i].Country != null && !RestaurantsResult[i].Country.Equals(""))
                    {
                        rest.locationValue.Content = RestaurantsResult[i].City + ", " + RestaurantsResult[i].Country;
                    }
                    else if ((RestaurantsResult[i].City == null || RestaurantsResult[i].City.Equals(""))
                        && RestaurantsResult[i].Country != null && !RestaurantsResult[i].Country.Equals(""))
                    {
                        rest.locationValue.Content = "None, " + RestaurantsResult[i].Country;
                    }
                    else if (RestaurantsResult[i].City != null && !RestaurantsResult[i].City.Equals("")
                        && (RestaurantsResult[i].Country == null || !RestaurantsResult[i].Country.Equals("")))
                    {
                        rest.locationValue.Content = RestaurantsResult[i].City + ", None";
                    }
                    else
                    {
                        rest.locationValue.Content = "None, None";
                    }

                    // Add rest detailes to Rest list view.
                    stackPanel.Children.Add(rest);
                }
                if (CEndOfRests == true)
                {
                    moreRests.IsEnabled = false;
                    //moreRests.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SortChanged(object sender, SelectionChangedEventArgs e)
        {
            var filterChoice = (sender as ComboBox).SelectedItem;
            if (filterChoice == null) // Error handling.
            {
                return;
            }
            string filterChoiceValue = filterChoice.ToString();

            // Update the choice in the DB.
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
            else if (filterChoiceValue.Contains("Rating low to height"))
            {
                COrder = "rating";
                CAsc = true;
            }
            else
            {
                COrder = "rating";
                CAsc = false;
            }
            
            // Clear the List of rest view.
            stackPanel = (StackPanel)FindName("restList");
            stackPanel.Children.Clear();

            // Fill the List of rest view by the selected sort filter.
            FillRestStackPanel();
            moreRests.IsEnabled = true;
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Clean rest results in the Model.
            RestaurantsResult.Clear();
            NameFilter = null;

            // Go Back button - show client option view.
            ClientOptionsView clientOption = new ClientOptionsView();
            clientOption.Show();
            this.Close();
        }

        private void moreRests_Click(object sender, RoutedEventArgs e)
        {
            CLoadMoreRests = true;
            // Clear the List of rest view.
            stackPanel = (StackPanel)FindName("restList");
            stackPanel.Children.Clear();

            // Fill the List of rest view by the selected sort filter.
            FillRestStackPanel();

            if (CEndOfRests == true)
            {
                moreRests.IsEnabled = false;
                //moreRests.Visibility = Visibility.Collapsed;
            }
        }
    }
}