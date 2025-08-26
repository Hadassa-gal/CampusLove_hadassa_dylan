using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.DTOs
{
    public class CrearUsuarioRequest
    {
        [Required]
        public TipoDocumento TipoDocumento { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Range(15, 45)]
        public int Edad { get; set; }

        [Required]
        public Genero Genero { get; set; }

        [Required]
        [MaxLength(100)]
        public string Carrera { get; set; } = string.Empty;

        public string? FrasePerfil { get; set; }

        [Required]
        public string Contraseña { get; set; } = string.Empty;

        public List<string>? Intereses { get; set; }
    }
}