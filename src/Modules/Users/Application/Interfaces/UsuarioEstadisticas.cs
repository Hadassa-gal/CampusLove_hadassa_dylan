using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces
{
    public class UsuarioEstadisticas
    {
        public int UsuarioId { get; set; }
        public int LikesRecibidos { get; set; }
        public int LikesEnviados { get; set; }
        public int TotalMatches { get; set; }
        public int TotalIntereses { get; set; }
        public int CreditosDisponibles { get; set; }
    }
}