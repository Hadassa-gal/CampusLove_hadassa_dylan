namespace CampusLove_hadassa_dylan.src.Shared.UI;   
using CampusLove_hadassa_dylan.src.Shared.Credits;

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

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        CrearUsuario();
                        break;
                    case 2: Console.WriteLine("Listando usuarios..."); break;
                    case 3: Console.WriteLine("Editando usuario..."); break;
                    case 4: Console.WriteLine("Eliminando usuario..."); break;
                }

                if (opcion != 0) Console.ReadKey();

            } while (opcion != 0);
        }

        private static void CrearUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Crear Usuario ===");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }
            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad) || edad < 18)
            {
                Console.WriteLine("Edad inválida. Debe ser mayor o igual a 18.");
                return;
            }
            Console.Write("Género (M/F/O): ");
            string genero = Console.ReadLine() ?? string.Empty;
            if (genero != "M" && genero != "F" && genero != "O")
            {
                Console.WriteLine("Género inválido. Debe ser M, F u O.");
                return;
            }
            Console.WriteLine($"Usuario creado: {nombre}, Edad: {edad}, Género: {genero}");
            // Aquí se agregaría la lógica para guardar el usuario
        }
    }

