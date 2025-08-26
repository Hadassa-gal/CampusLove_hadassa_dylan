using Microsoft.EntityFrameworkCore;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Matches.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.UsuarioContraseñas.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Shared.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioContraseña> UsuarioContraseñas { get; set; }
        public DbSet<UsuarioInteres> UsuarioIntereses { get; set; }
        public DbSet<Interaccion> Interacciones { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Edad).IsRequired();
                entity.Property(e => e.Carrera).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Genero).HasConversion<string>();
                
                // Configurar relaciones
                entity.HasMany(e => e.Intereses)
                      .WithOne(e => e.Usuario)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.InteraccionesRealizadas)
                      .WithOne(e => e.Usuario)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.InteraccionesRecibidas)
                      .WithOne(e => e.UsuarioObjetivo)
                      .HasForeignKey(e => e.UsuarioObjetivoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // Configuración UsuarioContraseña
            modelBuilder.Entity<UsuarioContraseña>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.UsuarioId).IsUnique();
                entity.Property(e => e.Contraseña).IsRequired().HasMaxLength(255);
            });
            // Configuración de UsuarioInteres
            modelBuilder.Entity<UsuarioInteres>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.UsuarioId, e.Interes }).IsUnique();
                entity.Property(e => e.Interes).IsRequired().HasMaxLength(50);
            });

            // Configuración de Interaccion
            modelBuilder.Entity<Interaccion>( entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.UsuarioId, e.UsuarioObjetivoId }).IsUnique();
                entity.Property(e => e.TipoInteraccion).HasConversion<string>();

                // Verificar que usuario no interactúe consigo mismo
                entity.HasCheckConstraint("CK_Interaccion_DifferentUsers", "usuario_id != usuario_objetivo_id");
            });

            // Configuración de Match
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Usuario1Id, e.Usuario2Id }).IsUnique();
                
                entity.HasOne(e => e.Usuario1)
                      .WithMany(e => e.MatchesComoUsuario1)
                      .HasForeignKey(e => e.Usuario1Id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Usuario2)
                      .WithMany(e => e.MatchesComoUsuario2)
                      .HasForeignKey(e => e.Usuario2Id)
                      .OnDelete(DeleteBehavior.Restrict);

                // Verificar que los usuarios del match sean diferentes
                entity.HasCheckConstraint("CK_Match_DifferentUsers", "usuario1_id != usuario2_id");
                
                // Verificar rango de porcentaje de compatibilidad
                entity.HasCheckConstraint("CK_Match_CompatibilityRange", "porcentaje_compatibilidad BETWEEN 0 AND 100");
            });
        }
    }
}