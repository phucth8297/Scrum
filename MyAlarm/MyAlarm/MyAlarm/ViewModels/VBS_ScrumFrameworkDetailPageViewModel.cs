using System;
using System.Collections.Generic;
using System.Text;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using Prism.Commands;
using Prism.Navigation;

namespace MyAlarm.ViewModels
{
    public class VBS_ScrumFrameworkDetailPageViewModel : BaseViewModel
    {
        public VBS_ScrumFrameworkDetailPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
        }

        #region TitleBindProp
        private string _TitleBindProp = null;
        public string TitleBindProp
        {
            get { return _TitleBindProp; }
            set { SetProperty(ref _TitleBindProp, value); }
        }
        #endregion

        #region DetailBindProp
        private string _DetailBindProp = null;
        public string DetailBindProp
        {
            get { return _DetailBindProp; }
            set { SetProperty(ref _DetailBindProp, value); }
        }
        #endregion

        #region GoBackCommand

        public DelegateCommand<object> GoBackCommand { get; private set; }
        private async void OnGoBack(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            await NavigationService.GoBackAsync();

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoBackCommand()
        {
            GoBackCommand = new DelegateCommand<object>(OnGoBack);
            GoBackCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

        #region Override
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    break;
                case NavigationMode.New:

                    if (parameters.ContainsKey(Helper.KEY_SCRUMFRAMEWORK_OBJECT))
                    {
                        var scrumFramework = parameters[Helper.KEY_SCRUMFRAMEWORK_OBJECT] as ScrumFramework;

                        TitleBindProp = scrumFramework.Name;
                        DetailBindProp = scrumFramework.Des;

                    };

                    break;
                case NavigationMode.Forward:
                    break;
                case NavigationMode.Refresh:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
