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
    class VBS_ChangePassViewModel : BaseViewModel
    {
        private MemberLogic logic;

       

        public VBS_ChangePassViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
            logic = new MemberLogic(Helper.GetConnectionString());

        }

        #region PresentPassBindProp
        private string _PresentPassBindProp = "";
        public string PresentPassBindProp
        {
            get { return _PresentPassBindProp; }
            set { SetProperty(ref _PresentPassBindProp, value); }
        }
        #endregion

        #region NewPassBindProp
        private string _NewPassBindProp = "";
        public string NewPassBindProp
        {
            get { return _NewPassBindProp; }
            set { SetProperty(ref _NewPassBindProp, value); }
        }
        #endregion

        #region ConfirmPassBindProp
        private string _ConfirmPassBindProp = "";
        public string ConfirmPassBindProp
        {
            get { return _ConfirmPassBindProp; }
            set { SetProperty(ref _ConfirmPassBindProp, value); }
        }
        #endregion


        #region SaveChangeCommand

        public DelegateCommand<object> SaveChangeCommand { get; private set; }
        private bool CanChange(object obj)
        {
            if(IsNotBusyBindProp && string.IsNullOrWhiteSpace(EmailBindProp) == false)
            {
                return true;
            }
            return false;
        }
        private async void OnSaveChange(object obj)
        {
            if (CanChange(obj) == false)
            {
                await PageDialogService.DisplayAlertAsync("Xác nhận", "Bạn chưa đăng nhập", "Thử lại");
                await NavigationService.NavigateAsync(nameof(VBS_LoginPage));

            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            
            var member = await logic.GetMember(EmailBindProp);
            if (PresentPassBindProp != member.Password)
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Mật khẩu hiện tại không đúng", "Thử lại");
                IsBusyBindProp = false;
                return;
            }
            if(NewPassBindProp != ConfirmPassBindProp)
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Mật khẩu không giống nhau", "Thử lại");
                IsBusyBindProp = false;
                return;
            }
            member.Password = NewPassBindProp;
            var isChange = await logic.ChangePass(member);
            if (isChange)
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Thay đổi mật khẩu thành công", "Đồng ý");
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Thay đổi mật khẩu thất bại", "Thử lại");

            }
            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitSaveChangeCommand()
        {
            SaveChangeCommand = new DelegateCommand<object>(OnSaveChange);
            SaveChangeCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

    }
}
