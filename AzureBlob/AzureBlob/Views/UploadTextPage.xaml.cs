using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlob.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureBlob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadTextPage : ContentPage
    {
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        private static string ConnectionString => "DefaultEndpointsProtocol=https;AccountName=a00246407;AccountKey=/lVtRlumnz9W3WqmLDkcZv1zcVux84Hb1pK1kLy0PFbNRqUFN22ECKB8PlVdEzzd4qmFL5mBEWwSgqF90HCl9Q==;EndpointSuffix=core.windows.net";
        private static string Container => "picture";
        private static string FileName => "text2.json";

        BlobServiceClient client;
        BlobContainerClient containerClient;
        BlobClient blobClient;


        private async void Button_Clicked(object sender, EventArgs e)
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
            { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
            { DevicePlatform.Android, new[] { "*/*" } },
                });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = default,
            };
            var result = await FilePicker.PickAsync(options);
            var streams = await result.OpenReadAsync();
            var text2 = streams.ToString();

            if (streams != null) {
                var jsonString = JsonConvert.SerializeObject(text2);

                var stream = new MemoryStream();

                var writer = new StreamWriter(stream);
                writer.Write(jsonString);
                writer.Flush();

                stream.Position = 0;

                BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

                await blob.UploadAsync(stream, true, default);

                resultsLabel.Text += "Blob Uploaded\n";

                uploadButton.IsEnabled = false;
                //listButton.IsEnabled = true;
            }

        }

 

        public UploadTextPage()
        {
            InitializeComponent();
            

        }
        protected async override void OnAppearing()
        {

            uploadButton.IsEnabled = true;
        }
        async void Download_Clicked(object sender, EventArgs e)
        {
  
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;

                var jsonString = new StreamReader(stream).ReadToEnd();
                Console.WriteLine(jsonString);

                //var texts = JsonConvert.DeserializeObject<List<Text>>(jsonString);

                resultsLabel.Text = jsonString;

            }
            else
            {
                resultsLabel.Text = "default";
            }
  
        }
    }
}
