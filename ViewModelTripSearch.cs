using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope
{
    class ViewModelTripSearch: INotifyPropertyChanged
    {
        public List<string> Countries { get; set; }
        private IModel model;


        public ViewModelTripSearch(IModel model)
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

        public Dictionary<string, int> VM_CountriesPartStyle
        {
            get
            {
                return model.CountriesPartStyle;
            }
        }

    }
}
