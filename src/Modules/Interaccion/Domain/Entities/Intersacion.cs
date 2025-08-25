using System.Data;

namespace CampusLove_hadassa_dylan.src.Modules.Interaccion.Domain.Entities
{
    public class Interaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int UsuarioObjetivoId { get; set; }
        public TipoInteraccion Tipo { get; set; }
        public DateTime FechaInteraccion { get; set; }
    }

    public enum TipoInteraccion
    {
        Like,
        Dislike
    }
}