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
    /// Interaction logic for RestaurantOwnerWindow.xaml
    /// </summary>
    public partial class RestaurantOwnerWindow : Window
    {
        private StackPanel stp;
        private string ownerName;

        //public List<List<string>> MyRestaurants

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
        public RestaurantOwnerWindow(string username)
        {

            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantOwner(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            InitializeComponent();
            ownerName = username;
            usernameValue.Content = "Hello " + ownerName;
           
            noRest.Visibility = Visibility.Collapsed;

            stp = (StackPanel)FindName("restList");
           
            fillRestStackPanel();


        }

        public string getOwner()
        {
            return ownerName;
        }


        // TODO : TEMP FUNC - DELETE
        public Restaurant getDetailsByID(string id)
        {
            for(int i = 0; i < MyRestaurants.Count; i++)
            {
               if( MyRestaurants[i].ID == id)
                {
                    return MyRestaurants[i];
                }
            }
            return null;
        }

        private void addRest_Click(object sender, RoutedEventArgs e)
        {
            AddRest ar = new AddRest(ownerName);
            ar.Show();
            this.Close();
        }

        private void filterByAZ_Click(object sender, RoutedEventArgs e)
        {
            stp.Children.Clear();
            // filter A-Z
            fillRestStackPanel();
        }
        private void fillRestStackPanel()
        {
            // Check how many restuarents exsits.
            int restNum = MyRestaurants.Count;
            if (MyRestaurants == null || restNum == 0)
            {
                noRest.Visibility = Visibility.Visible;
                myRest.Visibility = Visibility.Collapsed;
                // TODO: Change color.
                addRest.Background = Brushes.Yellow;
            }
            else
            {
                // There are exists restuarents.
                for (int i = 0; i < restNum; i++)
                {

                    //string restID = "";
                    string restID = MyRestaurants[i].ID; // DELETE
                    Rest rest = new Rest(this, restID);

                    rest.restName.Content = MyRestaurants[i].Name;
                    rest.styleName.Content = MyRestaurants[i].Types;
                    rest.cityName.Content = MyRestaurants[i].City;
                    rest.CountryName.Content = MyRestaurants[i].Country;

                    // TODO: FUNC ???
                    if (MyRestaurants[i].Rate == 5)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                        rest.star5.Fill = Brushes.Yellow;
                    }
                    else if (MyRestaurants[i].Rate == 4)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                        rest.star4.Fill = Brushes.Yellow;
                    }
                    else if (MyRestaurants[i].Rate == 3)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                        rest.star3.Fill = Brushes.Yellow;
                    }
                    else if (MyRestaurants[i].Rate == 2)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                        rest.star2.Fill = Brushes.Yellow;
                    }
                    else if (MyRestaurants[i].Rate == 1)
                    {
                        rest.star1.Fill = Brushes.Yellow;
                    }
                    else
                    {
                        // half- .5 -think what to do?????
                    }

                    stp.Children.Add(rest);

                }

                //string name = MyRestaurants[0][0];

            }
        }

        private void filterByZA_Click(object sender, RoutedEventArgs e)
        {
            stp.Children.Clear();
            // filter Z-A
            fillRestStackPanel();
        }

        private void filterByLow_Click(object sender, RoutedEventArgs e)
        {
            stp.Children.Clear();
            // filter raiting low
            fillRestStackPanel();
        }

        private void filterByHeight_Click(object sender, RoutedEventArgs e)
        {
            stp.Children.Clear();
            // filter height
            fillRestStackPanel();
        }
    }

}
