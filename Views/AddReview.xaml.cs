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
            // Hides these objects until the visibility changes.
            errorText.Visibility = Visibility.Collapsed;
            errorReview.Visibility = Visibility.Collapsed;
            errorRate.Visibility = Visibility.Collapsed;

            // Initialize the fileds.
            restID = idRest;
            rating = 0;
        }

        private void Raiting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem raitingItem = (ComboBoxItem)raitingCombo.SelectedItem;
            string raitingString = raitingItem.Content.ToString();
            // Fill the rating filed with the chosen number.
            rating = Double.Parse(raitingString);
            errorRate.Visibility = Visibility.Collapsed;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            // Fill current date. 
            string date = DateTime.Now.ToString("MM/dd/yyy");

            // Update visbility.
            if (!reviewText.Equals(""))
            {
                errorReview.Visibility = Visibility.Collapsed;
            }

            // Checks if all mandatory details have been entered.
            if (reviewText.Text.Equals("") || rating == 0)
            {
                string errorMessage = " You must enter ";
                if (reviewText.Text.Equals(""))
                {
                    errorReview.Visibility = Visibility.Visible;
                    errorMessage += "review, ";
                }
                if (rating == 0)
                {
                    errorRate.Visibility = Visibility.Visible;
                    errorMessage += "rating, ";
                }
                errorText.Visibility = Visibility.Visible;
                errorText.Text = errorMessage;
            }
            else
            {
                // Insert to the DB the new review.
                MyNewReview = new UserReview(restID, reviewText.Text, date, rating);

                // Show the Restaurant Details view (with the new review).
                RestaurantDetails restdetails = new RestaurantDetails(restID);
                restdetails.Show();
                this.Close();
            } 
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
