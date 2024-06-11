using Microsoft.AspNetCore.Http;
using ShopAPI.Application.Abstraction.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services.Storage.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage storage;
        public StorageService(IStorage storage)
        {
            this.storage = storage;
        }

        public string StorageName  { get => storage.GetType().Name;}

        public async Task DeleteAsync(string pathOrbucketName, string fileName)
                => await storage.DeleteAsync(pathOrbucketName, fileName);


        public async Task<List<string>> GetFiles(string pathOrbucketName)
            =>  await storage.GetFiles(pathOrbucketName);


        public async Task<bool> HasFile(string pathOrbucketName, string fileName)
            =>  await storage.HasFile(pathOrbucketName, fileName);


        public Task<List<(string fileName, string pathOrbucketName)>> UploadAsync(string pathOrbucketName, IFormFileCollection files)
            => storage.UploadAsync(pathOrbucketName, files);
    }
}
