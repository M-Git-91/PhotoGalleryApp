using Microsoft.EntityFrameworkCore;
using PhotoGalleryApp.Models;

namespace PhotoGalleryApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=PC-MIRO1\\SQLEXPRESS;Initial Catalog=PhotoGalleryApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        */

        public DbSet<Photo> Photos { get; set; }
    }
}
