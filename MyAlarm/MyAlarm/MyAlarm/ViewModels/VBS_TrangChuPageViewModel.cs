using Logic;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using MyAlarm.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyAlarm.ViewModels
{
    class VBS_TrangChuPageViewModel : BaseViewModel
    {
        public VBS_TrangChuPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
        }
        
        #region GoToMemberCommand

        public DelegateCommand<object> GoToMemberCommand { get; private set; }
        private async void OnGoToMember(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            await NavigationService.NavigateAsync(nameof(VBS_MemberPage));

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToMemberCommand()
        {
            GoToMemberCommand = new DelegateCommand<object>(OnGoToMember);
            GoToMemberCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

        #region GoToWorkCommand

        public DelegateCommand<object> GoToWorkCommand { get; private set; }
        private async void OnGoToWork(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            await NavigationService.NavigateAsync(nameof(VBS_WorkPage));

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToWorkCommand()
        {
            GoToWorkCommand = new DelegateCommand<object>(OnGoToWork);
            GoToWorkCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion
        

        
    }
}
