namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIRanking
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== MENÚ RANKING =====");
                Console.WriteLine("1. Ver ranking general");
                Console.WriteLine("2. Buscar usuario en ranking");
                Console.WriteLine("0. Volver");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Console.WriteLine("Mostrando ranking..."); break;
                    case 2: Console.WriteLine("Buscando usuario..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
