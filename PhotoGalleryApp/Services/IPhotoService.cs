using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Enums;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.ViewModels;

namespace PhotoGalleryApp.Services
{
    public interface IPhotoService
    {
        public IEnumerable<Photo> GetPhotosByAlbumName(AlbumName albumNumber); 
        public void AddPhotoToDb(CreatePhotoViewModel requestVM);
        public void DeletePhoto(int id);
        public void UpdatePhoto(int id, EditPhotoViewModel requestVM, ImageUploadResult photoResult);
        public void Save();
        public Photo? FindPhoto(int id);
        public Photo? FindPhotoNoTracking(int id);
    }
}
