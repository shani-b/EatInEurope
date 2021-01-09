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
    /// Interaction logic for RestaurantDetails.xaml
    /// </summary>
    public partial class RestaurantDetails : Window
    {
        private StackPanel stp;
        //private string restID;

        // Properties.
        public string RestID
        {
            get { return (string)GetValue(RestIDProperty); }
            set
            {
                SetValue(RestIDProperty, value);
            }
        }

        public static readonly DependencyProperty RestIDProperty =
            DependencyProperty.Register("RestID", typeof(string), typeof(RestaurantDetails));


        public Restaurant RestDetails
        {
            get { return (Restaurant)GetValue(RestDetailsProperty); }
            set
            {
                SetValue(RestDetailsProperty, value);
            }
        }

        public static readonly DependencyProperty RestDetailsProperty =
            DependencyProperty.Register("RestDetails", typeof(Restaurant), typeof(RestaurantDetails));


        public bool WhoIsIt
        {
            get { return (bool)GetValue(WhoIsItProperty); }
            set
            {
                SetValue(WhoIsItProperty, value);
            }
        }

        public static readonly DependencyProperty WhoIsItProperty =
            DependencyProperty.Register("WhoIsIt", typeof(bool), typeof(RestaurantDetails));


        public RestaurantDetails(string idRest)
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantDetails(model);

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestIDProperty, bindingRestID);

            // Send model the rest id.
            RestID = idRest;

            var VMRestDetails = "VM_RestDetails";
            var bindingRestDetails = new Binding(VMRestDetails) { Mode = BindingMode.OneWay };
            this.SetBinding(RestDetailsProperty, bindingRestDetails);

            var VMIsClient = "VM_IsClient";
            var bindingIsClient = new Binding(VMIsClient) { Mode = BindingMode.OneWay };
            this.SetBinding(WhoIsItProperty, bindingIsClient);

            InitializeComponent();
            // Hides these objects until the visibility changes.
            noStyle.Visibility = Visibility.Collapsed;
            noReviews.Visibility = Visibility.Collapsed;
            noURL.Visibility = Visibility.Collapsed;

            bool isClient = WhoIsIt;
            // Checks who is it (clien/owner) for the correct view options.
            if (isClient)
            {
                // It's client.
                edit.Visibility = Visibility.Collapsed;
            }
            else
            {
                // It's rest owner.
                addNewReview.Visibility = Visibility.Collapsed;
            }
            

            if (idRest == null)
            {
                // Error handling
                Console.WriteLine("Error - No rest id exists");
            }
            else
            {
                // Fill view from the binding rest details.

                // Fill name view.
                if (RestDetails.Name == null || RestDetails.Name.Equals(""))
                {
                    restName.Content = "None";
                }
                else
                {
                    restName.Content = RestDetails.Name;
                }

                // Fill location view.
                if (RestDetails.City != null && !RestDetails.City.Equals("")
                    && RestDetails.Country != null && !RestDetails.Country.Equals(""))
                {
                    location.Content = RestDetails.City + ", " + RestDetails.Country;
                }
                else if ((RestDetails.City == null || RestDetails.City.Equals(""))
                    && RestDetails.Country != null && !RestDetails.Country.Equals(""))
                {
                    location.Content = "None, " + RestDetails.Country;
                }
                else if (RestDetails.City != null && !RestDetails.City.Equals("")
                    && (RestDetails.Country == null || !RestDetails.Country.Equals("")))
                {
                    location.Content = RestDetails.City + ", None";
                }
                else
                {
                    location.Content = "None, None";
                }

                // Fill rating view.
                raitingNum.Content = RestDetails.Rate;

                // Fill stars view according to rating number.
                stp = (StackPanel)FindName("stars");
                StarsRaitingView str = new StarsRaitingView();
                stp.Children.Add(str);

                // Fill price view.
                priceValue.Content = RestDetails.PriceRange;

                // Fill list of styles.
                int styleNum = RestDetails.Types.Count;
                if (styleNum == 0)
                {
                    style.Visibility = Visibility.Collapsed;
                    noStyle.Visibility = Visibility.Visible;
                }
                else
                {
                    // There is styles.
                    stp = (StackPanel)FindName("styleList");
                    for (int i = 0; i < styleNum; i++)
                    {
                        Style s = new Style(RestDetails.Types[i]);
                        stp.Children.Add(s);
                    }
                }

                // Fill list of reviews.
                List<UserReview> reviews = RestDetails.Reviews;
                if (reviews == null || reviews.Count == 0)
                {
                    review.Visibility = Visibility.Collapsed;
                    noReviews.Visibility = Visibility.Visible;
                    if (isClient)
                    {
                        addNewReview.Background = Brushes.Yellow;
                    }
                }
                else
                {
                    // There is reviews.
                    stp = (StackPanel)FindName("reviewsList");
                    for (int i = 0; i < reviews.Count; i++)
                    {
                        Review r = new Review(i + 1, reviews[i]);
                        stp.Children.Add(r);
                    }
                }

                // Fill URL view.
                if (RestDetails.URL == null || RestDetails.URL.Equals(""))
                {
                    // No URL address.
                }
                else
                {
                    // Show URL address.
                    Uri hyperlink;
                    if (RestDetails.URL.Contains("http"))
                    {
                        hyperlink = new Uri(RestDetails.URL);
                    }
                    else
                    {
                        hyperlink = new Uri("https://www.tripadvisor.co.il" + RestDetails.URL);
                    }
                    urlAdd.NavigateUri = hyperlink;
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // Display hyperlink.
            if (e.Uri.Equals("")) {
                // No URL.
                if (!WhoIsIt)
                {
                    // It's owner view.
                    noURL.Visibility = Visibility.Visible;
                    edit.Background = Brushes.Yellow;
                }
                return;
            }
            else
            {
                System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Show edit view.
            Edit edit = new Edit(RestID);
            edit.Show();
            this.Close();
        }

        private void AddNewReview_Click(object sender, RoutedEventArgs e)
        {
            // Show add review view.
            AddReview addreview = new AddReview(RestID);
            addreview.Show();
            this.Close();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            if (WhoIsIt)
            {
                // Client
                // Back button - Client view.
                Search client = new Search();
                client.Show();
                this.Close();
            }
            else
            {
                // Owner
                // Back button - Restaurant Owner window view.
                RestaurantOwnerWindow restOwner = new RestaurantOwnerWindow(RestDetails.Owner);
                restOwner.Show();
                this.Close();
            }
        }
    }
}
