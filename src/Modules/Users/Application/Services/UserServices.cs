using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Services
{
    public class UserServices(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        // 1. Obtener usuarios activos por carrera
        public async Task<List<Usuario>> ObtenerUsuariosPorCarreraAsync(string carrera)
        {
            return await _context.Usuarios
                .Where(u => u.Activo && u.Carrera.Contains(carrera, StringComparison.CurrentCultureIgnoreCase))
                .Include(u => u.Intereses)
                .OrderByDescending(u => u.FechaRegistro)
                .ToListAsync();
        }

        // 2. Obtener usuarios con intereses similares
        public async Task<List<Usuario>> ObtenerUsuariosConInteresesSimilaresAsync(int usuarioId)
        {
            var interesesUsuario = await _context.UsuarioIntereses
                .Where(ui => ui.UsuarioId == usuarioId)
                .Select(ui => ui.Interes)
                .ToListAsync();

            return await _context.Usuarios
                .Where(u => u.Id != usuarioId && u.Activo)
                .Where(u => u.Intereses.Any(i => interesesUsuario.Contains(i.Interes)))
                .Include(u => u.Intereses)
                .OrderByDescending(u => u.Intereses.Count(i => interesesUsuario.Contains(i.Interes)))
                .ThenByDescending(u => u.LikesRecibidos)
                .ToListAsync();
        }

        // 3. Obtener usuarios que no han sido evaluados por el usuario actual
        public async Task<List<Usuario>> ObtenerUsuariosPendientesAsync(int usuarioId)
        {
            var usuariosEvaluados = await _context.Interacciones
                .Where(i => i.UsuarioId == usuarioId)
                .Select(i => i.UsuarioObjetivoId)
                .ToListAsync();

            return await _context.Usuarios
                .Where(u => u.Id != usuarioId &&
                           u.Activo &&
                           !usuariosEvaluados.Contains(u.Id))
                .Include(u => u.Intereses)
                .OrderBy(x => Guid.NewGuid()) // Ordenamiento aleatorio
                .Take(10)
                .ToListAsync();
        }
        public async Task<UsuarioEstadisticas> ObtenerEstadisticasUsuarioAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Intereses)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                throw new ArgumentException("Usuario no encontrado");

            var likesRecibidos = usuario.LikesRecibidos;
            var likesEnviados = await _context.Interacciones
                .CountAsync(i => i.UsuarioId == usuarioId && i.TipoInteraccion == TipoInteraccion.Like);

            var matchesCount = await _context.Matches
                .CountAsync(m => (m.Usuario1Id == usuarioId || m.Usuario2Id == usuarioId) && m.Activo);

            return new UsuarioEstadisticas
            {
                UsuarioId = usuarioId,
                LikesRecibidos = likesRecibidos,
                LikesEnviados = likesEnviados,
                TotalMatches = matchesCount,
                TotalIntereses = usuario.Intereses.Count,
                CreditosDisponibles = usuario.CreditosInteraccion
            };
        }

        // 8. Recargar créditos diarios
        public async Task RecargarCreditosDiariosAsync()
        {
            var hoy = DateTime.Now.Date;
            
            var usuariosParaRecarga = await _context.Usuarios
                .Where(u => u.Activo && u.UltimaRecargaCreditos < hoy)
                .ToListAsync();

            foreach (var usuario in usuariosParaRecarga)
            {
                usuario.CreditosInteraccion = 10; // Recargar a 10 créditos
                usuario.UltimaRecargaCreditos = hoy;
            }

            await _context.SaveChangesAsync();
        }

        // 9. Buscar usuarios por filtros avanzados
        public async Task<List<Usuario>> BuscarUsuariosAsync(BusquedaFiltros filtros)
        {
            var query = _context.Usuarios.AsQueryable();

            query = query.Where(u => u.Activo && u.Id != filtros.UsuarioId);

            if (filtros.EdadMinima.HasValue)
                query = query.Where(u => u.Edad >= filtros.EdadMinima.Value);

            if (filtros.EdadMaxima.HasValue)
                query = query.Where(u => u.Edad <= filtros.EdadMaxima.Value);

            if (filtros.Genero.HasValue)
                query = query.Where(u => u.Genero == filtros.Genero.Value);

            if (!string.IsNullOrWhiteSpace(filtros.Carrera))
                query = query.Where(u => u.Carrera.ToLower().Contains(filtros.Carrera.ToLower()));

            if (filtros.Intereses != null && filtros.Intereses.Any())
            {
                query = query.Where(u => u.Intereses.Any(i => filtros.Intereses.Contains(i.Interes)));
            }

            return await query
                .Include(u => u.Intereses)
                .OrderByDescending(u => u.LikesRecibidos)
                .Take(filtros.Limite ?? 50)
                .ToListAsync();
        }
    }
}