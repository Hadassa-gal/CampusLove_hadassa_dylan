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
        public int Usuario1Id { get; set; }
        public int Usuario2Id { get; set; }
        public int? PorcentajeCompatibilidad { get; set; }
        public DateTime FechaMatch { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        public virtual Usuario Usuario1 { get; set; } = null!;
        public virtual Usuario Usuario2 { get; set; } = null!;
    }
}