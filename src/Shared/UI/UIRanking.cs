using System;

namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIRanking
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("🏆 RANKING GENERAL 🏆");
            Console.WriteLine("======================");
            
            // Datos de ejemplo para demostración
            Console.WriteLine($"{"Posición",-10} {"Usuario",-20} {"Likes",-10} {"Edad",-5} {"Carrera",-15}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"1",-10} {"Ana García",-20} {"25",-10} {"22",-5} {"Ingeniería",-15}");
            Console.WriteLine($"{"2",-10} {"Carlos López",-20} {"18",-10} {"24",-5} {"Medicina",-15}");
            Console.WriteLine($"{"3",-10} {"María Rodríguez",-20} {"15",-10} {"21",-5} {"Derecho",-15}");
            Console.WriteLine($"{"4",-10} {"Juan Pérez",-20} {"12",-10} {"23",-5} {"Arquitectura",-15}");
            Console.WriteLine($"{"5",-10} {"Laura Martínez",-20} {"8",-10} {"20",-5} {"Psicología",-15}");

            Console.WriteLine();
            Console.WriteLine("Nota: Esta es una demostración. La funcionalidad completa");
            Console.WriteLine("requiere la implementación de los repositorios de datos.");
            Console.WriteLine("Presione una tecla para volver...");
            Console.ReadKey();
        }
    }
}
