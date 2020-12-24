using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Windows;

namespace EatInEurope
{
    class DataBaseModel : IModel
    {
        DBConnect restaurants;

        public DataBaseModel(DBConnect dbConnect)
        {
            restaurants = dbConnect;

            CitiesOptions = new List<string> {
                "Amsterdam", "Athens","Barcelona", "Berlin","Bratislava","Brussels","Budapest",
                "Copenhagen","Dublin","Edinburgh","Geneva","Hamburg","Helsinki","Krakow","Lisbon","Ljubljana",
                "London","Luxembourg","Lyon","Madrid","Milan","Munich","Oporto","Oslo","Paris","Prague","Rome",
                "Stockholm","Vienna","Warsaw","Zurich"
            };
            CountriesOptions = new List<string> {
                "Netherlands", "England","Swiss", "France","Germany"
            };
            TypesOptions = new List<string> {
                "Chinese", "Indian","vegeterian", "vegan","Italian"
            };

            RestsResults = new List<Restaurant>
            {
                //new List<string> {"Martine of Martine's Table","Amsterdam", "French", "Dutch", "European"
                  //  , "5", "$$ - $$$", "136", "Just like home", "A Warm Welcome to Wintry Amsterdam", "/Restaurant_Review-g188590-d11752080-Reviews-Martine_of_Martine_s_Table-Amsterdam_North_Holland_Province.html" },
//                 new List<string> {"De Silveren Spiegel" ,"Amsterdam","Dutch", "European", "Vegetarian Friendly",
//                     "4.5","$$$$", "812", "Great food and staff", "just perfect","/Restaurant_Review-g188590-d693419-Reviews-De_Silveren_Spiegel-Amsterdam_North_Holland_Province.html" },
//                  new List<string> {"La Rive" ,"Amsterdam","Mediterranean", "French", "International", 
//                     "4.5","$$$$", "567", "Satisfaction", "Delicious old school restaurant","/Restaurant_Review-g188590-d696959-Reviews-La_Rive-Amsterdam_North_Holland_Province.html"}

                new Restaurant("d11752080","Martine of Martine's Table","Netherlands", "Amsterdam",new List<string>{ "French", "Dutch", "European" }
                    , 5, "$$ - $$$", 136,new List<string>{ "Just like home", "A Warm Welcome to Wintry Amsterdam" },
                    "/Restaurant_Review-g188590-d11752080-Reviews-Martine_of_Martine_s_Table-Amsterdam_North_Holland_Province.html"),
                new Restaurant("d693419", "De Silveren Spiegel","Netherlands", "Amsterdam",new List<string>{ "Dutch", "European", "Vegetarian Friendly", "Gluten Free Options" }
                    , 4.5, "$$$$", 812,new List<string>{ "Great food and staff", "just perfect" },
                    "/Restaurant_Review-g188590-d693419-Reviews-De_Silveren_Spiegel-Amsterdam_North_Holland_Province.html"),
                new Restaurant("d696959", "La Rive","Netherlands", "Amsterdam",new List<string>{ "Mediterranean", "French", "International", "European", "Vegetarian Friendly", "Vegan Options" }
                    , 4.5, "$$$$", 567,new List<string>{ "Satisfaction", "Delicious old school restaurant"},
                    "/Restaurant_Review-g188590-d696959-Reviews-La_Rive-Amsterdam_North_Holland_Province.html")
            };
            //RestsResults = new List<List<string>> { };


        }

