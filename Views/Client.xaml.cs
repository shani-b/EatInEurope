using EatInEurope.views;
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
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
            search.Visibility = Visibility.Collapsed;
            rest1.Visibility = Visibility.Collapsed;
            rest2.Visibility = Visibility.Collapsed;
        }

        private void Search_Trip_Click(object sender, RoutedEventArgs e)
        {
            TripSearch ts = new TripSearch();
            ts.Show();
            this.Close();
        }

        private void Search_Restaurant_Click(object sender, RoutedEventArgs e)
        {
            search.Visibility = Visibility.Visible;
            searchTrip.Visibility = Visibility.Collapsed;
            searchRest.Visibility = Visibility.Collapsed;
            rest1.Visibility = Visibility.Visible;
            rest2.Visibility = Visibility.Visible;
        }
    }
}
