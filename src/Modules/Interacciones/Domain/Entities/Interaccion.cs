using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities
{
    public enum TipoInteraccion
    {
        Like,
        Dislike
    }
    public class Interaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int UsuarioObjetivoId { get; set; }
        public TipoInteraccion TipoInteraccion { get; set; }
        public DateTime FechaInteraccion { get; set; } = DateTime.Now;
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Usuario UsuarioObjetivo { get; set; } = null!;
    }
}