using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope.ViewModels
{
    class ViewModelSearch: INotifyPropertyChanged
    {
        public List<string> Countries { get; set; }
/*        public List<string> Cities { get; set; }*/
        private IModel model;


        public ViewModelSearch(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            /*Cities = new List<string> {
                "Amsterdam", "Athens","Barcelona", "Berlin","Bratislava","Brussels","Budapest",
                "Copenhagen","Dublin","Edinburgh","Geneva","Hamburg","Helsinki","Krakow","Lisbon","Ljubljana",
                "London","Luxembourg","Lyon","Madrid","Milan","Munich","Oporto","Oslo","Paris","Prague","Rome",
                "Stockholm","Vienna","Warsaw","Zurich"
            };*/

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string PropName)
        {
            if (this.PropertyChanged!=null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropName));
            }
        }

        // The Dashboard propreties for update his values in the view.
        public string VM_CountryFilter
        {
            set
            {
                model.CountryFilter = value;
            }
        }

        public List<string> VM_CitiesFilter
        {
            set
            {
                model.CitiesFilter = value;
            }
        }

        public List<string> VM_TypesFilter
        {
            set
            {
                model.TypesFilter = value;
            }
        }

        public double[] VM_RateFilter
        {
            set
            {
                model.RateFilter = value;
            }
        }

        public int[] VM_PriceFilter
        {
            set
            {
                model.PriceFilter = value;
            }
        }

        public List<string> VM_CountriesOptions
        {
            get
            {
                return model.CountriesOptions;
            }

        }

        public List<string> VM_CitiesOptions
        {
            get
            {
                return model.CitiesOptions;
            }
            //set
            //{
            //    model.CitiesOptions = value;
            //}
        }

        public List<string> VM_TypesOptions
        {
            get
            {
                return model.TypesOptions;
            }
            //set
            //{
            //    model.TypesOptions = value;
            //} 
        }

        public List<Restaurant> VM_RestsResults
        {
            get { return model.RestsResults; }
            set
            {
                model.RestsResults = value;
            }
        }

        public string VM_RestName
        {
            set { model.RestName = value; }
        }

        public string VM_Order
        {
            get { return model.Order; }
            set { model.Order = value; }
        }
        public bool VM_Asc
        {
            get { return model.Asc; }
            set { model.Asc = value; }
        }
    }
}
