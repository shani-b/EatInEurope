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
        List<string> restDetails;
        string restID;
        public RestaurantDetails(List<string> idRest, bool whoIsIt)
        {
            InitializeComponent();
            //editView.Visibility = Visibility.Collapsed;

            bool isClient = whoIsIt;

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
                // TODO: CHANGE!! MEANTIME ACCEPT NAME INSTED ID!!!!!!!!
                restID = idRest[0];
                restDetails = idRest;


                // TODO: quere get this resturnts details.
                // In the meantime get the details listed from the list of restaurants in RestOW/ClientW
                restName.Content = idRest[0];
                location.Content = idRest[1] + ", " + idRest[2];
                raitingNum.Content = idRest[5];
                // fill stars color
                if (idRest[5] == "5")
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                    star4.Fill = Brushes.Yellow;
                    star5.Fill = Brushes.Yellow;
                }
                else if (idRest[5] == "4")
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                    star4.Fill = Brushes.Yellow;
                }
                else if (idRest[5] == "3")
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                    star3.Fill = Brushes.Yellow;
                }
                else if (idRest[5] == "2")
                {
                    star1.Fill = Brushes.Yellow;
                    star2.Fill = Brushes.Yellow;
                }
                else if (idRest[5] == "1")
                {
                    star1.Fill = Brushes.Yellow;
                }
                else
                {
                    // half- .5 -think what to do?????
                }


                priceValue.Content = idRest[6];

                // TODO : FOR for list of styles
                stp = (StackPanel)FindName("styleList");
                string styleName = idRest[3];
                Style s = new Style(styleName);

                stp.Children.Add(s);
                styleName = idRest[4];
                Style s1 = new Style(styleName);
                stp.Children.Add(s1);

                // TODO : FOR for list of reviews
                // Chck if no Review
                //int numReviews;
                //if (numReviews == 0)
                //{
                //    // no Reviews massenger!!!
                //    // bold add Reviews button!!!!!!!!
                //}
                stp = (StackPanel)FindName("reviewsList");
                string iReviews = idRest[8];
                Review r = new Review(iReviews);
                stp.Children.Add(r);

                iReviews = idRest[9];
                Review r1 = new Review(iReviews);
                stp.Children.Add(r1);

                Uri hyperlink = new Uri("https://www.tripadvisor.co.il" + idRest[10]);
                urlAdd.NavigateUri = hyperlink;
                //urlText.Content = idRest[10];
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
