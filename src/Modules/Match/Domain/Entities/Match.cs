using System.Data;

namespace CampusLove_hadassa_dylan.src.Modules.Match.Domain.Entities
{

    public class Matchs
    {
        public int Id { get; set; }
        public int Usuario1Id { get; set; }
        public int Usuario2Id { get; set; }
        public int PorcentajeCompatibilidad { get; set; }
        public DateTime FechaMatch { get; set; }
        public bool Activo { get; set; }
        public string NombreUsuario1 { get; set; }
        public string NombreUsuario2 { get; set; }
    }
}