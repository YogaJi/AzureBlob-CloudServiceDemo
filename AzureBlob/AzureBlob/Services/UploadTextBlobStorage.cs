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
    class UploadTextBlobStorage : ITextDataStore<Text>
    {
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        private static string ConnectionString => "DefaultEndpointsProtocol=https;AccountName=a00246407;AccountKey=/lVtRlumnz9W3WqmLDkcZv1zcVux84Hb1pK1kLy0PFbNRqUFN22ECKB8PlVdEzzd4qmFL5mBEWwSgqF90HCl9Q==;EndpointSuffix=core.windows.net";
        private static string Container => "picture";
        private static string FileName => "text.json";

        public async Task WriteFileText(List<Text> texts)
        {
            var jsonString = JsonConvert.SerializeObject(texts);

            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write(jsonString);
            writer.Flush();

            stream.Position = 0;

            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            await blob.UploadAsync(stream, true, default);
        }
        public async Task<List<Text>> ReadFileText()
        {
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;

                var jsonString = new StreamReader(stream).ReadToEnd();

                var texts = JsonConvert.DeserializeObject<List<Text>>(jsonString);

                return texts;

            }
            else
            {
                var defaultText = DefaultText();

                await WriteFileText(defaultText);

                return defaultText;
            }
        }

        private List<Text> DefaultText()
        {
            var texts = new List<Text>()
            {
                new Text { Id = "1", Content = "default content" }
            };

            return texts;
        }

        public Task AddText(Text text)
        {
            throw new NotImplementedException();
        }

        public Task<Text> GetText()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Text>> GetTexts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateText(Text text)
        {
            throw new NotImplementedException();
        }
    }
}
