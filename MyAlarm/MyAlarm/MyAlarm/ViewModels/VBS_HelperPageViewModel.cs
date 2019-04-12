using MyAlarm.Model;
using MyAlarm.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.ViewModels
{
    class VBS_HelperPageViewModel : BaseViewModel
    {
        public VBS_HelperPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {

        }
        #region GoToLoginCommand

        public DelegateCommand<object> GoToLoginCommand { get; private set; }
        private async void OnGoToLogin(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            await NavigationService.NavigateAsync(nameof(VBS_LoginPage));

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToLoginCommand()
        {
            GoToLoginCommand = new DelegateCommand<object>(OnGoToLogin);
            GoToLoginCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion
        #region GoToChangePassCommand

        public DelegateCommand<object> GoToChangePassCommand { get; private set; }
        private async void OnGoToChangePass(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            await NavigationService.NavigateAsync(nameof(VBS_ChangePass));

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToChangePassCommand()
        {
            GoToChangePassCommand = new DelegateCommand<object>(OnGoToChangePass);
            GoToChangePassCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

    }
}
