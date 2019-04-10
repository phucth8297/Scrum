using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.EFStandard
{
    public class BaseEntity : BindableBase
    {
        public void RaisePropertyChange(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
