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

        public SearchView()
        {
            IModel model = new DataBaseModel();
            DataContext = new ViewModelSearch(model);
            InitializeComponent();
            stackPanel = (StackPanel)FindName("choises");
        }
        public void GenerateControls(string newVal)
        {
            TextBlock filter = new TextBlock();
            filter.Name = newVal;
            filter.Text = newVal;
            stackPanel.Children.Add(filter);
            stackPanel.RegisterName(filter.Name, filter);
        }

        private void countriesChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (sender as ComboBox).SelectedItem as string;
            GenerateControls(country);
        }
    }
}
