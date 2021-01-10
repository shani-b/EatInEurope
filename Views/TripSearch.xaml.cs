using EatInEurope.ViewModels;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for TripSearch.xaml
    /// </summary>
    public partial class TripSearch : Window
    {
        private List<Category> Categories { get; set; }
        int maxPart;
        string contryResult;


        // Properties.
        public Dictionary<string, int> CountriesPartStyle
        {
            get { return (Dictionary<string, int>)GetValue(CountriesPartStyleProperty); }
            set
            {
                SetValue(CountriesPartStyleProperty, value);
            }
        }

        public static readonly DependencyProperty CountriesPartStyleProperty =
            DependencyProperty.Register("CountriesPartStyle", typeof(Dictionary<string, int>), typeof(TripSearch));

        public List<string> StyleFilter
        {
            get { return (List<string>)GetValue(StyleFilterProperty); }
            set
            {
                SetValue(StyleFilterProperty, value);
            }
        }

        public static readonly DependencyProperty StyleFilterProperty =
            DependencyProperty.Register("StyleFilter", typeof(List<string>), typeof(TripSearch));


        public TripSearch()
        {
            // Constructor.
            IModel model = (DataBaseModel)Application.Current.Properties["model"];
            DataContext = new ViewModelTripSearch(model);

            var VMStyleFilter = "VM_TypesFilter";
            var bindingStyleFilter = new Binding(VMStyleFilter) { Mode = BindingMode.TwoWay };
            this.SetBinding(StyleFilterProperty, bindingStyleFilter);

            InitializeComponent();
            // Hides these objects until the visibility changes.
            resultsView.Visibility = Visibility.Collapsed;
            run.IsEnabled = false;

            // Initialize the fileds.
            maxPart = 0;
            contryResult = "";
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var VMCountriesPartStyle = "VM_CountriesPartStyle";
            var binding = new Binding(VMCountriesPartStyle) { Mode = BindingMode.OneWay };
            this.SetBinding(CountriesPartStyleProperty, binding);

            float pieWidth = 250, pieHeight = 250, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainCanvas.Width = pieWidth;
            mainCanvas.Height = pieHeight;

            Categories = new List<Category>();
            List<SolidColorBrush> colors = new List<SolidColorBrush>
            {
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b3d9ff")), // blue
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff99cc")), // pink
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc99")), // orenge
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99ff99")), // green
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d9b3ff")), // perpul
            };
            int i = 0;
            foreach (KeyValuePair<string, int> entry in CountriesPartStyle)
            {
                Category category = new Category
                {
                    Title = entry.Key,
                    Percentage = entry.Value,
                    ColorBrush = colors[i],
                };
                Categories.Add(category);
                i++;

                // Check witch country is the country with  the biggest Percentage.
                if (entry.Value > maxPart)
                {
                    maxPart = entry.Value;
                    contryResult = entry.Key;
                }

            }

            detailsItemsControl.ItemsSource = Categories;

            // Draw pie
            float angle = 0, prevAngle = 0;
            foreach (var category in Categories)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = category.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = category.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new Size(arcWidth, arcHeight),
                    Point = new Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = category.ColorBrush,
                    Data = pathGeometry,
                };
                mainCanvas.Children.Add(path);

                prevAngle = angle;


                // Draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                mainCanvas.Children.Add(outline1);
                mainCanvas.Children.Add(outline2);
            }

            // Update result view.
            resultsView.Visibility = Visibility.Visible;
            resultVal.Text = contryResult;

            maxPart = 0;
        }

        private void Style_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the chosen style in the model.
            string style = (sender as ComboBox).SelectedItem as string;
            StyleFilter = new List<string> { style };
            run.IsEnabled = true;
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            StyleFilter.Clear();
            // Go Back button - show client option view.
            ClientOptionsView clientOption = new ClientOptionsView();
            clientOption.Show();
            this.Close();
        }
    }
}