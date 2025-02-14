
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPractica.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {

        }

        public DbSet<Libro> equipos { get; set; }
        public DbSet<Autor> equipos { get; set; }
    }
}
