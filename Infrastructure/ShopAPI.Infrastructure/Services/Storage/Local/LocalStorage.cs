using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopAPI.Application.Abstraction.Storage;
using ShopAPI.Application.Abstraction.Storage.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services.Storage.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task DeleteAsync(string path, string fileName)
        => File.Delete(Path.Combine(path, fileName));



        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(x => x.Name).ToList();
        }



        public bool HasFile(string path, string fileName)
            => Directory.Exists(Path.Combine(path, Path.GetFileName(fileName)));



        private async Task<bool> CopyFileAsync(string sourcePath, IFormFile file)
        {
            try
            {
                using (FileStream stream = new FileStream(sourcePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false))
                {
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public async Task<List<(string fileName, string pathOrbucketName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> uploadedFiles = new List<(string fileName, string path)>();

            foreach (IFormFile file in files)
            {
                string newName = await FileRenameAsync(file.FileName);
                string filePath = Path.Combine(uploadPath, file.Name);
                bool isCopied = await CopyFileAsync(filePath, file);

                if (isCopied)
                {
                    uploadedFiles.Add((file.Name, path));
                }
                else
                {
                    throw new Exception("Dosya kopyalanamadı!");
                }
            }

            return uploadedFiles;
        }

        Task<List<string>> IStorage.GetFiles(string pathOrbucketName)
        {
            throw new NotImplementedException();
        }

        Task<bool> IStorage.HasFile(string pathOrbucketName, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
