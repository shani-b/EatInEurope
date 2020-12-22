using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatInEurope
{
    public class Restaurant
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<string> Types { get; set; }
        public double Rate { get; set; }
        public string PriceRange { get; set; }
        public int NumOfReviews { get; set; }
        public List<string> Reviews { get; set; }
        public string URL { get; set; }

        public Restaurant(string id, string name, string country, string city, List<string> types, double rate, string priceRange,
                            int numOfReviews, List<string> reviews, string url)
        {
            ID = id;
            Name = name;
            Country = country;
            City = city;
            Types = types;
            Rate = rate;
            PriceRange = priceRange;
            NumOfReviews = numOfReviews;
            Reviews = reviews;
            URL = url;
        }
    }
}
