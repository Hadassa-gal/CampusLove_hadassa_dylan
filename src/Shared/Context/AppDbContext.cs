using Microsoft.EntityFrameworkCore;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Shared.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            
            // Configuraciones adicionales si es necesario
            base.OnModelCreating(modelBuilder);
        }

        // Método para sembrar datos de prueba
        public void SeedData()
        {
            if (!Users.Any())
            {
                var usuarios = new List<User>
                {
                    new User("Ana García", 20, "Femenino", "Ingeniería de Sistemas", "Amante de la tecnología y el café ☕")
                    {
                        Intereses = new List<string> { "Programación", "Gaming", "Música" }
                    },
                    new User("Carlos López", 22, "Masculino", "Administración", "Emprendedor nato 🚀")
                    {
                        Intereses = new List<string> { "Negocios", "Deportes", "Viajes" }
                    },
                    new User("María Rodríguez", 21, "Femenino", "Psicología", "Explorando la mente humana 🧠")
                    {
                        Intereses = new List<string> { "Lectura", "Arte", "Psicología" }
                    }
                };

                Users.AddRange(usuarios);
                SaveChanges();
            }
        }
    }
}