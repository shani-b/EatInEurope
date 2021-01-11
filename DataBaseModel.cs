using EatInEurope.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace EatInEurope
{
    class DataBaseModel : IModel
    {
        DBConnect dBConnect;

        public DataBaseModel(DBConnect dbConnect)
        {
            dBConnect = dbConnect;
            CountriesOptions = getCountries();
            if (CountriesOptions == null)
                raiseError("getCountries");
            TypesOptions = getStyles();
            if (TypesOptions == null)
                raiseError("getStyles");

           /* CountriesOptions = new List<string> {
                "Netherlands", "England","Swiss", "France","Germany"
            };
*/
            /* CitiesOptions = new List<string> {
                 "Amsterdam", "Athens","Barcelona", "Berlin","Bratislava","Brussels","Budapest",
                 "Copenhagen","Dublin","Edinburgh","Geneva","Hamburg","Helsinki","Krakow","Lisbon","Ljubljana",
                 "London","Luxembourg","Lyon","Madrid","Milan","Munich","Oporto","Oslo","Paris","Prague","Rome",
                 "Stockholm","Vienna","Warsaw","Zurich"
             };
             CountriesOptions = getCountries();
             TypesOptions = new List<string> {
                 "Chinese", "Indian","vegeterian", "vegan","Italian"
             };*/

            // RestsResults = new List<Restaurant>
            //{
            //new List<string> {"Martine of Martine's Table","Amsterdam", "French", "Dutch", "European"
            //    , "5", "$$ - $$$", "136", "Just like home", "A Warm Welcome to Wintry Amsterdam", "/Restaurant_Review-g188590-d11752080-Reviews-Martine_of_Martine_s_Table-Amsterdam_North_Holland_Province.html" },
            // new List<string> {"De Silveren Spiegel" ,"Amsterdam","Dutch", "European", "Vegetarian Friendly",
            //     "4.5","$$$$", "812", "Great food and staff", "just perfect","/Restaurant_Review-g188590-d693419-Reviews-De_Silveren_Spiegel-Amsterdam_North_Holland_Province.html" },
            //  new List<string> {"La Rive" ,"Amsterdam","Mediterranean", "French", "International", 
            //     "4.5","$$$$", "567", "Satisfaction", "Delicious old school restaurant","/Restaurant_Review-g188590-d696959-Reviews-La_Rive-Amsterdam_North_Holland_Province.html"}
            /*
                            new Restaurant("d11752080","Martine of Martine's Table","Netherlands", "Amsterdam",new List<string>{ "French", "Dutch", "European" }
                                , 5, "$$ - $$$", 136,new List<UserReview>{new UserReview("d11752080","gooooooooooooooood", "10/1/99", 0.0), new UserReview("d11752080","very recomend rest", "20/01/19", 2.5), new UserReview("d11752080","I LIKE THIS REST", "29/08/2000", 5) },
                                "/Restaurant_Review-g188590-d11752080-Reviews-Martine_of_Martine_s_Table-Amsterdam_North_Holland_Province.html", "Orel"),
                            new Restaurant("d693419", "De Silveren Spiegel","Netherlands", "Amsterdam",new List<string>{ "Dutch", "European", "Vegetarian Friendly", "Gluten Free Options" }
                                , 4.5, "$$$$", 812,new List<UserReview>(),
                                "/Restaurant_Review-g188590-d693419-Reviews-De_Silveren_Spiegel-Amsterdam_North_Holland_Province.html", "shani"),
                            new Restaurant("d696959", "La Rive","Netherlands", "Amsterdam",new List<string>{ "Mediterranean", "French", "International", "European", "Vegetarian Friendly", "Vegan Options" }
                                , 1.5, "$$$$", 567,new List<UserReview>(),
                                "/Restaurant_Review-g188590-d696959-Reviews-La_Rive-Amsterdam_North_Holland_Province.html", "reut")
                        };*/

            //RestsResults = new List<List<string>> { };


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string PropName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropName));
            }
        }




        // properties 
        private bool endOfRests;
        public bool EndOfRests {
            set {
                endOfRests = value;
                NotifyPropertyChanged("endOfRests");
            }
            get
            {
                return endOfRests;
            }
        }

        private bool loadMoreRests = true;
        public bool LoadMoreRests
        {
            set
            {
                loadMoreRests = value;
                NotifyPropertyChanged("loadMoreRests");
                queryContinue = true;
                List<Restaurant> result = getRestByFilter();
                if (result == null)
                    raiseError("getRestByFilter");
                else
                {
                    RestsResults.AddRange(result);
                    queryContinue = false;
                }
                queryContinue = false;
            }
            get
            {
                return loadMoreRests;
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
                //LoginOK = true;
                RestsResults.Clear();
                if (signIn(username, password))
                {
                    LoginOK = true;
                    //get relevent rest
                    RestsResults = getRestByFilter();
                    if (RestsResults == null)
                        raiseError("getRestByFilter");
                }
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
                RestsResults.Clear();

                UsernameFree = true;
                if (!signIn(username, null))
                {
                    if (register(username, newPassword) == false)
                        raiseError("register");
                }
                else
                {
                    UsernameFree = false;
                }
            }
                            
        }

        private bool isClient = false;
        public bool IsClient {
            set
            {
                isClient = value;
                NotifyPropertyChanged("isClient");

                if (isClient)
                {
                    CountriesOptions = getCountries();
                    if (CountriesOptions == null)
                        raiseError("getCountries");
                    TypesOptions = getStyles();
                    if (TypesOptions == null)
                        raiseError("getStyles");
                }
            } 
            get {
                return isClient;

            } 
        }
        
        private bool isEdit = false; 
        public bool IsEdit {
            get
            {
                return isEdit;
            }
            set
            {
                isEdit = value;
                if (isEdit)
                {
                    CitiesOptions = getAllCities();
                    if (CitiesOptions == null)
                        raiseError("getAllCities");
                }
                NotifyPropertyChanged("isEdit");
            }
        }

        private bool loginOK = false;
        public bool LoginOK { 
            get { return loginOK; }
            set {
                loginOK = value;
                NotifyPropertyChanged("loginOK");
            }
        }
        
        private bool usernameFree = false;
        public bool UsernameFree {
            get {
                // TODO: update flag by calling function checks username
                
                return usernameFree;
            }
            set
            {
                usernameFree = value;
                NotifyPropertyChanged("usernameFree");
            }
        }

        private string[] top5RestValue = null;

        private string[] top5Rests = new string[5];
        public string[] Top5Rests { 
            get { return top5Rests; }
            set {
                top5Rests = value;
                NotifyPropertyChanged("top5Rests");
            } 
        }

        private string countryFilter;
        public string CountryFilter {
            get { return countryFilter; }
            set {
                countryFilter = value;
                NotifyPropertyChanged("countryFilter");
                CitiesOptions = getCities();
                if (CitiesOptions == null)
                    raiseError("getCities");
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
        private bool startSearch;
        public bool StartSearch {
            set {
                startSearch = value;
                NotifyPropertyChanged("startSearch");
                if (startSearch)
                {
                    RestsResults = getRestByFilter();
                    if (RestsResults == null)
                        raiseError("getRestByFilter");
                }

            }
            get
            {
                return startSearch;
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

        private List<double> ratesFilter = new List<double>();
        public List<double> RateFilter {
            get { return ratesFilter; }
            set
            {
                ratesFilter = value;
                NotifyPropertyChanged("ratesFilter");
            }
        }

        private List<string> priceFilter = new List<string>();
        public List<string> PriceFilter {
            get { return priceFilter; }
            set
            {
                priceFilter = value;
                NotifyPropertyChanged("priceFilter");
            }
        }

        private string order;
        public string Order { 
            get { return order; }
            set
            {
                order = value;
                NotifyPropertyChanged("order");
            }
        }
       
        private bool asc;
        public bool Asc { 
            get { return asc; }
            set
            {
                asc = value;
                NotifyPropertyChanged("asc");
                RestsResults = getRestByFilter();
                if (RestsResults == null)
                    raiseError("getRestByFilter");
            }
        }
        
        private string restID;
        public string RestID {
            get { return restID; }
            set
            {
                // Delete this restId from the DB
                restID = value;
                NotifyPropertyChanged("restID");
            } 
        }
        
        private string restName;
        public string RestName { 
            get { return restName; }
            set
            {
                restName = value;
                NotifyPropertyChanged("restName");
                if (restName != null)
                {
                    RestsResults = getRestByFilter();
                    if (RestsResults == null)
                        raiseError("getRestByFilter");
                }
            }
        }
        
        private double restRating;
        public double RestRating { 
            get
            {
                restRating = RestsResults[restsResults.FindIndex(x => x.ID == RestID)].Rate;
                return restRating;
            }
        }
        public Restaurant RestDetails
        {
            get 
            {
                if (restID != null)
                {
                    if (!IsEdit)
                    {
                        Restaurant rest = restDetails();
                        if (rest == null)
                        {
                            raiseError("restDetails");
                            return null;
                        }
                        return rest;
                    }
                    return RestsResults[restsResults.FindIndex(x => x.ID == restID)];
                } else
                {
                    return null;
                }
                
            }
            set
            {
                RestsResults[restsResults.FindIndex(x => x.ID == restID)] = value;
                bool result = updateRestaurant(value);
                if (result == false)
                    raiseError("updateRestaurant");
                NotifyPropertyChanged("RestDetails");
            }
        }

        private string idToRemove;
        public string IDToRemove {
            get
            {
                return idToRemove;
            }
            set
            {
                idToRemove = value;
                NotifyPropertyChanged("idToRemove");
                if(!deleteRest())
                {
                    raiseError("deleteRest");
                }
            }
        }

        private UserReview newReview;
        public UserReview NewReview { 
            get { return newReview; }
            set
            {
                newReview = value;
                RestsResults.Find(x => x.ID == restID).Reviews.Add(value);
                NotifyPropertyChanged("newReview");
                bool result = addReview(newReview);
                if (result == false)
                    raiseError("addReview");
            }
        }
        
        private bool isAddRest;
        public bool IsAddRest { 
            get {
                return isAddRest;
            }
            set {
                isAddRest = value;
                NotifyPropertyChanged("isAddRest");
            }
        }

        private List<Restaurant> restsResults = new List<Restaurant>();
        public List<Restaurant> RestsResults
        {
            get { return restsResults; }
            set
            {
                if (IsAddRest)
                {
                    bool result = addRest(restsResults[restsResults.Count - 1]);
                    if (result == false)
                        raiseError("addRest");
                    IsAddRest = false;
                }
                restsResults = value;
                NotifyPropertyChanged("restsResults");
            }
        }
       
        private Dictionary<string, int> countriesPartStyle = new Dictionary<string, int>();
        public Dictionary<string, int> CountriesPartStyle
        {
            get
            {
                Dictionary<string, int> dic = graphCountriesByType("VeganOptions");
                if (dic != null)
                    return dic;
                raiseError("CountriesPartStyle");
                return null;
            }
        }

        bool queryContinue = false;

        //TODO update endReading

        // functions
        private List<string> calculatePriceRange()
        {

            List<string> priceRange = new List<string>();
            if (PriceFilter[0] == "$" && PriceFilter[1] == "$$$$$")
                return priceRange;

            int priceFrom = 0;
            int priceto = 0;

            if (PriceFilter[0] == "$")
                priceFrom = 1;
            else if (PriceFilter[0] == "$$")
                priceFrom = 2;
            else if (PriceFilter[0] == "$$$")
                priceFrom = 3;
            else if (PriceFilter[0] == "$$$$")
                priceFrom = 4;
            else
                priceFrom = 5;

            if (PriceFilter[1] == "$")
                priceto = 1;
            else if (PriceFilter[1] == "$$")
                priceto = 2;
            else if (PriceFilter[1] == "$$$")
                priceto = 3;
            else if (PriceFilter[1] == "$$$$")
                priceto = 4;
            else
                priceto = 5;
             
            List<List<int>> tmp = new List<List<int>>();

            for (int i = priceto; i >= priceFrom; i--)
            {
                List<int> range = new List<int>();
                range.Add(i);
                range.Add(-1);
                tmp.Add(range);

                for (int j = i-1; j>= priceFrom; j--)
                {
                    List<int> range1 = new List<int>();
                    range1.Add(j);
                    range1.Add(i);
                    tmp.Add(range1);
                }
            }

            
            foreach(List<int> range in tmp)
            {
                if (range[1] != -1)
                    priceRange.Add(priceToStr(range[0]) + " - " + priceToStr(range[1]));
                else
                    priceRange.Add(priceToStr(range[0]));
            }
            return priceRange;
        }

        private string priceToStr(int price)
        {
            switch (price)
            {
                case 1:
                    return "$";
                case 2:
                    return "$$";
                case 3:
                    return "$$$";
                case 4:
                    return "$$$$";
                case 5:
                    return "$$$$$";
                default://-
                    return null;

            }

        }

        private double calculateRate(double value)
        {
            return (Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2);
        }

        public void raiseError(string error)
        {
            string error1 = "An error occurred while connecting to the database";
            MessageBox.Show(error1);

        }


        // create data base queries:
        public bool deleteRest()
        {
            //TODO CHANGE ID
            return dBConnect.Delete("t_restaurants", "ID_TA=" +IDToRemove );
        }
        
        public List<Restaurant> getTop5Rest()
        {
            //select * from t_restaurants where Price_Range LIKE '$' order by Rating LIMIT 5;
            top5RestValue[0] = " Price_Range LIKE '$' And ";
            top5RestValue[1] = " order by Rating LIMIT 5";
            List<Restaurant> top5 = getRestByFilter();
            if (top5 == null)
                return null;
            top5RestValue[0] = null;
            top5RestValue[1] = null;
            return top5;
        }
        
        public bool register(string username, string password)
        {
            // check if not exist:
/*            List<string> result = dBConnect.Check_existing(username, password, "t_owners");
            if (result == null)
                throw new Exception("error from db");
            if (result[0].Equals("0"))
            {
                return false;
            }*/
            List<string> values = new List<string> { username, password };
            bool result = dBConnect.Insert("('" + username + "','" + password +"')", "t_owners");
            if (result == false)
                return false;
            UserName = username;
            return true;
        }

        public List<Restaurant> getRestByFilter()
        {

            // create sql query
            string select = "t_restaurants.ID_TA, t_restaurants.name, t_City.Name as city,  t_country.Name as country, t_restaurants.rating, t_restaurants.owner";
            string from = "t_restaurants join t_city on t_restaurants.city_id = t_city.id join t_country on t_city.countrycode = t_country.code";
            string where;

            if (isClient == true)
            {
                if (RestName == null)
                {
                    where = "t_restaurants.city_id = (select t_city.id from t_city where t_city.name = '" + CitiesFilter[0] + "')";

                    if (TypesFilter.Count != 0)
                    {
                        if (TypesFilter[0] != null)
                        {
                            where += " And t_restaurants.ID_TA IN (select ID_TA from t_style_rest join t_style on t_style_rest.styleid = t_style.id where t_style.style='" + TypesFilter[0] + "'";
                            for (int i = 1; i < TypesFilter.Count; i++)
                            {
                                where += " OR t_style.style='" + TypesFilter[i] + "'";
                            }
                            where += ")";

                        }
                    }

                    List<string> ranges = calculatePriceRange();
                    if (ranges.Count != 0)
                    {

                        where += " And (t_restaurants.Price_Range = '" + ranges[0] + "'";
                        for (int i = 1; i < ranges.Count; i++)
                        {
                            where += " OR Price_Range = '" + ranges[i] + "'";
                        }
                        where += ")";
                    }

                    if (RateFilter.Count != 0)
                    {
                        where += " And t_restaurants.rating Between " + RateFilter[0].ToString() + " AND " + RateFilter[1].ToString();
                    }
                }
                else
                {
                    where = " t_restaurants.Name LIKE '%" + RestName + "%'";
                }
            }
            else
            {
                where = "t_restaurants.owner = '" + UserName + "'";
            }
            
            /*if (top5RestValue[0] != null)
            {
                //add filter of 5 top rest
                where = top5RestValue[0] + where + top5RestValue[1];
            }*/

            string orderByValue = null;
            string order = null;
            
            if (Order != null)
            {
                orderByValue = Order;
                if (Asc == true)
                    order = "Asc";
                else
                    order = "Desc";
            }

            bool endReading = false;
            List<Restaurant> all_rest = new List<Restaurant>();
            List<string>[] rest = dBConnect.SelectRest(from, where , orderByValue, order, select,10, queryContinue, ref endReading);
            if (endReading == true)
                EndOfRests = true;
            else
                EndOfRests = false;
            if (rest == null)
                return null;
            
            select = "t_restaurants.ID_TA, t_style.style";
            from = "t_restaurants join t_style_rest on  t_restaurants.ID_TA = t_style_rest.ID_TA join t_style on t_style.id = t_style_rest.styleid";
            
            for (int i = 0; i < rest[0].Count ;i++)
            {
                Restaurant new_rest = new Restaurant(rest[0][i], rest[1][i], rest[4][i], rest[2][i], null, calculateRate(Convert.ToDouble(rest[3][i])),null, -1 ,null, null, rest[5][i]);
                // condition for specific id -restaurant-styles
                string id = rest[0][i];
                where = " t_restaurants.ID_TA='" + id + "'";
                List<string>[] dbStyles = dBConnect.Select(from, where, null, null, select,1);
                if (dbStyles == null)
                    return null;
                List<string> styles = new List<string>();
                if (dbStyles[9].Count != 0)
                {
                    styles.Add(dbStyles[9][0]);
                    new_rest.Types = styles;
                }
                all_rest.Add(new_rest);
            }
            return all_rest;
        }

        public bool signIn(string username, string password)
        {
            List<string> result = dBConnect.Check_existing(username, password, "t_owners");
            if (result == null) {
                raiseError("signIn");
                return false;
            }
            if (result[0].Equals("0")) {
                return false;
            }

            UserName = username;
            return true;
        }

        public Restaurant restDetails()
        {
            Restaurant currRest = RestsResults[restsResults.FindIndex(x => x.ID == RestID)];
            // create sql query
            string select = "t_restaurants.price_range, t_restaurants.Numbers_of_reviews, t_restaurants.url_TA ";
            string from = "t_restaurants";
            string where = "t_restaurants.ID_TA = " + currRest.ID;
            
            List<string>[] rest = dBConnect.Select(from, where, null, null, select,-1);
            if (rest == null)
                return null;
            select = "t_restaurants.ID_TA, t_style.style";
            from = "t_restaurants inner join t_style_rest on  t_restaurants.ID_TA = t_style_rest.ID_TA inner join t_style on t_style.id = t_style_rest.styleid";
            for (int i = 0; i < rest[0].Count; i++)
            {
                
                currRest.PriceRange = rest[4][i];
                currRest.NumOfReviews = Convert.ToInt32(rest[7][i]);
                currRest.URL = rest[5][i];
 
                // condition for specific id -restaurant-styles
                ;
                where = " t_restaurants.ID_TA='" + currRest.ID + "' ";
                List<string>[] dbStyles = dBConnect.Select(from, where, null, null, select,3);
                if (dbStyles == null)
                    return null;
                List<string> styles = new List<string>();
                for (int j = 0; j < dbStyles[0].Count; j++)
                {
                    styles.Add(dbStyles[9][j]);
                }
                // get -restaurant -reviews
                List<string>[] dbReviews = dBConnect.Select("t_reviews", "ID_TA= " + currRest.ID , null, null, null,-1);
                if (dbReviews == null)
                    return null;
                List<UserReview> listReviews = new List<UserReview>();
                for (int j = 0; j < dbReviews[0].Count; j++)
                {
                    double rate;
                    if (Convert.ToDouble(dbReviews[12][j]) == -1)
                        rate = calculateRate(currRest.Rate);
                    else
                        rate = calculateRate(Convert.ToDouble(dbReviews[12][j]));
                    UserReview newReview = new UserReview(currRest.ID, dbReviews[10][j], dbReviews[11][j], rate);
                    listReviews.Add(newReview);
                }
                currRest.Types = styles;
                currRest.Reviews = listReviews;
            }
            return currRest;
        }

        public bool addReview(UserReview userReview)
        {

            // insert into t_reviews values('ID_TA', 'review', 'date');
            bool result = dBConnect.Insert("(\"" + userReview.RestID + "\", \"" + userReview.Content + "\" ,\"" + userReview.Date + "\" , " + userReview.Rate.ToString() + ")", "t_reviews");
            if (result ==false)
            {
                return false;
            }
            List<string>[] data = dBConnect.Select("t_restaurants", "ID_TA = '" + userReview.RestID + "'", null, null, null,-1);
            if (data == null)
                return false;
            int numOfReviews = Int32.Parse(data[7][0]);
            double rate = Convert.ToDouble(data[3][0]);
            double newRate = (numOfReviews * rate + userReview.Rate) / (numOfReviews + 1);
            // UPDATE t_restaurants SET Num_of_reviews = 'num', Rating =rate WHERE ID_TA = 'ID_TA';
            result = dBConnect.Update("t_restaurants","Numbers_of_reviews= " 
                + (numOfReviews+1).ToString() + " ,Rating= \"" + newRate.ToString() + "\"",  "ID_TA= \"" + userReview.RestID + "\"");
            if (result == false)
            {
                return false;
            }
            RestsResults[restsResults.FindIndex(x => x.ID == userReview.RestID)].Rate = newRate;
            return true;
        }

        public bool addStyle()
        {
            //TODO CHANGE
            //insert into t_style_rest
            //select 'ID_TA' , t_style.id from t_style where t_style.style = 'French';
            /*string select = "'"+ID_TA+"' , t_style.id from t_style";
            string where = " t_style.style = '" + style + "'";
            bool result = dBConnect.InsertSelect(select,where, "t_style_rest");
            if (result == false)
            {
                return false;
            }*/
            return true;
        }

        public bool addRest(Restaurant rest)
        {
            bool price = false;
            bool url = false;
            string select = "'" + rest.Name + "', t_city.id, 0";
            if (rest.PriceRange != null)
            {
                select += ", '" + rest.PriceRange + "'";
                price = true;
            }
            select += ", 0";
            if (!rest.URL.Equals(""))
            {
                select += ", '" + rest.URL + "'";
                url = true;
            }
            select += ", '" + username + "' from t_city";
            string where = "t_city.name = '" + rest.City + "'";
            string values = "name, city_id,rating ,";
            if (price)
                values += "price_range,";
            values += "numbers_of_reviews,";
            if (url)
                values += "url_ta,";
            values += "owner ";
            // insert restaurant
            bool result = dBConnect.InsertSelect(select, where, "t_restaurants", values);
            if (result == false)
                return false;
            int id = dBConnect.select_last_inserted_id();
            if (id == -1)
                return false;

            // insert styles     
            if (rest.Types.Count != 0)
            {
                select =  id.ToString() + " , t_style.id from t_style ";
                foreach (string type in rest.Types)
                {
                    where = " t_style.style = \'" + type + "\';";
                    result = dBConnect.InsertSelect(select, where, "t_style_rest", null);
                    if (result == false)
                        return false;
                }
            }
            int new_id = dBConnect.select_last_inserted_id();
            if (new_id == -1)
                return false;
            restsResults[RestsResults.Count - 1].ID = new_id.ToString();
            return true;
        }

        public Dictionary<string, int> graphCountriesByType(string type)
        {
            string query = "select t_country.name ,count(*) from t_restaurants join t_city on t_restaurants.city_id = t_city.id join t_country on t_city.countrycode = t_country.code where t_restaurants.ID_TA in (select ID_TA from t_style_rest where styleid =(select id from t_style where style = '" + TypesFilter[0] + "'))";
            query += " group by t_country.name order by count(*) Desc Limit 5";
            List<string>[] result = dBConnect.Count(query);
            Dictionary<string, int> dic = new Dictionary<string, int>();
           

            if (result == null) {
                return null;
            }

            double total = 0;
            for (int i = 0; i < result[0].Count; i++)
            {
                total += Convert.ToInt32(result[1][i]);
            }

            double percent = 0;
            int sumPercent = 0;
            for (int i = 0; i < result[0].Count-1; i++)
            {
                percent = (Convert.ToInt32(result[1][i]) / total) * 100;
                if (Convert.ToInt32(percent) == 0)
                    continue;
                sumPercent += Convert.ToInt32(percent);
                dic.Add(result[0][i], Convert.ToInt32(percent));
            }
            if((100-sumPercent) != 0)
                dic.Add(result[0][4], 100-sumPercent);

            /*dic.Add("france", 5);
            dic.Add("germany", 22);
            dic.Add("Netherlands", 27);
            dic.Add("England", 46);*/

            return dic;
        }

        public List<string> getCountries()
        {
            return dBConnect.SelectColumn("t_country", null, null, null, "name");
        }

        public List<string> getCities()
        {
            string whereCond = "countrycode = (select code from t_country where name = \"" + countryFilter + "\")";
            return dBConnect.SelectColumn("t_city",  whereCond ,"name", "ASC", "name");
        }

        public List<string> getAllCities()
        {
            return dBConnect.SelectColumn("t_city", null, "name", "Asc", "name");
        }

        public List<string> getStyles()
        {
            return dBConnect.SelectColumn("t_style", null , "style", "Asc", "style");
        }

        public bool updateRestaurant(Restaurant rest)
        {
            string set = "name='" + rest.Name + "' ,URL_TA= '" + rest.URL + "' ,price_range= '" + rest.PriceRange + "' ,t_restaurants.city_id = (SELECT t_city.id FROM t_city WHERE t_city.name = '" + rest.City + "')";
            return dBConnect.Update("t_restaurants", set, "ID_TA = " + rest.ID);
        }

    }
}
