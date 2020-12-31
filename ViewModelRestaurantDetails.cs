using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope
{
    class ViewModelRestaurantDetails : INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelRestaurantDetails(IModel model)
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

        public string VM_RestID
        {
            get { return model.RestID; }
            set { model.RestID = value; }
        }

        public Restaurant VM_RestDetails
        {
            get { return model.RestDetails; }
        }
    }
}
