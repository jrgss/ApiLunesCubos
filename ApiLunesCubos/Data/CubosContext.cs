using ApiLunesCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLunesCubos.Data
{
    public class CubosContext:DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }
        public DbSet<Cubo>Cubos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CompraCubos> CompraCubos { get; set; }
    }
}
