using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using AzureBlob.Models;
using AzureBlob.Services;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Diagnostics;
using Text = AzureBlob.Models.Text;
using Xamarin.Essentials;
using Command = Xamarin.Forms.Command;

namespace AzureBlob.ViewModels
{
    public class UploadTextViewModel : TextBaseViewModel
    {
        public ObservableRangeCollection<Text> Texts { get; set; }
        public AsyncCommand UploadTextCommand { get; }
        public AsyncCommand DownloadTextCommand { get; }

        private string downloadText;
        public string DownloadText
        {
            get => downloadText;
            set => SetProperty(ref downloadText, value);
        }
        public UploadTextViewModel()
        {
            
            UploadTextCommand = new AsyncCommand(Upload);
            DownloadTextCommand = new AsyncCommand(Download);
        }

        private string content;


        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        async Task Upload()
        {
            Text text = new Text{ Id = 1, Content = "" };
            await TextDataStore.AddText(text);
        }
        async Task Download()
        {
            LoadTexts();
        }
        public async void LoadTexts()
        {
            try
            {
                Text text = await TextDataStore.GetText();

                DownloadText = text.Content;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

 
    }
    
}