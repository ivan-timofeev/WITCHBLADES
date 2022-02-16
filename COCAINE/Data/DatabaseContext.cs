using COCAINE.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace COCAINE.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<MusicLabel> Labels { get; set; }
        public DbSet<Album> Albums { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
