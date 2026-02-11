using Microsoft.EntityFrameworkCore;
using TechCity.Entidades;

namespace TechCity
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Articulo> Articulos { get; set; }
    }
}
