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
    /// Interaction logic for AddReview.xaml
    /// </summary>
    public partial class AddReview : Window
    {
        private string RestID;
        private double raiting;

        public UserReview MyNewReview
        {
            get { return (UserReview)GetValue(MyNewReviewProperty); }
            set
            {
                SetValue(MyNewReviewProperty, value);
            }
        }

        public static readonly DependencyProperty MyNewReviewProperty =
            DependencyProperty.Register("MyNewReview", typeof(UserReview), typeof(AddReview));

        public AddReview(string idRest)
        {
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelReview(model);
            
            var VMMyNewReview = "VM_NewReview";
            var bindingMyNewReview = new Binding(VMMyNewReview) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyNewReviewProperty, bindingMyNewReview);

            InitializeComponent();
            RestID = idRest;
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyy");
            MyNewReview = new UserReview(RestID, reviewText.Text, date, raiting);
  
            RestaurantDetails rd = new RestaurantDetails(RestID, true);
            rd.Show();
            this.Close();
        }

        private void raiting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem raitingItem = (ComboBoxItem)raitingCombo.SelectedItem;
            string raitingString = raitingItem.Content.ToString();
            raiting = Double.Parse(raitingString);
        }
    }
}
