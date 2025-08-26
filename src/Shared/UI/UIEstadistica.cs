using System;

namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIEstadistica
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("📊 ESTADÍSTICAS DEL SISTEMA 📊");
            Console.WriteLine("=============================");
            
            // Datos de ejemplo para demostración
            Console.WriteLine($"{"Métrica",-25} {"Valor",-10}");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"{"Usuarios totales",-25} {"15",-10}");
            Console.WriteLine($"{"Matches realizados",-25} {"8",-10}");
            Console.WriteLine($"{"Likes dados hoy",-25} {"23",-10}");
            Console.WriteLine($"{"Promedio de edad",-25} {"22.3",-10}");
            Console.WriteLine($"{"Usuarios activos",-25} {"12",-10}");
            Console.WriteLine($"{"Tasa de éxito",-25} {"73%",-10}");

            Console.WriteLine();
            Console.WriteLine("Nota: Esta es una demostración. La funcionalidad completa");
            Console.WriteLine("requiere la implementación del sistema de estadísticas.");
            Console.WriteLine("Presione una tecla para volver...");
            Console.ReadKey();
        }
    }
}
