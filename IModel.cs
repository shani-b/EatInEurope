using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EatInEurope
{
    interface IModel : INotifyPropertyChanged
    {
        string UserName { get; set; }
        string Password { get; set; }
        string NewPassword { get; set; }
        bool IsClient { set; get; }
        bool IsEdit { set; get; }
        bool IsAddRest { set; get; }
        string IDToRemove { set; get; }
        bool LoginOK { get; set; }
        bool UsernameFree { get; set; }
        string[] Top5Rests {get; set;} // results of the top 5 rests fitting to user choise

        // users filters
        string CountryFilter { get; set; }
        List<string> CitiesFilter { get; set; }
        List<string> TypesFilter { get; set; }
        List<string> CountriesOptions { get; set; }
        List<string> CitiesOptions { get; set; }
        List<string> TypesOptions { get; set; }
        string Order { get; set; }
        bool Asc { get; set; }
        string RestID { get; set; }
        string RestName { get; set; }
        double RestRating { get; }
        Restaurant RestDetails { get; set; }
        UserReview NewReview { get; set; }
        List<double> RateFilter { get; set; }
        List<string> PriceFilter { get; set; }
        Dictionary<string, int> CountriesPartStyle { get; }

        List<Restaurant> RestsResults { get; set; } // all rests fitting to user choise *or* all rests owned by the user

        // Methods which makes queries.
        bool register(string username, string password);
        List<Restaurant> getRestByFilter();
        bool signIn(string username, string password);
        //List<Restaurant> orderBy(string orderType, bool order); // orderType=rests name,price,rate | asc=true -> A-Z | asc=false -> Z-A
        Restaurant restDetails(); // get the details of the rest by its id
        bool addReview(UserReview userReview);
        bool addRest(Restaurant rest); // for owner
        // Restaurant restDetails(string rest); // get the details of the rest by its name
        //void addReview(int rate, string body);
        //void addRest(string name, string country, string city, List<string> types); // for owner

        Dictionary<string, int> graphCountriesByType(string type); // key=country | value=precentage
        List<string> getCountries();
        List<string> getCities();
        List<string> getAllCities();
        bool updateRestaurant(Restaurant rest);
    }
}
