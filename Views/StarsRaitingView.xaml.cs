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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for StarsRaitingView.xaml
    /// </summary>
    public partial class StarsRaitingView : UserControl
    {
        public Restaurant RestDetails
        {
            get { return (Restaurant)GetValue(RestDetailsProperty); }
            set
            {
                SetValue(RestDetailsProperty, value);
            }
        }

        public static readonly DependencyProperty RestDetailsProperty =
            DependencyProperty.Register("RestDetails", typeof(Restaurant), typeof(StarsRaitingView));


        public StarsRaitingView()
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantDetails(model);

            var VMRestDetails = "VM_RestDetails";
            var bindingRestDetails = new Binding(VMRestDetails) { Mode = BindingMode.OneWay };
            this.SetBinding(RestDetailsProperty, bindingRestDetails);

            InitializeComponent();

            stat1C.Visibility = Visibility.Collapsed;
            stat2C.Visibility = Visibility.Collapsed;
            stat3C.Visibility = Visibility.Collapsed;
            stat4C.Visibility = Visibility.Collapsed;
            stat5C.Visibility = Visibility.Collapsed;

            stat1H.Visibility = Visibility.Collapsed;
            stat2H.Visibility = Visibility.Collapsed;
            stat3H.Visibility = Visibility.Collapsed;
            stat4H.Visibility = Visibility.Collapsed;
            stat5H.Visibility = Visibility.Collapsed;

            stat1F.Visibility = Visibility.Collapsed;
            stat2F.Visibility = Visibility.Collapsed;
            stat3F.Visibility = Visibility.Collapsed;
            stat4F.Visibility = Visibility.Collapsed;
            stat5F.Visibility = Visibility.Collapsed;

            double raiting = RestDetails.Rate;
            int rate = (int)raiting;
            // int
            switch (rate)
            {
                case 1:
                    stat1F.Visibility = Visibility.Visible;
                    break;
                case 2:
                    stat1F.Visibility = Visibility.Visible;
                    stat2F.Visibility = Visibility.Visible;
                    break;
                case 3:
                    stat1F.Visibility = Visibility.Visible;
                    stat2F.Visibility = Visibility.Visible;
                    stat3F.Visibility = Visibility.Visible;
                    break;
                case 4:
                    stat1F.Visibility = Visibility.Visible;
                    stat2F.Visibility = Visibility.Visible;
                    stat3F.Visibility = Visibility.Visible;
                    stat4F.Visibility = Visibility.Visible;
                    break;
                case 5:
                    stat1F.Visibility = Visibility.Visible;
                    stat2F.Visibility = Visibility.Visible;
                    stat3F.Visibility = Visibility.Visible;
                    stat4F.Visibility = Visibility.Visible;
                    stat5F.Visibility = Visibility.Visible;
                    break;
            }

            switch (rate)
            {
                case 4:
                    stat5C.Visibility = Visibility.Visible;
                    break;
                case 3:
                    stat4C.Visibility = Visibility.Visible;
                    stat5C.Visibility = Visibility.Visible;
                    break;
                case 2:
                    stat3C.Visibility = Visibility.Visible;
                    stat4C.Visibility = Visibility.Visible;
                    stat5C.Visibility = Visibility.Visible;
                    break;
                case 1:
                    stat2C.Visibility = Visibility.Visible;
                    stat3C.Visibility = Visibility.Visible;
                    stat4C.Visibility = Visibility.Visible;
                    stat5C.Visibility = Visibility.Visible;
                    break;
                case 0:
                    stat1C.Visibility = Visibility.Visible;
                    stat2C.Visibility = Visibility.Visible;
                    stat3C.Visibility = Visibility.Visible;
                    stat4C.Visibility = Visibility.Visible;
                    stat5C.Visibility = Visibility.Visible;
                    break;
            }

            if (raiting % 1 != 0)
            {
                // double
                switch (rate)
                {
                    case 4:
                        stat5H.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        stat4H.Visibility = Visibility.Visible;
                        stat5C.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        stat3H.Visibility = Visibility.Visible;
                        stat4C.Visibility = Visibility.Visible;
                        stat5C.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        stat2H.Visibility = Visibility.Visible;
                        stat3C.Visibility = Visibility.Visible;
                        stat4C.Visibility = Visibility.Visible;
                        stat5C.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        stat1H.Visibility = Visibility.Visible;
                        stat2C.Visibility = Visibility.Visible;
                        stat3C.Visibility = Visibility.Visible;
                        stat4C.Visibility = Visibility.Visible;
                        stat5C.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
    }
}
