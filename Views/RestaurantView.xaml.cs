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

        string restID;
        Client c;
        bool isClient;
        public List<string> temp;
        public RestaurantView(Client rest, string idRest)
        {
            InitializeComponent();

            restID = idRest;
            c = rest;
            isClient = true;
        }


        private void details_Click(object sender, RoutedEventArgs e)
        {
            //Restaurant showThisRestDetailes = c.getDetailsByID(restID);
            RestaurantDetails restD = new RestaurantDetails(restID, isClient);
            restD.Show();
            c.Close();
        }
    }
}
