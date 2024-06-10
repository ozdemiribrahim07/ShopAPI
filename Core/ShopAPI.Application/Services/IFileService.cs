using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files, string path);
        Task<string> FileRenameAsync(string fileName);
        Task<bool> CopyFileAsync(string sourcePath, IFormFile file);
    }
}
