using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureBlob.ViewModels;
using AzureBlob.Views;
using Xamarin.Forms;

namespace AzureBlob
{
 
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UploadPicturePage), typeof(UploadPicturePage));
            Routing.RegisterRoute(nameof(UploadTextPage), typeof(UploadTextPage));
            Routing.RegisterRoute(nameof(UploadTextFilePage), typeof(UploadTextFilePage));
        }
    }
}