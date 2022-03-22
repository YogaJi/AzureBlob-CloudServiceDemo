using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MvvmHelpers;
using AzureBlob.Models;
using AzureBlob.Services;
using Image = AzureBlob.Models.Image;

namespace AzureBlob.ViewModels
{
    public class UploadBaseViewModel : BaseViewModel
    {
        public IDataStore<Image> ImageDataStore => DependencyService.Get<IDataStore<Image>>();

    }
}
