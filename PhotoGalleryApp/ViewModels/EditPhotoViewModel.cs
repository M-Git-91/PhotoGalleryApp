using PhotoGalleryApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalleryApp.ViewModels
{
    public class EditPhotoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string? URL { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public AlbumName AlbumCategory { get; set; }
    }
}
