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
    /// Interaction logic for Rest.xaml
    /// </summary>
    public partial class Rest : UserControl
    {
        string restID;
        RestaurantOwnerWindow restow;
        bool isClient;
        public Rest(RestaurantOwnerWindow rest, string idRest)
        {
            InitializeComponent();
            restID = idRest;
            restow = rest;
            isClient = false;
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            // detaile window without recomends and raiting 
            // with add and edit options.
            // TODO: THIS THE CORRECT LINE : string showThisRestDetailes = restID;
            List<string> showThisRestDetailes = restow.getDetailsByID(restID);
            RestaurantDetails restD = new RestaurantDetails(showThisRestDetailes, isClient);
            restD.Show();
            restow.Close();

        }

        private void deleteRest_Click(object sender, RoutedEventArgs e)
        {
            // delete this rest
            string restToDelete = restID;
            //  TODO : quere - delete in DB
            string ownerRest = restow.getOwner();
            
            RestaurantOwnerWindow rest = new RestaurantOwnerWindow(ownerRest);
            rest.Show();
            restow.Close();
        }
    }
}
