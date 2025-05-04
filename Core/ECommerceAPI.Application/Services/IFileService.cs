using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services
{
    public interface IFileService
    {
        Task UploadAsync(string path, IFormFileCollection files);
    }
}
