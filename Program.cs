using System;
using System.Collections.Generic;
using System.Data;
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
                Console.WriteLine("2. 👀 Ver usuarios registrados");
                Console.WriteLine("3. 👎 Ver dislikes recibidos por usuario");
                Console.WriteLine("4. 🚪 Salir");
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
                        VerDislikes();
                        break;
                    case "4":
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
                var query = "SELECT id, nombre, edad, genero, carrera FROM usuarios";
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                Console.WriteLine("\nUsuarios registrados:");
                Console.WriteLine("══════════════════════════════════════");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Nombre: {reader["nombre"]}, Edad: {reader["edad"]}, Género: {reader["genero"]}, Carrera: {reader["carrera"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener los usuarios: {ex.Message}");
            }
        }

        static void VerDislikes()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = @"
                    SELECT 
                        u.nombre AS usuario, 
                        COUNT(*) AS dislikes_recibidos
                    FROM 
                        interacciones i
                    JOIN 
                        usuarios u ON i.usuario_objetivo_id = u.id
                    WHERE 
                        i.tipo_interaccion = 'dislike'
                    GROUP BY 
                        i.usuario_objetivo_id
                    ORDER BY 
                        dislikes_recibidos DESC";
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                Console.WriteLine("\nDislikes recibidos por usuario:");
                Console.WriteLine("══════════════════════════════════════");
                while (reader.Read())
                {
                    Console.WriteLine($"Usuario: {reader["usuario"]}, Dislikes: {reader["dislikes_recibidos"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener los dislikes: {ex.Message}");
            }
        }
    }
}