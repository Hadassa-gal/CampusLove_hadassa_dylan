namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIMatch
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== MENÚ MATCHES =====");
                Console.WriteLine("1. Crear match");
                Console.WriteLine("2. Ver matches de usuario");
                Console.WriteLine("3. Eliminar match");
                Console.WriteLine("0. Volver");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Console.WriteLine("Creando match..."); break;
                    case 2: Console.WriteLine("Listando matches..."); break;
                    case 3: Console.WriteLine("Eliminando match..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
