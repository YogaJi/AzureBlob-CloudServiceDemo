using AzureBlob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureBlob.Services
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<Image>> GetImages();
        Task<Image> GetImage();
        Task AddImage(Image image);
        Task UpdateImage(Image image);

    }
}
