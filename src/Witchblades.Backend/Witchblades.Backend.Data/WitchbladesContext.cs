using Microsoft.EntityFrameworkCore;
using Witchblades.Backend.Models;

namespace Witchblades.Backend.Data
{
    public class WitchbladesContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; } = default!;
        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<MusicLabel> Labels { get; set; } = default!;
        public DbSet<Album> Albums { get; set; } = default!;

        public WitchbladesContext(DbContextOptions<WitchbladesContext> options)
            : base(options)
        {
        }
    }
}