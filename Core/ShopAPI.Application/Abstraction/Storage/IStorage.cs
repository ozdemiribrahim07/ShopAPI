using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Abstraction.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrbucketName)> > UploadAsync(string pathOrbucketName, IFormFileCollection files); 
        Task DeleteAsync(string pathOrbucketName, string fileName);

        Task<List<string>> GetFiles(string pathOrbucketName);
        Task<bool> HasFile(string pathOrbucketName, string fileName);
    }
}
