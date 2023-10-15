using CloudinaryDotNet.Actions;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Enums;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.ViewModels;

namespace PhotoGalleryApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly ICloudService _cloudService;

        public PhotoService(DataContext context, ICloudService cloudService)
        {
            _context = context;
            _cloudService = cloudService;
        }

        public void AddPhotoToDb(CreatePhotoViewModel requestVM)
        {

            var result = _cloudService.AddPhoto(requestVM.Image);

            var newPhoto = new Photo
            {
                Title = requestVM.Title,
                URL = result.Url.ToString(),
                AlbumCategory = requestVM.AlbumCategory,
            };

            _context.Add(newPhoto);
        }

        public void DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (!string.IsNullOrEmpty(photo.URL))
            {
                _cloudService.DeletePhoto(photo.URL);
            }
            _context.Photos.Remove(photo);
        }

        public Photo? FindPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return null;
            }
            else
            {
                return photo;
            }
        }

        public Photo? FindPhotoNoTracking(int id)
        {
            var photo = _context.Photos.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return null;
            }
            else
            {
                return photo;
            }
        }

        public IEnumerable<Photo> GetPhotosByAlbumName(AlbumName albumNumber)
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == albumNumber);
            return photos;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdatePhoto(int id, EditPhotoViewModel requestVM, ImageUploadResult photoResult)
        {

            var newPhoto = new Photo
            {
                Id = requestVM.Id,
                Title = requestVM.Title,
                AlbumCategory = requestVM.AlbumCategory,
                URL = photoResult.Url.ToString()
            };

            _context.Photos.Update(newPhoto);
        }       
    }
};

