using EatInEurope.ViewModels;
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
        private string city;
        private string price;

        public Restaurant UpdateRest
        {
            get { return (Restaurant)GetValue(UpdateRestProperty); }
            set
            {
                SetValue(UpdateRestProperty, value);
            }
        }

        public static readonly DependencyProperty UpdateRestProperty =
            DependencyProperty.Register("UpdateRest", typeof(Restaurant), typeof(Edit));


        public Edit(string restID)
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelEdit(model);
            InitializeComponent();

            var VMUpdateRest = "VM_RestDetails";
            var bindingUpdateRest = new Binding(VMUpdateRest) { Mode = BindingMode.TwoWay };
            this.SetBinding(UpdateRestProperty, bindingUpdateRest);

            idRest = restID;
            city = "";
            price = "";
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Restaurant rest = UpdateRest;
            if (!restName.Text.Equals(""))
            {
                rest.Name = restName.Text;
            }
            if (!city.Equals(""))
            {
                rest.City = city;
            }
            if (!urltAdd.Text.Equals(""))
            {
                rest.URL = urltAdd.Text;
            }
            if (!price.Equals(""))
            {
                rest.PriceRange = price;
            }
            UpdateRest = rest;
            RestaurantDetails rd = new RestaurantDetails(idRest, false);
            rd.Show();
            this.Close();
        }

        private void citiesChanged(object sender, SelectionChangedEventArgs e)
        {
             city = (sender as ComboBox).SelectedItem as string;
        }

        private void lowPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem priceItem = (ComboBoxItem)lowPriceVal.SelectedItem;
            price = priceItem.Content.ToString();
            topPriceVal.IsEnabled = true;

            switch (priceItem.Name[0])
            {
                case 'a': top1.Visibility = Visibility.Collapsed;
                    break;
                case 'b':
                    top1.Visibility = Visibility.Collapsed;
                    top2.Visibility = Visibility.Collapsed;
                    break;
                case 'c':
                    top1.Visibility = Visibility.Collapsed;
                    top2.Visibility = Visibility.Collapsed;
                    top3.Visibility = Visibility.Collapsed;
                    break;
                case 'd':
                    top1.Visibility = Visibility.Collapsed;
                    top2.Visibility = Visibility.Collapsed;
                    top3.Visibility = Visibility.Collapsed;
                    top4.Visibility = Visibility.Collapsed;
                    break;
                case 'e':
                    top1.Visibility = Visibility.Collapsed;
                    top2.Visibility = Visibility.Collapsed;
                    top3.Visibility = Visibility.Collapsed;
                    top4.Visibility = Visibility.Collapsed;
                    top5.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void topPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)topPriceVal.SelectedItem;
            string topPrice = typeItem.Content.ToString();
            price += " - " + topPrice;
        }
    }
}
