using System;
using System.Collections.Generic;
using System.Text;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using Prism.Navigation;

namespace MyAlarm.ViewModels
{
    public class VBS_ScrumFrameworkDetailPageViewModel : BaseViewModel
    {
        public VBS_ScrumFrameworkDetailPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
        }


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
