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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private string idRest;
        public Edit(string restID)
        {
            InitializeComponent();
            idRest = restID;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            // quere update of restID 
            // after update!!
            // TODO: change null to idrest
            RestaurantDetails rd = new RestaurantDetails(null, false);
            rd.Show();
            this.Close();
        }
    }
}
