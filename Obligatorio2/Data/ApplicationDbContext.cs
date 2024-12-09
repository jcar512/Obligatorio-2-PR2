using Microsoft.EntityFrameworkCore;
using Obligatorio2.Models;

namespace Obligatorio2.Data
    {
    public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

        public DbSet<Habitacion>? Habitaciones { get; set; }
        public DbSet<Hotel>? Hoteles { get; set; }
        public DbSet<Pago>? Pagos { get; set; }
        public DbSet<Pais>? Paises { get; set; }
        public DbSet<Reserva>? Reservas { get; set; }
        public DbSet<Servicio>? Servicios { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Pago)
                .WithOne(p => p.Reserva)
                .HasForeignKey<Pago>(p => p.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Pais)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(u => u.PaisId)
                .OnDelete(DeleteBehavior.NoAction);
            }
        }
    }