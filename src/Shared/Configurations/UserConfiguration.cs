using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.UsuarioIntereses.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Shared.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Configuración de propiedades string
            builder.Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Genero)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(u => u.Carrera)
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(u => u.FrasePerfil)
                .HasMaxLength(500)
                .IsRequired(false);

            // Configuración de propiedades numéricas
            builder.Property(u => u.Edad)
                .IsRequired();

            builder.Property(u => u.CreditosInteraccion)
                .HasDefaultValue(10);

            builder.Property(u => u.LikesRecibidos)
                .HasDefaultValue(0);

            // Configuración de fechas
            builder.Property(u => u.FechaRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("NOW()")
                .IsRequired();

            builder.Property(u => u.UltimaRecargaCreditos)
                .HasColumnType("datetime")
                .IsRequired();

            // Configuración de boolean
            builder.Property(u => u.Activo)
                .HasDefaultValue(true);

            // Configuración para List<string> - convertir a JSON string
            builder.Property(u => u.Intereses)
                .HasConversion(
                    v => string.Join(";", v), // Separar con punto y coma
                    v => (ICollection<UsuarioInteres>)(string.IsNullOrEmpty(v) 
                        ? new List<UsuarioInteres>() 
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(i => new UsuarioInteres { Interes = i }).ToList())
                )
                .HasColumnName("InteresesJson")
                .HasMaxLength(1000);

            // Índices para mejorar rendimiento
            builder.HasIndex(u => u.Nombre);
            builder.HasIndex(u => u.Carrera);
            builder.HasIndex(u => u.FechaRegistro);
        }
    }
}