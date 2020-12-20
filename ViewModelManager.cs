﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EatInEurope
{
    class ViewModelManager : INotifyPropertyChanged
    {
        private IModel model;


        public ViewModelManager(IModel model)
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

        public string VM_UserName
        {
            get { return model.UserName; }
            set
            {
                model.UserName = value;
            }
        }

        public string VM_Password
        {
            get { return model.Password; }
            set
            {
                model.Password = value;
            }
        }
        public string VM_NewPassword
        {
            get { return model.NewPassword; }
            set
            {
                model.NewPassword = value;
            }
        }
    }
}
