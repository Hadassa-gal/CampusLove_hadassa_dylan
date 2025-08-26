using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.DTOs;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.Services;
using CampusLove_hadassa_dylan.src.Modules.UsuarioContraseñas.Application.Services;
using CampusLove_hadassa_dylan.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIUser
    {

        public static async Task Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== 👤 MENÚ USUARIOS 👤 =====");
                Console.WriteLine("1. ✨ Crear usuario");
                Console.WriteLine("2. 📋 Listar usuarios");
                Console.WriteLine("3. 🔍 Buscar usuarios por carrera");
                Console.WriteLine("4. 💕 Ver usuarios con intereses similares");
                Console.WriteLine("5. 📊 Ver estadísticas de usuario");
                Console.WriteLine("6. 🎯 Ver usuarios pendientes");
                Console.WriteLine("7. 🔍 Búsqueda avanzada");
                Console.WriteLine("8. 🔄 Recargar créditos diarios");
                Console.WriteLine("0. 🔙 Volver");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("❌ Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            //await CrearUsuarioAsync();
                            break;
                        case 2:
                            //await ListarUsuariosAsync();
                            break;
                        case 3:
                            //await BuscarPorCarreraAsync();
                            break;
                        case 4:
                            //await VerInteresesSimilaresAsync();
                            break;
                        case 5:
                            //await VerEstadisticasAsync();
                            break;
                        case 6:
                            //await VerUsuariosPendientesAsync();
                            break;
                        case 7:
                            //await BusquedaAvanzadaAsync();
                            break;
                        case 8:
                            //await RecargarCreditosAsync();
                            break;
                        case 0:
                            Console.WriteLine("👋 Volviendo al menú principal...");
                            break;
                        default:
                            Console.WriteLine("❌ Opción inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);

            await Task.CompletedTask;
        }
    }
}