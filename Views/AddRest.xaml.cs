using EatInEurope.ViewModels;
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
    /// Interaction logic for AddRest.xaml
    /// </summary>
    public partial class AddRest : Window
    {
        private string owner;
        private string name;
        private string country;
        private string city;
        private List<string> styles;
        private string price;
        private string url;
        private List<Restaurant> myRest;

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


        public AddRest(string ownerName)
        {
            InitializeComponent();
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelAddRest(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);

            owner = ownerName;
            styles = new List<string>();
            myRest = MyRestaurants;
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            // TODO: after quere INSERT. (we exept to see the new rest there)
            
            url = newurltAdd.Text;
            name = newrestName.Text;
            Restaurant newRest = new Restaurant("", name, country, city, styles, 0, price, 0, new List<UserReview>(), url);
            myRest.Add(newRest);
            MyRestaurants = myRest;


            RestaurantOwnerWindow row = new RestaurantOwnerWindow(owner);
            row.Show();
            this.Close();
        }

        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {
            country = (sender as ComboBox).SelectedItem as string;
        }

        private void citiesChanged(object sender, SelectionChangedEventArgs e)
        {
            city = (sender as ComboBox).SelectedItem as string;
        }

        private void typesChanged(object sender, SelectionChangedEventArgs e)
        {
            string newVal = (sender as ComboBox).SelectedItem as string;
            var exist = styles.Find(val => val.Equals(newVal));
            if (exist == null)
            {
                if (newVal == null)
                {
                    return;
                }

                styles.Add(newVal);
            }
        }

        private void lowPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem priceItem = (ComboBoxItem)lowPriceVal.SelectedItem;
            price = priceItem.Content.ToString();

            topPriceVal.IsEnabled = true;

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
                    top1.Visibility = Visibility.Collapsed;
                    top2.Visibility = Visibility.Collapsed;
                    top3.Visibility = Visibility.Collapsed;
                    top4.Visibility = Visibility.Collapsed;
                    top5.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void topPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem priceItem = (ComboBoxItem)topPriceVal.SelectedItem;
            string topPrice = priceItem.Content.ToString();
            price += " - " + topPrice;
        }
    }
}
