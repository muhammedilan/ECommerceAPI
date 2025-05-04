using ECommerceAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService(IWebHostEnvironment _webHostEnvironment) : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment = _webHostEnvironment;

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
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
