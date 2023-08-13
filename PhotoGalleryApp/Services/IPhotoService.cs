using CloudinaryDotNet.Actions;

namespace PhotoGalleryApp.Services
{
    public interface IPhotoService
    {
        ImageUploadResult AddPhoto(IFormFile file);
        DeletionResult DeletePhoto(string publicId);

    }
}
