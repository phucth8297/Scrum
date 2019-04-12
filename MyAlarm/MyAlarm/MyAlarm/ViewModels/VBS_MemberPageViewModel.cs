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
using System.Collections.ObjectModel;
using System.Text;

namespace MyAlarm.ViewModels
{
    class VBS_MemberPageViewModel : BaseViewModel
    {
        private MemberLogic logic;

        public VBS_MemberPageViewModel(InitParamVm initParamVm) : base(initParamVm)
        {
            logic = new MemberLogic(Helper.GetConnectionString());

        }

        #region ListMemberBindProp
        private ObservableCollection<Member> _ListMemberBindProp = new ObservableCollection<Member>();
        public ObservableCollection<Member> ListMemberBindProp
        {
            get { return _ListMemberBindProp; }
            set { SetProperty(ref _ListMemberBindProp, value); }
        }
        #endregion

        #region SelectedMemberBindProp
        private Member _SelectedMemberBindProp = null;
        public Member SelectedMemberBindProp
        {
            get { return _SelectedMemberBindProp; }
            set { SetProperty(ref _SelectedMemberBindProp, value); }
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

        

        #region GoToAddMemberCommand

        public DelegateCommand<object> GoToAddMemberCommand { get; private set; }
        private async void OnGoToAddMember(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            if (EmailBindProp == "")
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Bạn cần phải đăng nhập để thực hiện chức năng này", "Đồng ý");
                var param = new NavigationParameters();
                param.Add(Param.PARAM_TITLE, TitleBindProp);
                await NavigationService.NavigateAsync(nameof(VBS_LoginPage), param);

            }
            else
            {
                var member = await logic.GetMember(EmailBindProp);
                var role = logic.CheckRole(member);
                if (role == ConstRole.R01.ToString() || role == ConstRole.R02.ToString())
                {
                    ModeNewBindProp = true;
                    var param = new NavigationParameters();
                    param.Add(Param.PARAM_TITLE, TitleBindProp);
                    param.Add(Param.PARAM_MODE, ModeNewBindProp);
                    await NavigationService.NavigateAsync(nameof(VBS_AddMemberPage), param);
                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("Thông báo", "Bạn không có quyền thực hiện chức năng này", "Đồng ý");
                }
            }


            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitGoToAddMemberCommand()
        {
            GoToAddMemberCommand = new DelegateCommand<object>(OnGoToAddMember);
            GoToAddMemberCommand.ObservesCanExecute(() => IsNotBusyBindProp);
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

        #region DeleteCommand

        public DelegateCommand<object> DeleteCommand { get; private set; }
        private async void OnDelete(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            if (obj is Member member)
            {
                var role = logic.CheckRole(member);
                if (role == ConstRole.R01.ToString() || role == ConstRole.R02.ToString())
                {
                    var resultConfirm = await PageDialogService.DisplayAlertAsync("Xác nhận", "Bạn có chắc muốn xóa thành viên này không?", "Có", "Không");

                    if (resultConfirm)
                    {
                        var item = logic.DeleteMember(member.Id);
                        ListMemberBindProp.Remove(member);
                    }
                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("Thông báo", "Bạn không có quyền thực hiện chức năng này", "Đồng ý");
                }
            }
            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitDeleteCommand()
        {
            DeleteCommand = new DelegateCommand<object>(OnDelete);
            DeleteCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion

        #region GoToEditMemberCommand

        public DelegateCommand<object> GoToEditMemberCommand { get; private set; }
        private async void OnEdit(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            // Thuc hien cong viec tai day
            if (EmailBindProp == null)
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", "Bạn cần phải đăng nhập để thực hiện chức năng này", "Đồng ý");
                await NavigationService.NavigateAsync(nameof(VBS_LoginPage));

            }
            else
            {
                var member = await logic.GetMember(EmailBindProp);
                var role = logic.CheckRole(member);
                if (role == ConstRole.R01.ToString() || role == ConstRole.R02.ToString())
                {
                    ModeNewBindProp = false;

                    if (obj is Member mem)
                    {
                        var param = new NavigationParameters();
                        param.Add(Param.PARAM_MODE, ModeNewBindProp);
                        param.Add(Param.PARAM_EDIT_MEMBER, mem);
                        await NavigationService.NavigateAsync(nameof(VBS_AddMemberPage), param);
                    }

                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("Thông báo", "Bạn không có quyền thực hiện chức năng này", "Đồng ý");
                }
            }
            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitEditCommand()
        {
            GoToEditMemberCommand = new DelegateCommand<object>(OnEdit);
            GoToEditMemberCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion




        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    break;
                case NavigationMode.New:

                    var logic = new MemberLogic(Helper.GetConnectionString());
                    var listMember = logic.GetAllAsync();

                    ListMemberBindProp = new ObservableCollection<Member>(listMember);

                    if (parameters.ContainsKey(Param.PARAM_MEMBER_EMAIL))
                    {
                        EmailBindProp = parameters[Param.PARAM_MEMBER_EMAIL] as string;

                    }
                    if (parameters.ContainsKey(Param.PARAM_TITLE))
                    {
                        TitleBindProp = parameters[Param.PARAM_TITLE] as string;
                        TitleBindProp = "Member";
                    }
                    if (parameters.ContainsKey(Param.PARAM_ADD_MEMBER))
                    {
                        var member = parameters[Param.PARAM_ADD_MEMBER] as Member;
                        ListMemberBindProp.Add(member);

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
