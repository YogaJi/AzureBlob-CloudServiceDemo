using AzureBlob.Models;
using AzureBlob.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Text = AzureBlob.Models.Text;

namespace AzureBlob.ViewModels
{
    public class TextBaseViewModel : BaseViewModel
    {
     public ITextDataStore<Text> TextDataStore => DependencyService.Get<ITextDataStore<Text>>();
     }

}
