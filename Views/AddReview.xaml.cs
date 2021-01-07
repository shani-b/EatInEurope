using EatInEurope.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for AddReview.xaml
    /// </summary>
    public partial class AddReview : Window
    {
        private string restID;
        private double rating;

        // Properties.
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
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelReview(model);
            
            var VMMyNewReview = "VM_NewReview";
            var bindingMyNewReview = new Binding(VMMyNewReview) { Mode = BindingMode.TwoWay };
            this.SetBinding(MyNewReviewProperty, bindingMyNewReview);

            InitializeComponent();
            restID = idRest;
            rating = 0;
        }

        private void Raiting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem raitingItem = (ComboBoxItem)raitingCombo.SelectedItem;
            string raitingString = raitingItem.Content.ToString();
            // Fill the rating filed with the chosen number.
            rating = Double.Parse(raitingString);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            // Fill current date. 
            string date = DateTime.Now.ToString("dd/MM/yyy");

            // Insert to the DB the new review.
            MyNewReview = new UserReview(restID, reviewText.Text, date, rating);

            // Show the Restaurant Details view (with the new review).
            RestaurantDetails restdetails = new RestaurantDetails(restID);
            restdetails.Show();
            this.Close();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            // Go Back - show the Restaurant Details view. (without add the new review)
            RestaurantDetails restdetails = new RestaurantDetails(restID);
            restdetails.Show();
            this.Close();
        }
    }
}
