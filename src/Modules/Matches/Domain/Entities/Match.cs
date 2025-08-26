using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Matches.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int Compatibilidad { get; set; }
        public DateTime FechaMatch { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        public int Usuario1Id { get; set; }
        public Usuario? Usuario1 { get; set; }
        public int Usuario2Id { get; set; }
        public Usuario? Usuario2 { get; set; }
    }
}