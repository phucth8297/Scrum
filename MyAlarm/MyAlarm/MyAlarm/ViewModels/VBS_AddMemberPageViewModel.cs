using Domain;
using Logic;
using MyAlarm.Domain;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using MyAlarm.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.ViewModels
{
    class VBS_AddMemberPageViewModel : BaseViewModel
    {
        private MemberLogic logic; 

        public VBS_AddMemberPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
            logic = new MemberLogic(Helper.GetConnectionString());
        }

        #region BindProp
        #region NameMemberBindProp
        private string _NameMemberBindProp = "";
        public string NameMemberBindProp
        {
            get { return _NameMemberBindProp; }
            set { SetProperty(ref _NameMemberBindProp, value); }
        }
        #endregion

        #region GenderMemberBindProp
        private string _GenderMemberBindProp = "";
        public string GenderMemberBindProp
        {
            get { return _GenderMemberBindProp; }
            set { SetProperty(ref _GenderMemberBindProp, value); }
        }
        #endregion

        #region PhoneNumberMemberBindProp
        private string _PhoneNumberMemberBindProp = "";
        public string PhoneNumberMemberBindProp
        {
            get { return _PhoneNumberMemberBindProp; }
            set { SetProperty(ref _PhoneNumberMemberBindProp, value); }
        }
        #endregion

        #region EmailMemberBindProp
        private string _EmailMemberBindProp = "";
        public string EmailMemberBindProp
        {
            get { return _EmailMemberBindProp; }
            set { SetProperty(ref _EmailMemberBindProp, value); }
        }
        #endregion

        #region ModeNewBindProp
        private bool _ModeNewBindProp = false;
        public bool ModeNewBindProp
        {
            get { return _ModeNewBindProp; }
            set { SetProperty(ref _ModeNewBindProp, value); }
        }
        #endregion

        #endregion

        #region Command

        #region AddMemberCommand

        public DelegateCommand<object> AddMemberCommand { get; private set; }
        private bool CanAdd(object b)
        {
            if (IsNotBusyBindProp && string.IsNullOrWhiteSpace(NameMemberBindProp) == false && string.IsNullOrWhiteSpace(GenderMemberBindProp) == false
                && string.IsNullOrWhiteSpace(PhoneNumberMemberBindProp) == false && string.IsNullOrWhiteSpace(EmailMemberBindProp) == false)
            {
                return true;
            }
            return false;
        }
        private async void OnAddMember(object obj)
        {
            if (CanAdd(obj) == false)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day

            var member = new Member
            {
                Id = Guid.NewGuid().ToString(),
                Name = NameMemberBindProp,
                NumPhone = PhoneNumberMemberBindProp,
                Gender = GenderMemberBindProp,
                Email = EmailMemberBindProp,
                FkRole = "R03"
            };
            try
            {
                if (ModeNewBindProp)
                {
                    var createMember = await logic.CreateMember(member);
                }
                else
                {
                    var editMember = await logic.CreateMember(member);
                }
            }
            catch (Exception e )
            {

                throw e;
            }
            finally
            {
                IsBusyBindProp = false;

            }


        }

        [Initialize]
        private void InitAddMemberCommand()
        {
            AddMemberCommand = new DelegateCommand<object>(OnAddMember, CanAdd);
            AddMemberCommand.ObservesProperty(() => IsNotBusyBindProp);
            AddMemberCommand.ObservesProperty(() => NameMemberBindProp);
            AddMemberCommand.ObservesProperty(() => GenderMemberBindProp);
            AddMemberCommand.ObservesProperty(() => PhoneNumberMemberBindProp);
            AddMemberCommand.ObservesProperty(() => EmailMemberBindProp);

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

        #endregion
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    break;
                case NavigationMode.New:

 
                    if (parameters.ContainsKey(Param.PARAM_MODE))
                    {
                        ModeNewBindProp = (bool)parameters[Param.PARAM_MODE];

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
    }
}
