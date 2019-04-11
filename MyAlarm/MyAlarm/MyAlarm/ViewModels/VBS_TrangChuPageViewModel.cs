using Domain;
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

        #region TitleBindProp
        private string _TitleBindProp = "Scrum Axon";
        public string TitleBindProp
        {
            get { return _TitleBindProp; }
            set { SetProperty(ref _TitleBindProp, value); }
        }
        #endregion


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
            var param = new NavigationParameters();
            param.Add(Param.PARAM_TITLE, TitleBindProp);
            await NavigationService.NavigateAsync(nameof(VBS_MemberPage),param);

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
            var param = new NavigationParameters();
            param.Add(Param.PARAM_TITLE, TitleBindProp);
            await NavigationService.NavigateAsync(nameof(VBS_WorkPage),param);

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToWorkCommand()
        {
            GoToWorkCommand = new DelegateCommand<object>(OnGoToWork);
            GoToWorkCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

        #region GoToScrumFrameworkCommand

        public DelegateCommand<object> GoToScrumFrameworkCommand { get; private set; }
        private async void OnGoToScrumFramework(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            var param = new NavigationParameters();
            param.Add(Param.PARAM_TITLE, TitleBindProp);
            await NavigationService.NavigateAsync(nameof(VBS_ScrumFrameworkPage), param);
            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToScrumFrameworkCommand()
        {
            GoToScrumFrameworkCommand = new DelegateCommand<object>(OnGoToScrumFramework);
            GoToScrumFrameworkCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion


    }
}
