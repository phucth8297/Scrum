using Domain;
using Logic;
using MyAlarm.Domain;
using MyAlarm.EFStandard;
using MyAlarm.Helpers;
using MyAlarm.Model;
using MyAlarm.Views;
using Prism.Commands;
using Prism.Mvvm;
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

        

        #region ModeNewBindProp
        private bool _ModeNewBindProp = false;
        public bool ModeNewBindProp
        {
            get { return _ModeNewBindProp; }
            set { SetProperty(ref _ModeNewBindProp, value); }
        }
        #endregion

        #region ModelBindProp
        private Member _ModelBindProp = null;
        public Member ModelBindProp
        {
            get { return _ModelBindProp; }
            set { SetProperty(ref _ModelBindProp, value); }
        }
        #endregion

        #endregion

        #region Command

        #region AddMemberCommand

        public DelegateCommand<object> AddMemberCommand { get; private set; }
        private bool CanAdd(object b)
        {
            if (IsNotBusyBindProp && string.IsNullOrWhiteSpace(NameMemberBindProp) == false && string.IsNullOrWhiteSpace(GenderMemberBindProp) == false
                && string.IsNullOrWhiteSpace(PhoneNumberMemberBindProp) == false && string.IsNullOrWhiteSpace(EmailBindProp) == false)
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
                Email = EmailBindProp,
                FkRole = "R03"
            };
            try
            {
                if (ModeNewBindProp)
                {
                    var createMember = await logic.CreateMember(member);
                    var param = new NavigationParameters();
                    param.Add(Param.PARAM_ADD_MEMBER, createMember);
                    await NavigationService.NavigateAsync(nameof(VBS_MemberPage), param);
                }
                else
                {
                    var editMember = await logic.EditMember(ModelBindProp);
                    editMember.RaisePropertyChange(nameof(Member.NumPhone));
                    editMember.RaisePropertyChange(nameof(Member.Name));
                    editMember.RaisePropertyChange(nameof(Member.Email));
                    await NavigationService.GoBackAsync();
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
            AddMemberCommand.ObservesProperty(() => EmailBindProp);

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
                    if (parameters.ContainsKey(Param.PARAM_EDIT_MEMBER))
                    {
                        ModelBindProp = (Member)parameters[Param.PARAM_MODE];
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
