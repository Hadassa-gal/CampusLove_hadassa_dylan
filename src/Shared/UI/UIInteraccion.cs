namespace CampusLove_hadassa_dylan.src.Shared.UI;
using CampusLove_hadassa_dylan.src.Shared.Credits;
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
                            Console.WriteLine("Dando like...");
                        }
                        else
                        {
                            Console.WriteLine("No tienes créditos suficientes para dar más likes hoy.");
                        }
                        break;
                    case 2: Console.WriteLine("Enviando mensaje..."); break;
                    case 3: Console.WriteLine("Mostrando historial..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
