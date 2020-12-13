using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EatInEurope
{
    interface IModel : INotifyPropertyChanged
    {
        string UserName { get; set; }
        string[] Top5Rests {get; set;} // results of the top 5 rests fitting to user choise

        // users filters
        List<string> CountriesFilter { get; set; }
        List<string> CitiesFilter { get; set; }
        List<string> TypesFilter { get; set; }
        double[] RateFilter { get; set; }
        int[] PriceFilter { get; set; }

        List<string> RestsResult { get; set; } // all rests fitting to user choise *or* all rests owned by the user


        // Methods which makes queries.
        bool register(string username, string password1, string password2);
        bool signIn(string username, string password);
        void orderBy(string orderType, bool asc); // orderType=rests name,price,rate | asc=true -> A-Z | asc=false -> Z-A
        string[] restDetails(string rest); // get the details of the rest by its name
        void addReview(int rate, string body);
        void addRest(string name, string country, string city, List<string> types); // for owner
        Dictionary<string, double> graphCountriesByType(string type); // key=country | value=precentage

        // Message to user property
        string Message { get; set; }

        // Method which comunicate with the SQL server
        void send(string message);
        void connect(string ip, int port);
        void start();
        void disconnect();
    }
}
