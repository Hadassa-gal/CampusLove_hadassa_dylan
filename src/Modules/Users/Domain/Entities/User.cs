using System.Data;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities
{
    public class Userss
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string Carrera { get; set; }
        public string FrasePerfil { get; set; }
        public int CreditosInteraccion { get; set; }
        public DateTime UltimaRecargaCreditos { get; set; }
        public int LikesRecibidos { get; set; }
        public List<string> Intereses { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public Userss(int id, int edad, string carrera, int creditosInteraccion, int likesRecibidos, DateTime fechaRegistro) 
        {
            Id = id;
            Edad = edad;
            Carrera = carrera;
            CreditosInteraccion = creditosInteraccion;
            LikesRecibidos = likesRecibidos;
            FechaRegistro = fechaRegistro;
   
        }

        public Userss()
        {
            Intereses = new List<string>();
        }
    }
}