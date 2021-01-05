using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope.ViewModels
{
    class ViewModelEdit : INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelEdit(IModel model)
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

        public Restaurant VM_RestDetails
        {
            get { return model.RestDetails; }
            set { model.RestDetails = value; }
        }

        public List<string> VM_CitiesOptions
        {
            get
            {
                return model.CitiesOptions;
            }
        }
    }
}
