using System.ComponentModel.DataAnnotations;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        public int Edad { get; set; }
        
        [MaxLength(20)]
        public string Genero { get; set; } = string.Empty;
        
        [MaxLength(150)]
        public string Carrera { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string FrasePerfil { get; set; } = string.Empty;
        
        public int CreditosInteraccion { get; set; }
        public DateTime UltimaRecargaCreditos { get; set; }
        public int LikesRecibidos { get; set; }
        
        // Inicializada directamente en la propiedad
        public List<string> Intereses { get; set; } = new List<string>();
        
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; } = true;

        // Constructor vacío para EF
        public User()
        {
            FechaRegistro = DateTime.Now;
            UltimaRecargaCreditos = DateTime.Now;
        }

        // Constructor con parámetros
        public User(string nombre, int edad, string genero, string carrera, string frasePerfil = "")
            : this()
        {
            Nombre = nombre;
            Edad = edad;
            Genero = genero;
            Carrera = carrera;
            FrasePerfil = frasePerfil;
            CreditosInteraccion = 10; // Créditos iniciales
        }

        // Método para agregar intereses
        public void AgregarInteres(string interes)
        {
            if (!string.IsNullOrWhiteSpace(interes) && !Intereses.Contains(interes))
            {
                Intereses.Add(interes);
            }
        }

        // Método para usar créditos
        public bool UsarCredito()
        {
            if (CreditosInteraccion > 0)
            {
                CreditosInteraccion--;
                return true;
            }
            return false;
        }

        // Método para recargar créditos
        public void RecargarCreditos(int cantidad)
        {
            CreditosInteraccion += cantidad;
            UltimaRecargaCreditos = DateTime.Now;
        }

        public override string ToString()
        {
            return $"User: {Nombre}, {Edad} años, {Carrera}, Créditos: {CreditosInteraccion}";
        }
    }
}