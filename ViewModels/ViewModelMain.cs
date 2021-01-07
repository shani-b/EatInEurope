using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope.ViewModels
{
    class ViewModelMain : INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelMain(IModel model)
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

        public bool VM_IsClient
        {
            get
            {
                return model.IsClient;
            }
            set
            {
                model.IsClient = value;
            }
        }
    }
}
