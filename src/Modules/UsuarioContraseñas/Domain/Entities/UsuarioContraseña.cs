using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.UsuarioContraseñas.Domain.Entities
{
    public class UsuarioContraseña
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Contraseña { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
    
}