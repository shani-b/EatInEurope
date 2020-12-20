using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope
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
        public List<string> VM_CountriesFilter
        {
            set
            {
                model.CountriesFilter = value;
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
        }

        public List<string> VM_TypesOptions
        {
            get
            {
                return model.TypesOptions;
            }
        }
    }
}
