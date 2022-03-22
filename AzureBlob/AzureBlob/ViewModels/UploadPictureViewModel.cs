using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MvvmHelpers;
using AzureBlob.Models;
using AzureBlob.Services;
using System.IO;
using MvvmHelpers.Commands;
using Image = AzureBlob.Models.Image;
using System.Threading.Tasks;

namespace AzureBlob.ViewModels
{
    public class UploadPictureViewModel : UploadBaseViewModel
    {
        public ObservableRangeCollection<Image> Images { get; set; }
        public AsyncCommand UploadPictureCommand { get; }
        public AsyncCommand DownloadPictureCommand { get; }

        private Uri downloadPicture;
        public Uri DownloadPicture
        {
            get => downloadPicture;
            set => SetProperty(ref downloadPicture, value);
        }

        public UploadPictureViewModel()
        {
            UploadPictureCommand = new AsyncCommand(Upload);
            DownloadPictureCommand = new AsyncCommand(Download);
        }
        async Task Upload()
        {
            Image image = new Image
            { Uri = new Uri("https://image.shutterstock.com/image-photo/picture-beautiful-view-birds-260nw-1836263689.jpg"), Title = "default picture" };
            await ImageDataStore.AddImage(image);
        }
        async Task Download()
        {

            //Images.Clear();
            //load
            LoadImages();
        }
        public async void LoadImages()
        {
            //IEnumerable<Image> images = await ImageDataStore.GetImages();
            //Images.AddRange(images);
            Image image = await ImageDataStore.GetImage();

            DownloadPicture = image.Uri;
           
        }
    }
}