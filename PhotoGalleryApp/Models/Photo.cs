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
        public AlbumNumber AlbumCategory { get; set; }
        public DateOnly DateTaken { get; set; }
    }
}
