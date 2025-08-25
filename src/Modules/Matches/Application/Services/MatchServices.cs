using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CampusLove_hadassa_dylan.src.Modules.Matches.Domain.Entities;
using CampusLove_hadassa_dylan.src.Shared.Context;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Matches.Application.Services
{
    public class MatchServices
    {
        private readonly AppDbContext _context;

        public MatchServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Match?> CrearMatchAsync(int usuario1Id, int usuario2Id)
        {
            // Verificar que ambos usuarios se han dado like mutuamente
            var like1 = await _context.Interacciones
                .FirstOrDefaultAsync(i => i.UsuarioId == usuario1Id &&
                                         i.UsuarioObjetivoId == usuario2Id &&
                                         i.TipoInteraccion == TipoInteraccion.Like);

            var like2 = await _context.Interacciones
                .FirstOrDefaultAsync(i => i.UsuarioId == usuario2Id &&
                                         i.UsuarioObjetivoId == usuario1Id &&
                                         i.TipoInteraccion == TipoInteraccion.Like);

            if (like1 != null && like2 != null)
            {
                // Verificar si ya existe un match entre estos usuarios
                var matchExistente = await _context.Matches
                    .FirstOrDefaultAsync(m => 
                        (m.Usuario1Id == Math.Min(usuario1Id, usuario2Id) && 
                         m.Usuario2Id == Math.Max(usuario1Id, usuario2Id)) ||
                        (m.Usuario1Id == Math.Max(usuario1Id, usuario2Id) && 
                         m.Usuario2Id == Math.Min(usuario1Id, usuario2Id)));

                if (matchExistente != null)
                {
                    return matchExistente; // Ya existe el match
                }

                // Calcular porcentaje de compatibilidad basado en intereses comunes
                var porcentajeCompatibilidad = await CalcularCompatibilidadAsync(usuario1Id, usuario2Id);

                var match = new Match
                {
                    Usuario1Id = Math.Min(usuario1Id, usuario2Id), // Siempre el menor ID primero
                    Usuario2Id = Math.Max(usuario1Id, usuario2Id),
                    PorcentajeCompatibilidad = porcentajeCompatibilidad,
                    FechaMatch = DateTime.Now,
                    Activo = true
                };

                _context.Matches.Add(match);
                await _context.SaveChangesAsync();
                return match;
            }

            return null;
        }

        // Método para calcular compatibilidad (necesario para que compile)
        private async Task<int> CalcularCompatibilidadAsync(int usuario1Id, int usuario2Id)
        {
            var interesesUsuario1 = await _context.UsuarioIntereses
                .Where(ui => ui.UsuarioId == usuario1Id)
                .Select(ui => ui.Interes)
                .ToListAsync();

            var interesesUsuario2 = await _context.UsuarioIntereses
                .Where(ui => ui.UsuarioId == usuario2Id)
                .Select(ui => ui.Interes)
                .ToListAsync();

            if (interesesUsuario1.Count == 0 || interesesUsuario2.Count == 0)
                return 0;

            var interesesComunes = interesesUsuario1.Intersect(interesesUsuario2).Count();
            var totalIntereses = interesesUsuario1.Union(interesesUsuario2).Count();

            return (int)Math.Round((double)interesesComunes / totalIntereses * 100);
        }

        // Método adicional para obtener matches de un usuario
        public async Task<List<Match>> ObtenerMatchesUsuarioAsync(int usuarioId)
        {
            return await _context.Matches
                .Where(m => (m.Usuario1Id == usuarioId || m.Usuario2Id == usuarioId) && m.Activo)
                .Include(m => m.Usuario1)
                .Include(m => m.Usuario2)
                .OrderByDescending(m => m.FechaMatch)
                .ToListAsync();
        }

        // Método para desactivar un match
        public async Task<bool> DesactivarMatchAsync(int matchId, int usuarioId)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == matchId && 
                                         (m.Usuario1Id == usuarioId || m.Usuario2Id == usuarioId));

            if (match != null)
            {
                match.Activo = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}