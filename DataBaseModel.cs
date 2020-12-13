using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Threading;

namespace EatInEurope
{
    class DataBaseModel : IModel

    {
        private string ip;
        private int port;
        Queue<string> queCommand;
        volatile Boolean stop;

/*        public SimulatorModel()
        {
            Message = "Welcome to Flight Simulator";
            ip = ConfigurationManager.AppSettings["ip"];
            port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            queCommand = new Queue<string>();
            this.telnetClient = telnetClient;
            stop = false;
            IsConnect = false;
            connect(ip, port);
            Coordinates = "32.002644, 34.888781";
        }*/

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

        private List<string> restsResults = new List<string>();
        public List<string> RestsResult
        {
            get { return restsResults; }
            set
            {
                restsResults = value;
                NotifyPropertyChanged("restsResults");
            }
        }

        public void send(string message)
        {
            // Push the messege to the queue.
            queCommand.Enqueue(message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, int port)
        {
            /*try
            {
                // Stop is false for start loop.
                stop = false;
                telnetClient.connect(ip, port);
                IsConnect = true;
                Message = "Connect To Server";
                start();
            }
            catch
            {
                // If the connection is failed- tellto the user.
                IsConnect = false;
                stop = true;
                Message = "Cannot connect to server";
            }*/
        }

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

        public void disconnect()
        {
            
        }

        public bool register(string username, string password1, string password2)
        {
            if (!password1.Equals(password2)) {
                Message = "The passwords do not match, please try again";
                return false;
            }

            // add query for adding new user
            return true;

        }

        public bool signIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void orderBy(string orderType, bool asc)
        {
            throw new NotImplementedException();
        }

        public string[] restDetails(string rest)
        {
            throw new NotImplementedException();
        }

        public void addReview(int rate, string body)
        {
            throw new NotImplementedException();
        }

        public void addRest(string name, string country, string city, List<string> types)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> graphCountriesByType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
