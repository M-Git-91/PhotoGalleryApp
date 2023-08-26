using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoGalleryApp.Models;

namespace PhotoGalleryApp.Data
{
    public class DataContext : IdentityDbContext<AppAdmin>
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<AppAdmin> AppAdmins { get; set; }
    }
}
