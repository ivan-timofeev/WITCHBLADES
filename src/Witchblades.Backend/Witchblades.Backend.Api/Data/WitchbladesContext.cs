#nullable disable
using Microsoft.EntityFrameworkCore;
using Witchblades.Backend.Models;

namespace Witchblades.Backend.Data
{
    public class WitchbladesContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<MusicLabel> Labels { get; set; }
        public DbSet<Album> Albums { get; set; }

        public WitchbladesContext(DbContextOptions<WitchbladesContext> options)
            : base(options)
        {
        }
    }
}