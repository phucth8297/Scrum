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

                        var logic = new WorkDetailLogic(Helper.GetConnectionString());
                        var workDetail = await logic.GetAsync(work.Id);

                        TitleBindProp = workDetail.Name;
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
