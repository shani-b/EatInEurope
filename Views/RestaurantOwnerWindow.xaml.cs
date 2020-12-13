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
        public RestaurantOwnerWindow(string username)
        {
            InitializeComponent();
            usernameValue.Content = "Hello " + username;
        }
    }
}
