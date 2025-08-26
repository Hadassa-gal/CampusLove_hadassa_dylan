using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.DTOs;
using CampusLove_hadassa_dylan.src.Modules.UsuarioContraseñas.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.UsuarioIntereses.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.UsuarioContraseñas.Application.Services
{
    public class UsuarioContraseñaServices
    {
        private readonly AppDbContext _context;

        public UsuarioContraseñaServices(AppDbContext context)
        {
            _context = context;
        }

        #region Métodos Principales

        public async Task<Usuario> CrearUsuarioConContraseñaAsync(CrearUsuarioRequest request)
        {
            // Validar contraseña
            if (!ValidarContraseña(request.Contraseña))
            {
                throw new ArgumentException("La contraseña debe contener al menos 1 letra y 4 números");
            }

            // Verificar que el documento no exista
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NumeroDocumento == request.NumeroDocumento);

            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("Ya existe un usuario con este número de documento");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear usuario
                var usuario = new Usuario
                {
                    TipoDocumento = request.TipoDocumento,
                    NumeroDocumento = request.NumeroDocumento,
                    Nombre = request.Nombre,
                    Edad = request.Edad,
                    Genero = request.Genero,
                    Carrera = request.Carrera,
                    FrasePerfil = request.FrasePerfil,
                    FechaRegistro = DateTime.Now,
                    Activo = true,
                    CreditosInteraccion = 10,
                    UltimaRecargaCreditos = DateTime.Now.Date,
                    LikesRecibidos = 0
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Crear contraseña hasheada
                var contraseñaHasheada = HashearContraseña(request.Contraseña);
                var usuarioContraseña = new UsuarioContraseña
                {
                    UsuarioId = usuario.Id,
                    Contraseña = contraseñaHasheada,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                _context.UsuarioContraseñas.Add(usuarioContraseña);
                await _context.SaveChangesAsync();

                // Agregar intereses si los hay
                if (request.Intereses != null && request.Intereses.Any())
                {
                    foreach (var interes in request.Intereses)
                    {
                        var usuarioInteres = new UsuarioInteres
                        {
                            UsuarioId = usuario.Id,
                            Interes = interes.Trim()
                        };
                        _context.UsuarioIntereses.Add(usuarioInteres);
                    }
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                // Retornar usuario con sus datos completos
                return await _context.Usuarios
                    .Include(u => u.Intereses)
                    .FirstAsync(u => u.Id == usuario.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        //Validacion
        public async Task<Usuario?> ValidarCredencialesAsync(string numeroDocumento, string contraseña)
        {
            var usuario = await (
                from u in _context.Usuarios
                join c in _context.UsuarioContraseñas on u.Id equals c.UsuarioId
                where u.NumeroDocumento == numeroDocumento
                select new { Usuario = u, Contrasena = c.Contraseña }
            ).FirstOrDefaultAsync();

            if (usuario == null)
                return null;

            if (usuario.Contrasena == contraseña)
                return usuario.Usuario;

            return null;
        }
        // Cambiar contraseña
        public async Task<bool> CambiarContraseñaAsync(int usuarioId, string contraseñaActual, string nuevaContraseña)
        {
            if (!ValidarContraseña(nuevaContraseña))
            {
                throw new ArgumentException("La nueva contraseña debe contener al menos 1 letra y 4 números");
            }

            var usuarioContraseña = await _context.UsuarioContraseñas
                .FirstOrDefaultAsync(uc => uc.UsuarioId == usuarioId);

            if (usuarioContraseña == null)
                return false;

            // Verificar contraseña actual
            if (!VerificarContraseña(contraseñaActual, usuarioContraseña.Contraseña))
                return false;

            // Actualizar con nueva contraseña
            usuarioContraseña.Contraseña = HashearContraseña(nuevaContraseña);
            usuarioContraseña.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        // Restablecer contraseña (para admin o reset)
        public async Task<string> RestablecerContraseñaAsync(int usuarioId)
        {
            var usuarioContraseña = await _context.UsuarioContraseñas
                .FirstOrDefaultAsync(uc => uc.UsuarioId == usuarioId);

            if (usuarioContraseña == null)
                throw new ArgumentException("Usuario no encontrado");

            // Generar contraseña temporal que cumpla los requisitos
            var contraseñaTemporal = GenerarContraseñaTemporal();
            
            usuarioContraseña.Contraseña = HashearContraseña(contraseñaTemporal);
            usuarioContraseña.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            
            return contraseñaTemporal; // Retornar para enviarla al usuario
        }

        #endregion

        #region Métodos de Validación y Seguridad

        // Validar formato de contraseña (al menos 1 letra y 4 números)
        private static bool ValidarContraseña(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña))
                return false;

            // Contar letras y números
            int letras = contraseña.Count(char.IsLetter);
            int numeros = contraseña.Count(char.IsDigit);

            return letras >= 1 && numeros >= 4;
        }

        // Hashear contraseña con SHA256
        private static string HashearContraseña(string contraseña)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
            return Convert.ToBase64String(hashedBytes);
        }

        // Verificar contraseña
        private static bool VerificarContraseña(string contraseña, string hashAlmacenado)
        {
            var hashContraseña = HashearContraseña(contraseña);
            return hashContraseña == hashAlmacenado;
        }

        // Generar contraseña temporal
        private static string GenerarContraseñaTemporal()
        {
            var random = new Random();
            var letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var numeros = "0123456789";

            var sb = new StringBuilder();
            
            // Agregar al menos 1 letra
            sb.Append(letras[random.Next(letras.Length)]);
            
            // Agregar 4 números
            for (int i = 0; i < 4; i++)
            {
                sb.Append(numeros[random.Next(numeros.Length)]);
            }

            // Agregar más caracteres aleatorios para completar (2-3 más)
            var todosCaracteres = letras + numeros;
            for (int i = 0; i < random.Next(2, 4); i++)
            {
                sb.Append(todosCaracteres[random.Next(todosCaracteres.Length)]);
            }

            // Mezclar los caracteres
            var chars = sb.ToString().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int j = random.Next(i, chars.Length);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars);
        }

        #endregion
    }
}