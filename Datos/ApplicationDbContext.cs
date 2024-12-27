using CrudNet8MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        //Agregar los modelos aquí (Cada modelo corresponde a una tabla en la BD)
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<Producto> Producto { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación uno a muchos
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Persona)
                .WithMany(p => p.Ventas)
                .HasForeignKey(v => v.PersonaId);
            
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Productos)      // Una venta tiene muchos productos
                .WithOne(p => p.Venta)          // Cada producto pertenece a una venta
                .HasForeignKey(p => p.VentaId);
        }
    }
}