        // Message to user property.
        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                NotifyPropertyChanged("Message");
            }
        }

        private string username;
        public string UserName { 
            get { return username; }
            set { 
                username = value;
                NotifyPropertyChanged("username");
            } 
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("password");
                signIn(username, password);
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                NotifyPropertyChanged("newPassword");
                register(username, newPassword);
            }
        }

        private string[] top5Rests = new string[5];
        public string[] Top5Rests { 
            get { return top5Rests; }
            set {
                top5Rests = value;
                NotifyPropertyChanged("top5Rests");
            } 
        }

        private List<string> countriesFilter = new List<string>();
        public List<string> CountriesFilter {
            get { return countriesFilter; }
            set {
                countriesFilter = value;
                NotifyPropertyChanged("countriesFilter");
            } 
        }

        private List<string> citiesFilter = new List<string>();
        public List<string> CitiesFilter {
            get { return citiesFilter; }
            set
            {
                citiesFilter = value;
                NotifyPropertyChanged("citiesFilter");
            }
        }

        private List<string> typesFilter = new List<string>();
        public List<string> TypesFilter {
            get { return typesFilter; }
            set
            {
                typesFilter = value;
                NotifyPropertyChanged("typesFilter");
            }
        }

        private List<string> countriesOptions = new List<string>();
        public List<string> CountriesOptions
        {
            get { return countriesOptions; }
            set
            {
                countriesOptions = value;
                NotifyPropertyChanged("countriesOptions");
            }
        }

        private List<string> citiesOptions = new List<string>();
        public List<string> CitiesOptions
        {
            get { return citiesOptions; }
            set
            {
                citiesOptions = value;
                NotifyPropertyChanged("citiesOptions");
            }
        }

        private List<string> typesOptions = new List<string>();
        public List<string> TypesOptions
        {
            get { return typesOptions; }
            set
            {
                typesOptions = value;
                NotifyPropertyChanged("typesOptions");
            }
        }

        private double[] ratesFilter = new double[2];
        public double[] RateFilter {
            get { return ratesFilter; }
            set
            {
                ratesFilter = value;
                NotifyPropertyChanged("ratesFilter");
            }
        }

        private int[] priceFilter = new int[2];
        public int[] PriceFilter {
            get { return priceFilter; }
            set
            {
                priceFilter = value;
                NotifyPropertyChanged("priceFilter");
            }
        }

        private List<Restaurant> restsResults = new List<Restaurant>();
        public List<Restaurant> RestsResults
        {
            get { return restsResults; }
            set
            {
                restsResults = value;
                NotifyPropertyChanged("restsResults");
            }
        }
        private Dictionary<string, int> countriesPartStyle = new Dictionary<string, int>();
        public Dictionary<string, int> CountriesPartStyle
        {
            get
            {
                return graphCountriesByType("vegiterian");
            }
        }

        public void send(string message)
        {
/*            // Push the messege to the queue.
            queCommand.Enqueue(message);*/
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void start()
        {
        }

        public void NotifyPropertyChanged(string PropName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropName));
            }
        }

        public bool register(string username, string password)
        {
            List<string> values = new List<string> { username, password };
            // restaurants.Insert("('" + username + "','" + password +"')", "t_owners");
            UserName = username;
            return true;
        }

        public bool signIn(string username, string password)
        {
/*             List<string> result = restaurants.Check_existing(username, password, "t_owners");
            if (result[0].Equals("0")) {
                return false;
            }

            UserName = username;*/
            return true;
        }

        public List<Restaurant> orderBy(string orderType, string order)
        {
            //RestsResults = restaurants.Select("t_restaurants", null, orderType, order);
            return restsResults;
        }

        public List<string> restDetails(string rest)
        {
            List<string> details = null;
            // Select(string table, string whereCond, string orderByValue, string order)
            /*if (restsResults.Count != 0)
            {
                details = restsResults.Find(x => x.Name == rest);
            }
            else
            {
                // details = restaurants.Select("t_restaurants", "Name=" + rest, null, null)[0];
            }*/
            return details;
        }

        public void addReview(int rate, string body)
        {
            List<string> values = new List<string> { rate.ToString(), body };
            // restaurants.Insert("('" + rate + "','" + body + "')", "t_reviews");
        }

        public void addRest(string name, string country, string city, List<string> types)
        {
            List<string> values = new List<string> { name, country, city};
            foreach (string type in types)
            {
                values.Add(type);
            }

            string valuesString = "('" + name + "','" + country + "','" + city;
            foreach (string type in types)
            {
                valuesString += "','" + type;
            }
            // restaurants.Insert(valuesString + "')", "t_reviews");
        }

        public Dictionary<string, int> graphCountriesByType(string type)
        {
            // quary count all rests with this type = total
            // count for each country the rests with this type (group by) = each
            // divide = total/each

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("france", 5);
            dic.Add("germany", 22);
            dic.Add("Netherlands", 27);
            dic.Add("England", 46);

            return dic;
        }
    }
}
