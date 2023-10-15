using PhotoGalleryApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGalleryApp.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]      
        public string URL { get; set; } = string.Empty;
        public AlbumName AlbumCategory { get; set; }
    }
}
