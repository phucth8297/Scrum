using System;
using System.Collections.Generic;
using System.Text;
using Logic;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using Prism.Navigation;

namespace MyAlarm.ViewModels
{
    class VBS_WorkDetailPageViewModel : BaseViewModel
    {

        public VBS_WorkDetailPageViewModel(InitParamVm initParamVm) : base(initParamVm)
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

        #region DescriptionBindProp
        private string _DescriptionBindProp = null;
        public string DescriptionBindProp
        {
            get { return _DescriptionBindProp; }
            set { SetProperty(ref _DescriptionBindProp, value); }
        }
        #endregion

        #region WorkerBindProp
        private string _WorkerBindProp = null;
        public string WorkerBindProp
        {
            get { return _WorkerBindProp; }
            set { SetProperty(ref _WorkerBindProp, value); }
        }
        #endregion

        #region StatusBindProp
        private string _StatusBindProp = null;
        public string StatusBindProp
        {
            get { return _StatusBindProp; }
            set { SetProperty(ref _StatusBindProp, value); }
        }
        #endregion

        #region EndDateBindProp
        private string _EndDateBindProp = null;
        public string EndDateBindProp
        {
            get { return _EndDateBindProp; }
            set { SetProperty(ref _EndDateBindProp, value); }
        }
        #endregion

        #region StartDateBindProp
        private string _StartDateBindProp = null;
        public string StartDateBindProp
        {
            get { return _StartDateBindProp; }
            set { SetProperty(ref _StartDateBindProp, value); }
        }
        #endregion

        #region WoorkHourBindProp
        private string _WoorkHourBindProp = null;
        public string WoorkHourBindProp
        {
            get { return _WoorkHourBindProp; }
            set { SetProperty(ref _WoorkHourBindProp, value); }
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

                    if (parameters.ContainsKey(Helper.KEY_WORK_OBJECT))
                    {
                        var work = parameters[Helper.KEY_WORK_OBJECT] as Work;
                        EndDateBindProp = work.EndDate;
                        StartDateBindProp = work.StartDate;
                        WoorkHourBindProp = work.HourWork;
                        DescriptionBindProp = work.Des;

                        var logicStatus = new StatusLogic(Helper.GetConnectionString());
                        var status = await logicStatus.GetAsync(work.FkStatus);

                        StatusBindProp = status.Name;

                       
                        var logic = new WorkDetailLogic(Helper.GetConnectionString());
                        var workDetail = await logic.GetAsync(work.Id);

                        TitleBindProp = workDetail.Name;

                        var logicMember = new MemberLogic(Helper.GetConnectionString());
                        var member = await logicMember.GetAsync(workDetail.FkUser);

                        WorkerBindProp = member.Name;

                    }

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
