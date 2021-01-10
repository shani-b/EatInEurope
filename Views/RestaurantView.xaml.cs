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
        private string restID;
        private Search search;

        public RestaurantView(Search rest, string idRest)
        {
            // Constructor.
            InitializeComponent();

            // Initialize the fileds.
            restID = idRest;
            search = rest;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            //Restaurant showThisRestDetailes = c.getDetailsByID(restID);
            RestaurantDetails restD = new RestaurantDetails(restID);
            restD.Show();
            search.Close();
        }
    }
}
