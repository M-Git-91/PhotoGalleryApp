using PhotoGalleryApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalleryApp.ViewModels
{
    public class CreatePhotoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; }
        public AlbumNumber AlbumCategory { get; set; }
    }
}
