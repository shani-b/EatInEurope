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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : UserControl
    {


        public List<string> temp;
        public RestaurantView()
        {
            InitializeComponent();
            if (temp == null)
            {
                // NO RESTURENT in this search try filter in differant way.
            }
            // TODO: DELETE {"Country", "City", "Name", "$$$", "Style","Raiting - stars"}
            temp = new List<string> {"Country", "City", "Name", "$$$", "Style","3"};
        }

        private void Rest_Details_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
