using ECommerceAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService(IWebHostEnvironment _webHostEnvironment) : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment = _webHostEnvironment;

        public async Task UploadAsync(string path, IFormFileCollection files)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var fullPath = Path.Combine(uploadPath, Path.GetFileName(file.FileName));
                using FileStream fileStream = new(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
