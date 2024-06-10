using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopAPI.Application.Services;
using ShopAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<bool> CopyFileAsync(string sourcePath, IFormFile file)
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



        public async Task<string> FileRenameAsync(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string regulatedName = NameOperation.CharacterRegulatory(nameWithoutExtension);
            string timeStamp = DateTime.Now.ToString("yyyyMMdd");
            string newFileName = $"{regulatedName}_{timeStamp}{extension}";
            return await Task.FromResult(newFileName);
        }




        public async Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, string path)
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
                string filePath = Path.Combine(uploadPath, newName);
                bool isCopied = await CopyFileAsync(filePath, file);

                if (isCopied)
                {
                    uploadedFiles.Add((newName, filePath));
                }
                else
                {
                    throw new Exception("Dosya kopyalanamadı!");
                }
            }

            return uploadedFiles;
        }
    }
}
