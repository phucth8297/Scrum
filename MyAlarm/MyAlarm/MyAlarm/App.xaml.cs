﻿using Prism;
using Prism.Ioc;
using MyAlarm.ViewModels;
using MyAlarm.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System;
using System.Reflection;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyAlarm
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }
        public string DbConnectionString { get; set; } = "";
        public string DbPath { get; set; } = "";

        
        protected override async void OnInitialized()
        {
            InitializeComponent();

            DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.db");
            DbConnectionString = $"Data Source={DbPath};";

            //File.Delete(DbPath);
            if (File.Exists(DbPath) == false)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("MyAlarm.File.data.db");

                using (var reader = new System.IO.MemoryStream())
                {
                    await stream.CopyToAsync(reader);
                    File.WriteAllBytes(DbPath, reader.GetBuffer());
                }
            }
            await NavigationService.NavigateAsync("NavigationPage/VBS_TrangChuPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<VBS_TrangChuPage, VBS_TrangChuPageViewModel>();
            containerRegistry.RegisterForNavigation<VBS_01Page>();
            containerRegistry.RegisterForNavigation<VBS_AddMemberPage>();
            containerRegistry.RegisterForNavigation<VBS_MemberDetailPage>();
            containerRegistry.RegisterForNavigation<VBS_MemberPage>();
            containerRegistry.RegisterForNavigation<VBS_WorkPage>();

        }
    }
}
