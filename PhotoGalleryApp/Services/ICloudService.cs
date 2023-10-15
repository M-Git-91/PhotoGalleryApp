using CloudinaryDotNet.Actions;

namespace PhotoGalleryApp.Services
{
    public interface ICloudService
    {
        ImageUploadResult AddPhoto(IFormFile file);
        DeletionResult DeletePhoto(string publicId);

    }
}
