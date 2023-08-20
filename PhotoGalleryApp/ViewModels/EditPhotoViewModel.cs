using PhotoGalleryApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalleryApp.ViewModels
{
    public class EditPhotoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? URL { get; set; }
        public IFormFile Image { get; set; }
        public AlbumNumber AlbumCategory { get; set; }
    }
}
