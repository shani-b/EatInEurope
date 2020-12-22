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
            InitializeComponent();
            usernameValue.Content = "Hello " + username;

            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantOwner(model);

            var VMMyRests = "VM_RestsResults";
            var bindingMyRests = new Binding(VMMyRests) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyRestaurantsProperty, bindingMyRests);
        }
    }
}
