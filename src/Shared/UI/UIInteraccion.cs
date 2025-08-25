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

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Console.WriteLine("Dando like..."); break;
                    case 2: Console.WriteLine("Enviando mensaje..."); break;
                    case 3: Console.WriteLine("Mostrando historial..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
