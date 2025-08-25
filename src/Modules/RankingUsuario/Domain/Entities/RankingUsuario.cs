using System.Data;

namespace CampusLove_hadassa_dylan.src.Modules.RankingUsuario.Domain.Entities
{
    public class RankingUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int LikesRecibidos { get; set; }
        public string Carrera { get; set; }
        public int TotalMatches { get; set; }
    }
}