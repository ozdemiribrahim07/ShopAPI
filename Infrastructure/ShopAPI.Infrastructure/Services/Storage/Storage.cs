using ShopAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services.Storage.Storage
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string regulatedName = NameOperation.CharacterRegulatory(nameWithoutExtension);
            string timeStamp = DateTime.Now.ToString("yyyyMMdd");
            string newFileName = $"{regulatedName}_{timeStamp}{extension}";
            return await Task.FromResult(newFileName);
        }
    }
}
