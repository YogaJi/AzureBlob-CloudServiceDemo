using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AzureBlob.Services;

namespace AzureBlob
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<UploadBlobStorage>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
