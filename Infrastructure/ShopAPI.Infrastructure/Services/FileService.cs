using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services
{
    public class FileService
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;
        //public FileService(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}


       




        //public async Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, string path)
        //{
        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        //    if (!Directory.Exists(uploadPath))
        //    {
        //        Directory.CreateDirectory(uploadPath);
        //    }

        //    List<(string fileName, string path)> uploadedFiles = new List<(string fileName, string path)>();

        //    foreach (IFormFile file in files)
        //    {
        //        string newName = await FileRenameAsync(file.FileName);
        //        string filePath = Path.Combine(uploadPath, newName);
        //        bool isCopied = await CopyFileAsync(filePath, file);

        //        if (isCopied)
        //        {
        //            uploadedFiles.Add((newName, path));
        //        }
        //        else
        //        {
        //            throw new Exception("Dosya kopyalanamadı!");
        //        }
        //    }

        //    return uploadedFiles;
        //}
    }
}
