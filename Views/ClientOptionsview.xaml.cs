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
    /// Interaction logic for ClientOptionsview.xaml
    /// </summary>
    public partial class ClientOptionsView : Window
    {
        public ClientOptionsView()
        {
            InitializeComponent();
        }

        private void Search_Restaurant_Click(object sender, RoutedEventArgs e)
        {
            // Show search view.
            Search search = new Search();
            search.Show();
            this.Close();
        }

        private void Search_Trip_Click(object sender, RoutedEventArgs e)
        {
            // Show Trip Search view.
            TripSearch tripSearch = new TripSearch();
            tripSearch.Show();
            this.Close();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Back button - Main window.
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
