using System;
using MySql.Data.MySqlClient;

namespace CampusLove
{
    class Program
    {
        private const string ConnectionString = "Server=localhost;Database=campus_love;Uid=campus2023;Pwd=campus2023;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n══════════════════════════════════════");
                Console.WriteLine("              MENÚ PRINCIPAL");
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("1. 📝 Registrar un nuevo usuario");
                Console.WriteLine("2. 👀 Ver usuarios registrados (con frase)");
                Console.WriteLine("3. 🔎 Ver perfil por nombre");
                Console.WriteLine("4. 👍👎 Dar like o dislike a un usuario");
                Console.WriteLine("5. 🚪 Salir");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Selecciona una opción: ");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarUsuario();
                        break;
                    case "2":
                        VerUsuarios();
                        break;
                    case "3":
                        VerPerfilPorNombre();
                        break;
                    case "4":
                        DarLikeODislike();
                        break;
                    case "5":
                        Console.WriteLine("¡Gracias por usar Campus Love! 💕");
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static void RegistrarUsuario()
        {
            Console.Write("\nNombre: ");
            var nombre = Console.ReadLine();

            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad) || edad < 18 || edad > 100)
            {
                Console.WriteLine("❌ Edad no válida. Debe ser entre 18 y 100 años.");
                return;
            }

            Console.Write("Género (Masculino/Femenino/Otro): ");
            var genero = Console.ReadLine();

            Console.Write("Carrera: ");
            var carrera = Console.ReadLine();

            Console.Write("Frase de perfil: ");
            var frasePerfil = Console.ReadLine();

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = "INSERT INTO usuarios (nombre, edad, genero, carrera, frase_perfil) VALUES (@nombre, @edad, @genero, @carrera, @frasePerfil)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@edad", edad);
                command.Parameters.AddWithValue("@genero", genero);
                command.Parameters.AddWithValue("@carrera", carrera);
                command.Parameters.AddWithValue("@frasePerfil", frasePerfil);
                command.ExecuteNonQuery();

                Console.WriteLine("✅ Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al registrar el usuario: {ex.Message}");
            }
        }

        static void VerUsuarios()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = "SELECT id, nombre, edad, genero, carrera, frase_perfil FROM usuarios";
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                Console.WriteLine("\nUsuarios registrados:");
                Console.WriteLine("══════════════════════════════════════");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Nombre: {reader["nombre"]}, Edad: {reader["edad"]}, Género: {reader["genero"]}, Carrera: {reader["carrera"]}, Frase: {reader["frase_perfil"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener los usuarios: {ex.Message}");
            }
        }

        static void VerPerfilPorNombre()
        {
            Console.Write("\n👉 Ingresa el nombre del usuario: ");
            var nombreBuscado = Console.ReadLine();

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                
                // Datos del usuario
                var queryUsuario = "SELECT id, nombre, edad, genero, carrera, frase_perfil FROM usuarios WHERE nombre = @nombre";
                using var commandUsuario = new MySqlCommand(queryUsuario, connection);
                commandUsuario.Parameters.AddWithValue("@nombre", nombreBuscado);
                using var reader = commandUsuario.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("❌ Usuario no encontrado.");
                    return;
                }

                var userId = Convert.ToInt32(reader["id"]);
                Console.WriteLine($"\nPerfil de {reader["nombre"]}:");
                Console.WriteLine($"Edad: {reader["edad"]}, Género: {reader["genero"]}, Carrera: {reader["carrera"]}");
                Console.WriteLine($"✨ Frase: {reader["frase_perfil"]}");
                reader.Close();

                // Likes y Dislikes
                var queryInteracciones = @"
                    SELECT tipo_interaccion, COUNT(*) AS total
                    FROM interacciones
                    WHERE usuario_objetivo_id = @id
                    GROUP BY tipo_interaccion";
                using var commandInt = new MySqlCommand(queryInteracciones, connection);
                commandInt.Parameters.AddWithValue("@id", userId);
                using var readerInt = commandInt.ExecuteReader();

                int likes = 0, dislikes = 0;
                while (readerInt.Read())
                {
                    if (readerInt["tipo_interaccion"].ToString() == "like")
                        likes = Convert.ToInt32(readerInt["total"]);
                    else
                        dislikes = Convert.ToInt32(readerInt["total"]);
                }
                Console.WriteLine($"👍 Likes: {likes} | 👎 Dislikes: {dislikes}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener el perfil: {ex.Message}");
            }
        }

        static void DarLikeODislike()
        {
            Console.Write("\n👉 Tu ID de usuario (quién da la interacción): ");
            if (!int.TryParse(Console.ReadLine(), out int usuarioId))
            {
                Console.WriteLine("❌ ID no válido.");
                return;
            }

            Console.Write("👉 ID del usuario objetivo: ");
            if (!int.TryParse(Console.ReadLine(), out int objetivoId))
            {
                Console.WriteLine("❌ ID no válido.");
                return;
            }

            Console.Write("👍 Dar like (1) o 👎 dislike (2): ");
            var opcion = Console.ReadLine();
            string tipo = opcion == "1" ? "like" : "dislike";

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = "INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion) VALUES (@usuarioId, @objetivoId, @tipo)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);
                command.Parameters.AddWithValue("@objetivoId", objetivoId);
                command.Parameters.AddWithValue("@tipo", tipo);
                command.ExecuteNonQuery();

                Console.WriteLine($"✅ {tipo.ToUpper()} registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al registrar interacción: {ex.Message}");
            }
        }
    }
}
