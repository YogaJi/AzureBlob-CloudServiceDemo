using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlob.Models;
using Microsoft.Bot.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureBlob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPicturePage : ContentPage
    {
        
        public UploadPicturePage()
        {
            InitializeComponent();
   
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }
  
    }
}
