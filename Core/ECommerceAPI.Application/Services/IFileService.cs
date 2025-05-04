using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    }
}
