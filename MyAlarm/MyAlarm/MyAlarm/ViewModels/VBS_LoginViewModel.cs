using Domain;
using Logic;
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
    class VBS_LoginViewModel : BaseViewModel
    {
        private MemberLogic logic;

        public VBS_LoginViewModel(InitParamVm initParamVm) : base(initParamVm) {
            logic = new MemberLogic(Helper.GetConnectionString());

        }

        

        #region PassBindProp
        private string _PassBindProp = "";
        public string PassBindProp
        {
            get { return _PassBindProp; }
            set { SetProperty(ref _PassBindProp, value); }
        }
        #endregion
            

        #region LoginCommand

        public DelegateCommand<object> LoginCommand { get; private set; }
        private bool CanLogin(object obj)
        {
            if (IsNotBusyBindProp && string.IsNullOrWhiteSpace(EmailBindProp) == false && string.IsNullOrWhiteSpace(PassBindProp) == false)
            {
                return true;
            }
            return false;
        }
        private async void OnLogin(object obj)
        {
            if (CanLogin(obj) == false)
            {
                return;
            }
            try
            {
                IsBusyBindProp = true;
                var isLogin = await logic.Login(EmailBindProp, PassBindProp);
                if (isLogin)
                {
                    var param = new NavigationParameters();
                    param.Add(Param.PARAM_MEMBER_EMAIL, EmailBindProp);
                    await NavigationService.NavigateAsync(nameof(VBS_MemberPage), param);

                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("Thông báo", "Tài khoản hoặc mật khẩu không đúng", "Thử lại");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusyBindProp = false;

            }
            // Thuc hien cong viec tai day

        }

        [Initialize]
        private void InitLoginCommand()
        {
            LoginCommand = new DelegateCommand<object>(OnLogin, CanLogin);
            LoginCommand.ObservesProperty(() => IsNotBusyBindProp);
            LoginCommand.ObservesProperty(() => EmailBindProp);
            LoginCommand.ObservesProperty(() => PassBindProp);

        }

        #endregion

    }
}
