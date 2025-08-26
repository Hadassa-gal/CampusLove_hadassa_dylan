using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusLove_hadassa_dylan.src.Modules.Matches.Application.Strategies
{
    public interface IMatchingStrategy
    {
        int CalcularCompatibilidad(Usuario u1, Usuario u2);
    }

    public class InterestBasedMatching : IMatchingStrategy
    {
        public int CalcularCompatibilidad(Usuario u1, Usuario u2)
        {
            // Lógica basada en intereses comunes
            if (u1.Intereses == null || u2.Intereses == null || !u1.Intereses.Any() || !u2.Intereses.Any())
                return 0;

            var interesesComunes = u1.Intereses
                .Select(i => i.Interes.ToLower())
                .Intersect(u2.Intereses.Select(i => i.Interes.ToLower()))
                .Count();

            var totalIntereses = u1.Intereses.Count + u2.Intereses.Count;
            
            if (totalIntereses == 0) return 0;

            // Porcentaje de intereses comunes (máximo 50 puntos)
            return (int)((interesesComunes * 100.0 / totalIntereses) * 0.5);
        }
    }

    public class AgeBasedMatching : IMatchingStrategy
    {
        public int CalcularCompatibilidad(Usuario u1, Usuario u2)
        {
            // Lógica basada en diferencia de edad (máximo 30 puntos)
            int diferenciaEdad = Math.Abs(u1.Edad - u2.Edad);
            
            if (diferenciaEdad >= 10) return 0;
            if (diferenciaEdad >= 8) return 10;
            if (diferenciaEdad >= 5) return 20;
            if (diferenciaEdad >= 3) return 25;
            return 30; // Diferencia de 0-2 años
        }
    }

    public class CareerBasedMatching : IMatchingStrategy
    {
        public int CalcularCompatibilidad(Usuario u1, Usuario u2)
        {
            // Lógica basada en carrera (máximo 20 puntos)
            if (string.IsNullOrEmpty(u1.Carrera) || string.IsNullOrEmpty(u2.Carrera))
                return 0;

            if (u1.Carrera.Equals(u2.Carrera, StringComparison.OrdinalIgnoreCase))
                return 20; // Misma carrera

            // Carreras relacionadas (puedes expandir esta lógica según necesidades)
            var carrerasRelacionadas = new Dictionary<string, string[]>
            {
                { "Ingeniería", new[] { "Tecnología", "Ciencias", "Matemáticas" } },
                { "Medicina", new[] { "Enfermería", "Biología", "Química" } },
                { "Derecho", new[] { "Ciencias Políticas", "Sociología", "Administración" } },
                { "Arquitectura", new[] { "Diseño", "Ingeniería Civil", "Urbanismo" } }
            };

            foreach (var relacion in carrerasRelacionadas)
            {
                if ((u1.Carrera.Equals(relacion.Key, StringComparison.OrdinalIgnoreCase) && 
                     relacion.Value.Any(c => c.Equals(u2.Carrera, StringComparison.OrdinalIgnoreCase))) ||
                    (u2.Carrera.Equals(relacion.Key, StringComparison.OrdinalIgnoreCase) && 
                     relacion.Value.Any(c => c.Equals(u1.Carrera, StringComparison.OrdinalIgnoreCase))))
                {
                    return 15; // Carreras relacionadas
                }
            }

            return 5; // Carreras diferentes pero no relacionadas
        }
    }
}
