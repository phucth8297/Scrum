using Logic;
using Microsoft.EntityFrameworkCore;
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
using Telerik.XamarinForms.DataControls.ListView.Commands;

namespace MyAlarm.ViewModels
{
    class VBS_WorkPageViewModel : BaseViewModel
    {
        public VBS_WorkPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
        }

        #region ListWorkBindProp
        private ObservableCollection<Work> _ListWorkBindProp = null;
        public ObservableCollection<Work> ListWorkBindProp
        {
            get { return _ListWorkBindProp; }
            set { SetProperty(ref _ListWorkBindProp, value); }
        }
        #endregion

        #region SelectWorkBindProp
        private Work _SelectWorkBindProp = null;
        public Work SelectWorkBindProp
        {
            get { return _SelectWorkBindProp; }
            set { SetProperty(ref _SelectWorkBindProp, value); }
        }
        #endregion

        #region TranferWorkDetailCommand

        public DelegateCommand<object> TranferWorkDetailCommand { get; private set; }
        private async void OnTranferWorkDetail(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            if (obj is ItemTapCommandContext objTap && objTap.Item is Work work)
            {
                var param = new NavigationParameters();
                param.Add(Helper.KEY_WORK_OBJECT, work);
                await NavigationService.NavigateAsync(nameof(VBS_WorkDetailPage), param);
            }

            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitTranferWorkDetailCommand()
        {
            TranferWorkDetailCommand = new DelegateCommand<object>(OnTranferWorkDetail);
            TranferWorkDetailCommand.ObservesCanExecute(() => IsNotBusyBindProp);
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

                    var logic = new WorkLogic(Helper.GetConnectionString());
                    var listWork = await logic.GetAllAsync().ToListAsync();

                    ListWorkBindProp = new ObservableCollection<Work>(listWork);

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
