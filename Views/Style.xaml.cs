using System.Windows.Controls;


namespace EatInEurope.Views
{
    /// <summary>
    /// Interaction logic for Style.xaml
    /// </summary>
    public partial class Style : UserControl
    {
        public Style(string styleName)
        {
            // Constructor.
            InitializeComponent();

            // Fiil the style with specific value.
            style.Content = styleName;
        }
    }
}
