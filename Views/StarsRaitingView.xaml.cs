using EatInEurope.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for StarsRaitingView.xaml
    /// </summary>
    public partial class StarsRaitingView : UserControl
    {
        // Property.
        public double RestRating
        {
            get { return (double)GetValue(RestRatingProperty); }
            set
            {
                SetValue(RestRatingProperty, value);
            }
        }

        public static readonly DependencyProperty RestRatingProperty =
            DependencyProperty.Register("RestRating", typeof(double), typeof(StarsRaitingView));


        public StarsRaitingView()
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelRestaurantDetails(model);

            var VMRestRating = "VM_RestRating";
            var bindingRestRating = new Binding(VMRestRating) { Mode = BindingMode.OneWay };
            this.SetBinding(RestRatingProperty, bindingRestRating);

            InitializeComponent();

            // Hides these objects until the visibility changes.
            stat1E.Visibility = Visibility.Collapsed;
            stat2E.Visibility = Visibility.Collapsed;
            stat3E.Visibility = Visibility.Collapsed;
            stat4E.Visibility = Visibility.Collapsed;
            stat5E.Visibility = Visibility.Collapsed;

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

            // Get current rest raiting.
            double raiting = RestRating;
            int rate = (int)raiting;

            // Fill the view with full stars according to rate number.
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

            // Fill the view with empty stars according to rate number.
            switch (rate)
            {
                case 4:
                    stat5E.Visibility = Visibility.Visible;
                    break;
                case 3:
                    stat4E.Visibility = Visibility.Visible;
                    stat5E.Visibility = Visibility.Visible;
                    break;
                case 2:
                    stat3E.Visibility = Visibility.Visible;
                    stat4E.Visibility = Visibility.Visible;
                    stat5E.Visibility = Visibility.Visible;
                    break;
                case 1:
                    stat2E.Visibility = Visibility.Visible;
                    stat3E.Visibility = Visibility.Visible;
                    stat4E.Visibility = Visibility.Visible;
                    stat5E.Visibility = Visibility.Visible;
                    break;
                case 0:
                    stat1E.Visibility = Visibility.Visible;
                    stat2E.Visibility = Visibility.Visible;
                    stat3E.Visibility = Visibility.Visible;
                    stat4E.Visibility = Visibility.Visible;
                    stat5E.Visibility = Visibility.Visible;
                    break;
            }

            // Checks if the raiting number is fraction.
            if (raiting % 1 != 0)
            {
                // Fill the view with half stars according to rate number.
                switch (rate)
                {
                    case 4:
                        stat5H.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        stat4H.Visibility = Visibility.Visible;
                        stat5E.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        stat3H.Visibility = Visibility.Visible;
                        stat4E.Visibility = Visibility.Visible;
                        stat5E.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        stat2H.Visibility = Visibility.Visible;
                        stat3E.Visibility = Visibility.Visible;
                        stat4E.Visibility = Visibility.Visible;
                        stat5E.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        stat1H.Visibility = Visibility.Visible;
                        stat2E.Visibility = Visibility.Visible;
                        stat3E.Visibility = Visibility.Visible;
                        stat4E.Visibility = Visibility.Visible;
                        stat5E.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
    }
}
