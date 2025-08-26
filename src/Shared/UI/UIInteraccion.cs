using CampusLove_hadassa_dylan.src.Shared.Credits;
using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using System;

namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIInteraccion
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== 🤝 MENÚ INTERACCIONES 🤝 =====");
                Console.WriteLine("1. 👍 Dar like");
                Console.WriteLine("2. ✉️ Enviar mensaje");
                Console.WriteLine("3. 📜 Ver historial de interacciones");
                Console.WriteLine("0. 🔙 Volver");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        if (CreditManager.CanLike())
                        {
                            CreditManager.UseLike();
                            Console.WriteLine("Like enviado exitosamente!");
                            Console.WriteLine($"Likes restantes hoy: {CreditManager.LikesRestantes()}");
                        }
                        else
                        {
                            Console.WriteLine("No tienes créditos suficientes para dar más likes hoy.");
                        }
                        break;
                    case 2: 
                        Console.WriteLine("Funcionalidad de mensajes en desarrollo...");
                        break;
                    case 3: 
                        MostrarHistorialInteracciones();
                        break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }

        private static void MostrarHistorialInteracciones()
        {
            Console.Clear();
            Console.WriteLine("📜 HISTORIAL DE INTERACCIONES 📜");
            Console.WriteLine("================================");
            
            // Datos de ejemplo para demostración
            Console.WriteLine($"{"Fecha",-15} {"Tipo",-10} {"Usuario Objetivo",-20} {"Estado",-10}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"2024-01-15",-15} {"Like",-10} {"Ana García",-20} {"Enviado",-10}");
            Console.WriteLine($"{"2024-01-14",-15} {"Like",-10} {"Carlos López",-20} {"Match!",-10}");
            Console.WriteLine($"{"2024-01-13",-15} {"Mensaje",-10} {"María Rodríguez",-20} {"Leído",-10}");
            Console.WriteLine($"{"2024-01-12",-15} {"Like",-10} {"Juan Pérez",-20} {"Pendiente",-10}");
            Console.WriteLine($"{"2024-01-11",-15} {"Like",-10} {"Laura Martínez",-20} {"Match!",-10}");

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para volver...");
            Console.ReadKey();
        }
    }
}
