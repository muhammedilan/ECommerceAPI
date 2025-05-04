using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage(IWebHostEnvironment _webHostEnvironment) : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment = _webHostEnvironment;

        public async Task DeleteAsync(string path, string fileName)
            => File.Delete(Path.Combine(path, fileName));

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists(Path.Combine(path, fileName));

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);

                using FileStream fileStream = new(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                datas.Add((fileName, Path.Combine(path, fileName)));
            }

            return datas;
        }
    }
}
