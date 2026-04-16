using Microsoft.AspNetCore.Http;

namespace RestaurantAlloraProjectWeb.Contracts
{
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile? imageFile, string name);
    }
}
