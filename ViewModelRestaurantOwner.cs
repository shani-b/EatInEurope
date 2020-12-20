using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope
{
    class ViewModelRestaurantOwner : INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelRestaurantOwner(IModel model)
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

        public List<List<string>> VM_RestsResults
        {
            get { return model.RestsResults; }
            set
            {
                model.RestsResults = value;
            }
        }
    }
}
