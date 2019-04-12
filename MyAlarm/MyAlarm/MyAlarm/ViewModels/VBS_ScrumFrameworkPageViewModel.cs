using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Logic;
using Microsoft.EntityFrameworkCore;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using MyAlarm.Views;
using Prism.Commands;
using Prism.Navigation;
using Telerik.XamarinForms.DataControls.ListView.Commands;

namespace MyAlarm.ViewModels
{
    public class VBS_ScrumFrameworkPageViewModel : BaseViewModel
    {
        public VBS_ScrumFrameworkPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
        }

        #region ListScrumFrameworkBindProp
        private ObservableCollection<ScrumFramework> _ListScrumFrameworkBindProp = null;
        public ObservableCollection<ScrumFramework> ListScrumFrameworkBindProp
        {
            get { return _ListScrumFrameworkBindProp; }
            set { SetProperty(ref _ListScrumFrameworkBindProp, value); }
        }
        #endregion

        #region SelectScrumFrameworkBindProp
        private ScrumFramework _SelectScrumFrameworkBindProp = null;
        public ScrumFramework SelectScrumFrameworkBindProp
        {
            get { return _SelectScrumFrameworkBindProp; }
            set { SetProperty(ref _SelectScrumFrameworkBindProp, value); }
        }
        #endregion

        #region TranferScrumFrameworkDetailCommand

        public DelegateCommand<object> TranferScrumFrameworkDetailCommand { get; private set; }
        private async void OnTranferScrumFrameworkDetail(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            if (obj is ItemTapCommandContext objTap && objTap.Item is ScrumFramework scrumFramework)
            {
                var param = new NavigationParameters();
                param.Add(Helper.KEY_SCRUMFRAMEWORK_OBJECT, scrumFramework);
                await NavigationService.NavigateAsync(nameof(VBS_ScrumFrameworkDetailPage), param);
            }

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitTranferScrumFrameworkDetailCommand()
        {
            TranferScrumFrameworkDetailCommand = new DelegateCommand<object>(OnTranferScrumFrameworkDetail);
            TranferScrumFrameworkDetailCommand.ObservesCanExecute(() => IsNotBusyBindProp);
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
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    break;
                case NavigationMode.New:
                    var logic = new ScrumFrameworkLogic(Helper.GetConnectionString());
                    var listScrumFramework = await logic.GetAllAsync().ToListAsync();

                    ListScrumFrameworkBindProp = new ObservableCollection<ScrumFramework>(listScrumFramework);

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
