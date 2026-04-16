using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RestaurantAlloraProjectWeb.Contracts;
using RestaurantAlloraProjectWeb.Helpers;

namespace RestaurantAlloraProjectWeb.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _settings;

        public ImageService(Cloudinary cloudinary, IOptions<CloudinarySettings> settings)
        {
            _cloudinary = cloudinary;
            _settings = settings.Value;
        }

        public async Task<string?> UploadImageAsync(IFormFile? imageFile, string name)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(_settings.CloudName)
                || string.IsNullOrWhiteSpace(_settings.ApiKey)
                || string.IsNullOrWhiteSpace(_settings.ApiSecret))
            {
                throw new InvalidOperationException("Попълни CloudinarySettings в appsettings.json преди качване на снимки.");
            }

            await using var stream = imageFile.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(name, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new InvalidOperationException(uploadResult.Error.Message);
            }

            return uploadResult.SecureUrl?.ToString();
        }
    }
}
