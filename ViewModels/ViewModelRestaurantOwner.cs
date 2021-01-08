using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope.ViewModels
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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropName));
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

        public string VM_RestID
        {
            get { return model.RestID; }
            set
            {
                model.RestID = value;
            }
        }

        public string VM_IDToRemove
        {
            set { model.IDToRemove = value; }
        }
    }
}
