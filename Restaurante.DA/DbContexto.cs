using Microsoft.EntityFrameworkCore;

namespace Restaurante.DA
{
    public class DbContexto:DbContext
    {

        public DbContexto(DbContextOptions<DbContexto> opciones) : base(opciones){}

        public DbSet<Restaurante.Model.Ingredientes> Ingredientes { get; set; }

        public DbSet<Restaurante.Model.Medidas> Medidas { get; set; }

        public DbSet<Restaurante.Model.Menu> Menu { get; set; }

        public DbSet<Restaurante.Model.MenuIngredientes> MenuIngredientes { get; set; }

        public DbSet<Restaurante.Model.MesaOrden> MesaOrden { get; set; }

        public DbSet<Restaurante.Model.Mesas> Mesas { get; set; }
    }
}