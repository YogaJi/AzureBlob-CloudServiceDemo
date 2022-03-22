using Azure.Storage.Blobs;
using AzureBlob.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlob.Services
{
    class UploadBlobStorage : IDataStore<Image>
    {

         private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        private static string ConnectionString => "DefaultEndpointsProtocol=https;AccountName=a00246407;AccountKey=/lVtRlumnz9W3WqmLDkcZv1zcVux84Hb1pK1kLy0PFbNRqUFN22ECKB8PlVdEzzd4qmFL5mBEWwSgqF90HCl9Q==;EndpointSuffix=core.windows.net";
         private static string Container => "picture";
         private static string FileName => "bird.json";

        public async Task WriteFile(List<Image> images)
        {
            var jsonString = JsonConvert.SerializeObject(images);

            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write(jsonString);
            writer.Flush();

            stream.Position = 0;

            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            await blob.UploadAsync(stream, true, default);
        }
        public async Task<List<Image>> ReadFile()
        {
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;

                var jsonString = new StreamReader(stream).ReadToEnd();

                var images = JsonConvert.DeserializeObject<List<Image>>(jsonString);

                return images;

            }
            else
            {
                var defaultImage = GetDefaultImage();

                await WriteFile(defaultImage);

                return defaultImage;
            }
        }

        private List<Image> GetDefaultImage()
        {
            var images = new List<Image>()
            {
                new Image { Uri = new Uri("https://macaulaylibrary.org/asset/202984001?__hstc=65717809.ff7bd0d304b4f25a2fa6f112f880dc37.1647911979247.1647911979247.1647911979247.1&__hssc=65717809.3.1647911979248&__hsfp=3495170302&_gl=1*18l2zgw*_ga*MTYwNjI5MzIzMi4xNjQ3OTExOTc4*_ga_QR4NVXZ8BM*MTY0NzkxMTk3OC4xLjEuMTY0NzkxMjE1NC42MA..#_ga=2.73609874.1099179884.1647911979-1606293232.1647911978"), Title = "default picture" }
               
            };

            return images;
        }

        public async Task AddImage(Image image)
        {
            var images = await ReadFile();
            images.Add(image);

            await WriteFile(images);
        }

        public async Task<Image> GetImage()
        {
            var images = await ReadFile();

            return images[1];
        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            var images = await ReadFile();

            return images;
        }

        public async Task UpdateImage(Image image)
        {
            var images = await ReadFile();
            images[0] = image;

            await WriteFile(images);
        }
    }
}
