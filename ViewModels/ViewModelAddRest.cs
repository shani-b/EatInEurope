using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope.ViewModels
{
    class ViewModelAddRest: INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelAddRest(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

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

        public List<Restaurant> VM_RestsResults
        {
            get { return model.RestsResults; }
            set
            {
                model.RestsResults = value;
            }
        }
    }
}
