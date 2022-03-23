using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MvvmHelpers;
using AzureBlob.Models;
using AzureBlob.Services;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Diagnostics;
using Text = AzureBlob.Models.Text;

namespace AzureBlob.ViewModels
{
    public class UploadTextFileViewModel : TextBaseViewModel
    {
        public ObservableRangeCollection<Text> Texts { get; set; }
        public AsyncCommand UploadTextFileCommand { get; }
        public AsyncCommand DownloadTextFileCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        public string uploadTextFile;
        public string UploadTextFile
        {
            get => uploadTextFile;
            set => SetProperty(ref uploadTextFile, value);
        }

        public UploadTextFileViewModel()
        {
            LoadTexts();
            Texts = new ObservableRangeCollection<Text>();
            UploadTextFileCommand = new AsyncCommand(Upload);
            DownloadTextFileCommand = new AsyncCommand(Download);
            RefreshCommand = new AsyncCommand(Refresh);
        }
        private async Task Refresh()
        {
            IsBusy = true;

            Texts.Clear();
            LoadTexts();

            IsBusy = false;
        }
        private string content;


        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        async Task Upload()
        {
            Text text = new Text { Content = UploadTextFile };
            await TextDataStore.AddText(text);
        }
        async Task Download()
        {
            Refresh();
            LoadTexts();
        }
        public async void LoadTexts()
        {
            try
            {
                IEnumerable<Text> texts = await TextDataStore.GetTexts();
                Texts.AddRange(texts);
                //Text text = await TextDataStore.GetText();
                Text text = await TextDataStore.GetText();
                //JobId = job.Id;
                Content = text.Content;
                //DownloadTextFile = text.Content;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }


    }

    }
