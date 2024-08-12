using Microsoft.EntityFrameworkCore;

namespace ProyectoDAW.Models
{
    public class RestauranteDbContext : DbContext
    {
        

        public RestauranteDbContext(DbContextOptions <RestauranteDbContext> options) : base(options)
        {
            
        }

        public DbSet <Carrito> carritos { get; set; }
        public DbSet <Categoria> categorias { get; set; }
        public DbSet <Compra> compras { get; set; }
        public DbSet<Facturacion> facturacions { get; set; }
        public DbSet <Pago> pagos { get; set; } 
        public DbSet <Perfil> perfils {  get; set; }
        public DbSet <Producto> productos { get; set; }
        public DbSet <Resena> resenas { get; set; }
        public DbSet <Usuario> usuarios { get; set; }
        public DbSet<Inventario> inventario { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resena>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Facturacion>()
                .HasOne(f => f.Usuario)
                .WithMany()
                .HasForeignKey(f => f.UsuarioId);
        }

    }
}
