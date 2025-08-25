namespace CampusLove_hadassa_dylan.src.Shared.UI   
{
    public static class UIUser
    {
        public static void Show()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== \uD83D\uDC64 MENÚ USUARIOS \uD83D\uDC64 =====");
                Console.WriteLine("1. ✨ Crear usuario");
                Console.WriteLine("2. 📋 Listar usuarios");
                Console.WriteLine("3. ✏️ Editar usuario");
                Console.WriteLine("4. 🗑️ Eliminar usuario");
                Console.WriteLine("0. 🔙 Volver");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Console.WriteLine("Creando usuario..."); break;
                    case 2: Console.WriteLine("Listando usuarios..."); break;
                    case 3: Console.WriteLine("Editando usuario..."); break;
                    case 4: Console.WriteLine("Eliminando usuario..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
