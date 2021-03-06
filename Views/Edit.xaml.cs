﻿using EatInEurope.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


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
        private bool flagFromPriceSelected;

        // Properties.
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

        public bool IsEdit
        {
            get { return (bool)GetValue(IsEditProperty); }
            set
            {
                SetValue(IsEditProperty, value);
            }
        }

        public static readonly DependencyProperty IsEditProperty =
            DependencyProperty.Register("IsEdit", typeof(bool), typeof(Edit));


        public Edit(string restID)
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelEdit(model);

            var VMIsEdit = "VM_IsEdit";
            var bindingIsEdit = new Binding(VMIsEdit) { Mode = BindingMode.TwoWay };
            this.SetBinding(IsEditProperty, bindingIsEdit);

            IsEdit = true;

            var VMUpdateRest = "VM_RestDetails";
            var bindingUpdateRest = new Binding(VMUpdateRest) { Mode = BindingMode.TwoWay };
            this.SetBinding(UpdateRestProperty, bindingUpdateRest);

            InitializeComponent();

            // Initialize the fileds.
            idRest = restID;
            city = "";
            price = "";
            flagFromPriceSelected = false;
        }

        private void CityChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update city filed to be the selected city.
            city = (sender as ComboBox).SelectedItem as string;
        }

        private void LowPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update price filed to be the selected price.
            ComboBoxItem priceItem = (ComboBoxItem)lowPriceVal.SelectedItem;
            if (priceItem == null)
            {
                lowPriceVal.IsEnabled = true;
                topPriceVal.IsEnabled = false;
            }
            else
            {
                price = priceItem.Content.ToString();

                // Enabled 'To' view.
                topPriceVal.IsEnabled = true;
                if (!flagFromPriceSelected)
                {
                    lowPriceVal.IsEnabled = false;
                    flagFromPriceSelected = true;
                }

                // Update combo item view for fit the low price.
                switch (priceItem.Name[0])
                {
                    case 'a':
                        top1.Visibility = Visibility.Collapsed;
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
                        topPriceVal.IsEnabled = false;
                        break;
                }
            }
        }

        private void TopPriceVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update price filed to be 'the prev price  - the selected price'.
            ComboBoxItem priceItem = (ComboBoxItem)topPriceVal.SelectedItem;
            if (priceItem == null)
            {
                topPriceVal.IsEnabled = false;
            }
            else
            {
                string topPrice = priceItem.Content.ToString();
                price += " - " + topPrice;
                topPriceVal.IsEnabled = false;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Clean choise.
            price = "";
            lowPriceVal.SelectedIndex = -1;
            topPriceVal.SelectedIndex = -1;
            flagFromPriceSelected = false;
            top1.Visibility = Visibility.Visible;
            top2.Visibility = Visibility.Visible;
            top3.Visibility = Visibility.Visible;
            top4.Visibility = Visibility.Visible;
            top5.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // 'rest' is the rest to update.
            Restaurant rest = UpdateRest;

            // Check witch filed was changed.
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

            // Update in the DB the details that was changed.
            UpdateRest = rest;

            // Show the Restaurant Details view. (with the changes)
            IsEdit = false;
            RestaurantDetails rd = new RestaurantDetails(idRest);
            rd.Show();
            this.Close();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Go back - show the Restaurant Details view. (without changes)
            IsEdit = false;
            RestaurantDetails rd = new RestaurantDetails(idRest);
            rd.Show();
            this.Close();
        }

    }
}
