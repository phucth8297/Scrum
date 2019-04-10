using MyAlarm.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAlarm.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAlarm _alarm;

        public MainPageViewModel(INavigationService navigationService, IAlarm alarm)
            : base(navigationService)
        {
            Title = "Main Page";

            _alarm = alarm;
            CommandNameCmd = new DelegateCommand<object>(OnCommandName);
        }


        public DelegateCommand<object> CommandNameCmd { get; private set; }
        private void OnCommandName(object obj)
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddSeconds(30);
            _alarm.SetAlarm(dateTime);
        }
    }
}
