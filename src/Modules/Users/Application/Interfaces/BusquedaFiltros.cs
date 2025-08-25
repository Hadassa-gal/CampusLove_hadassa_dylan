using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces
{
    public class BusquedaFiltros
    {
        public int UsuarioId { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public Genero? Genero { get; set; }
        public string? Carrera { get; set; }
        public List<string>? Intereses { get; set; }
        public int? Limite { get; set; } = 50;
    }
}