namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIEstadistica
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== 📊 MENÚ ESTADÍSTICAS 📊 =====");
                Console.WriteLine("1. 🏅 Usuarios más activos");
                Console.WriteLine("2. 💑 Matches más comunes");
                Console.WriteLine("3. 🔢 Interacciones totales");
                Console.WriteLine("0. 🔙 Volver");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Console.WriteLine("Mostrando usuarios más activos..."); break;
                    case 2: Console.WriteLine("Mostrando matches comunes..."); break;
                    case 3: Console.WriteLine("Mostrando interacciones totales..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
