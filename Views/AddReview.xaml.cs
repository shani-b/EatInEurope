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
    /// Interaction logic for AddReview.xaml
    /// </summary>
    public partial class AddReview : Window
    {
        String RestID;
        public AddReview(String idRest)
        {
            InitializeComponent();
            RestID = idRest;
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {

            // TODO: after quere INSERT. (we exept to see the new review there)
            // TODO: change null to RestID
            RestaurantDetails rd = new RestaurantDetails(null, true);
            rd.Show();
            this.Close();
        }
    }
}
