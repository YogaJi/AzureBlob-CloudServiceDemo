using AzureBlob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlob.Services
{
    public interface ITextDataStore<T>
    {
        Task<IEnumerable<Text>> GetTexts();
        Task<Text> GetText();
        Task AddText(Text text);
        Task UpdateText(Text text);
    }
}
