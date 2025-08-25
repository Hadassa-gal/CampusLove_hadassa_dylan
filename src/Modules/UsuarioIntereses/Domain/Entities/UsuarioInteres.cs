using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities
{
   public class UsuarioInteres
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Interes { get; set; } = string.Empty;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}