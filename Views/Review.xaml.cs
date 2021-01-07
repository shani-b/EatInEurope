using System.Windows.Controls;


namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Review.xaml
    /// </summary>
    public partial class Review : UserControl
    {
        public Review(int i, UserReview iReview)
        {
            // Constructor.
            InitializeComponent();

            // Fill the review with details.
            reviewNum.Content = i.ToString();
            reviewText.Text = iReview.Content;
            date.Content = iReview.Date;
            if (iReview.Rate == 0)
            {
                ratingNum.Content = " --";
            }
            else if (iReview.Rate % 1 == 0)
            {
                ratingNum.Content = " " + iReview.Rate.ToString();
            }
            else
            {
                ratingNum.Content = iReview.Rate.ToString();
            } 
        }
    }
}
