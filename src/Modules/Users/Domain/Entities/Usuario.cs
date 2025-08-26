using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Matches.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities
{
    public enum Genero
    {
        Masculino,
        Femenino,
        Otro
    }
    public enum TipoDocumento
    {
        CC,
        CE, 
        TI,
        Pasaporte,
        Otro
    }
    public class Usuario
    {
        public int Id { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string Carrera { get; set; } = string.Empty;
        public string? FrasePerfil { get; set; }
        public int CreditosInteraccion { get; set; } = 10;
        public DateTime UltimaRecargaCreditos { get; set; } = DateTime.Now.Date;
        public int LikesRecibidos { get; set; } = 0;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        // Propiedades de navegación
        public virtual ICollection<UsuarioInteres> Intereses { get; set; } = new List<UsuarioInteres>();
        public virtual ICollection<Interaccion> InteraccionesRealizadas { get; set; } = new List<Interaccion>();
        public virtual ICollection<Interaccion> InteraccionesRecibidas { get; set; } = new List<Interaccion>();
        public virtual ICollection<Match> MatchesComoUsuario1 { get; set; } = new List<Match>();
        public virtual ICollection<Match> MatchesComoUsuario2 { get; set; } = new List<Match>();
    }
}