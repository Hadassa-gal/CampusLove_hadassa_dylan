namespace CampusLove_hadassa_dylan.src.Shared.UI
{
    public static class UIMain
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== 💖 CAMPUS LOVE 💖 =====");
                Console.WriteLine("1. 👤 Usuarios");
                Console.WriteLine("2. 🏆 Ranking");
                Console.WriteLine("3. 💞 Matches");
                Console.WriteLine("4. 🤝 Interacciones");
                Console.WriteLine("5. 📊 Estadísticas");
                Console.WriteLine("0. 🚪 Salir");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: UIUser.Show(); break;
                    case 2: UIRanking.Show(); break;
                    case 3: UIMatch.Show(); break;
                    case 4: UIInteraccion.Show(); break;
                    case 5: UIEstadistica.Show(); break;
                }

            } while (opcion != 0);
        }
    }
}
