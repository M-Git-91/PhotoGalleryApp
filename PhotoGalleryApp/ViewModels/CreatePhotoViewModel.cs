using PhotoGalleryApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalleryApp.ViewModels
{
    public class CreatePhotoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Album is required")]
        public AlbumName AlbumCategory { get; set; }
    }
}
