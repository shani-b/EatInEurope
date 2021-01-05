using EatInEurope.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RestaurantDetails.xaml
    /// </summary>
    public partial class RestaurantDetails : Window
    {
        private StackPanel stp;
        Restaurant restDetails;
        string restID;

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

        public RestaurantDetails(string idRest, bool whoIsIt)
        {
            // TODO: prameter whousit
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantDetails(model);
            InitializeComponent();

            var VMRestID = "VM_RestID";
            var bindingRestID = new Binding(VMRestID) { Mode = BindingMode.TwoWay };
            this.SetBinding(RestIDProperty, bindingRestID);

            // send Model the rest id.
             RestID = idRest;
            // TODO:  May delete this filed
            restID = idRest;
            var VMRestDetails = "VM_RestDetails";
            var bindingRestDetails = new Binding(VMRestDetails) { Mode = BindingMode.OneWay };
            this.SetBinding(RestDetailsProperty, bindingRestDetails);

            var VMIsClient = "VM_IsClient";
            var bindingIsClient = new Binding(VMIsClient) { Mode = BindingMode.OneWay };
            this.SetBinding(WhoIsItProperty, bindingIsClient);



            bool isClient = WhoIsIt;

            if (isClient)
            {
                // It's client!!!!

                edit.Visibility = Visibility.Collapsed;
            }
            else
            {
                // It's rest owner!!!!
                addNewReview.Visibility = Visibility.Collapsed;
            }

            if (idRest == null)
            {
                Console.WriteLine("no rest id exists"); // TODO: change!! ERROR!!!
            }
            else
            {
                

                // fill view from the binding rest details.
                restName.Content = RestDetails.Name;
                location.Content = RestDetails.City + ", " + RestDetails.Country;
                raitingNum.Content = RestDetails.Rate;
                // fill stars color
                if (RestDetails.Rate == 5)
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                    star4.Fill = Brushes.Yellow;
                    star5.Fill = Brushes.Yellow;
                }
                else if (RestDetails.Rate == 4)
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                    star4.Fill = Brushes.Yellow;
                }
                else if (RestDetails.Rate == 3)
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                }
                else if (RestDetails.Rate == 2)
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                }
                else if (RestDetails.Rate == 1)
                {
                    star1.Fill = Brushes.Yellow;
                }
                else
                {
                    // half- .5 -think what to do?????
                }


                priceValue.Content = RestDetails.PriceRange;

                //  list of styles
                stp = (StackPanel)FindName("styleList");
                for (int i=0; i< RestDetails.Types.Count; i++)
                {
                    Style s = new Style(RestDetails.Types[i]);
                    stp.Children.Add(s);
                }

                // TODO : FOR for list of reviews
                // Chck if no Review
                //int numReviews;
                //if (numReviews == 0)
                //{
                //    // no Reviews massenger!!!
                //    // bold add Reviews button!!!!!!!!
                //}
                stp = (StackPanel)FindName("reviewsList");

             
                // TODO: delete stars from review

                List<UserReview> iReviews = RestDetails.Reviews;
                for (int i = 0; i < RestDetails.Reviews.Count; i++)
                {
                    Review r = new Review(RestDetails.Reviews[i].Content);
                    stp.Children.Add(r);
                }

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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            details.Visibility = Visibility.Collapsed;
            Edit ev = new Edit(restID);
            ev.Show();
            this.Close();
        }

        private void addNewReview_Click(object sender, RoutedEventArgs e)
        {
            // TODO : FOR for list of reviews
            AddReview ar = new AddReview(restID);
            ar.Show();
            this.Close();
        }
    }
}
