using System;

namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIMatch
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("💞 MATCHES ENCONTRADOS 💞");
            Console.WriteLine("========================");
            
            // Datos de ejemplo para demostración
            Console.WriteLine($"{"Usuario 1",-20} {"Usuario 2",-20} {"Compatibilidad",-15}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Ana García",-20} {"Carlos López",-20} {"85%",-15}");
            Console.WriteLine($"{"María Rodríguez",-20} {"Juan Pérez",-20} {"78%",-15}");
            Console.WriteLine($"{"Laura Martínez",-20} {"Pedro Sánchez",-20} {"92%",-15}");
            Console.WriteLine($"{"Sofía Hernández",-20} {"Miguel Torres",-20} {"67%",-15}");
            Console.WriteLine($"{"Elena Castro",-20} {"David Ruiz",-20} {"89%",-15}");

            Console.WriteLine();
            Console.WriteLine("Nota: Esta es una demostración. La funcionalidad completa");
            Console.WriteLine("requiere la implementación de los algoritmos de matching.");
            Console.WriteLine("Presione una tecla para volver...");
            Console.ReadKey();
        }
    }
}
