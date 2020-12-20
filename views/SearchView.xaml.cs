using EatInEurope.Views;
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


namespace EatInEurope.views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        StackPanel stackPanel;
        List<string> filters;



        public SearchView()
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelSearch(model);
            InitializeComponent();
            stackPanel = (StackPanel)FindName("choises");
            filters = new List<string>();
        }
        public void GenerateControls(string newVal)
        {
            var exist = filters.FirstOrDefault(val => val.Equals(newVal));
            if (exist == null)
            {
                filters.Add(newVal);
                TextBlock filter = new TextBlock();
                filter.Name = newVal;
                filter.Text = newVal;
                stackPanel.Children.Add(filter);
                stackPanel.RegisterName(filter.Name, filter);
            }
        }

        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (sender as ComboBox).SelectedItem as string;
            GenerateControls(country);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            RestaurantView rest1 = new RestaurantView();
            rest1.Visibility = Visibility.Visible;
        }
    }
}
