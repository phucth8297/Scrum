using MyAlarm.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyAlarm.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible, IConfirmNavigation
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected bool ShowConfirmGoBack { get; set; } = false;


        private BaseViewModel() : base()
        {
            Type thisType = this.GetType();
            do
            {
                var methods = thisType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                      .Where(h => h.GetCustomAttributes<InitializeAttribute>().Any())
                                      .ToList();
                foreach (var method in methods)
                {
                    method.Invoke(this, null);
                }

                if (thisType == typeof(BindableBase))
                {
                    break;
                }

                thisType = thisType.BaseType;
            } while (true);
        }

        public BaseViewModel(InitParamVm initParamVm) : this()
        {
            NavigationService = initParamVm.NavigationService;
            PageDialogService = initParamVm.PageDialogService;
            EventAggregator = initParamVm.EventAggregator;
        }



        #region TitleBindProp
        private string _TitleBindProp = "";
        public string TitleBindProp
        {
            get { return _TitleBindProp; }
            set { SetProperty(ref _TitleBindProp, value); }
        }
        #endregion

        #region IsBusyBindProp
        private bool _IsBusyBindProp = false;
        public bool IsBusyBindProp
        {
            get { return _IsBusyBindProp; }
            set
            {
                if (SetProperty(ref _IsBusyBindProp, value))
                {
                    RaisePropertyChanged(nameof(IsNotBusyBindProp));
                }
            }
        }
        #endregion

        public bool IsNotBusyBindProp { get { return !_IsBusyBindProp; } }



        #region ConfirmGoBackCommand

        public DelegateCommand<object> ConfirmGoBackCommand { get; private set; }
        private async void OnConfirmGoBackCommand(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            var flagCanGoBack = false;
            if (ShowConfirmGoBack)
            {
                var resultConfirm = await PageDialogService.DisplayAlertAsync("Xác nhận", "Bạn có chắc muốn thoát khỏi màn hình này quay trở về?", "Có", "Không");
                if (resultConfirm)
                {
                    flagCanGoBack = true;
                }
            }
            else
            {
                flagCanGoBack = true;
            }

            if (flagCanGoBack)
            {
                var navParam = new NavigationParameters();
                var handleOnGoBack = await OnGoBackCommandAsync(obj, navParam);
                if (handleOnGoBack)
                {
                    await NavigationService.GoBackAsync(navParam);
                }
            }

            IsBusyBindProp = false;
        }

        protected virtual Task<bool> OnGoBackCommandAsync(object obj, NavigationParameters navParam)
        {
            return Task.FromResult(true);
        }

        [Initialize]
        private void InitConfirmGoBackCommand()
        {
            ConfirmGoBackCommand = new DelegateCommand<object>(OnConfirmGoBackCommand);
            ConfirmGoBackCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion



        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }


    }
}
