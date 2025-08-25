using System;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace CampusLove
{
    class Program
    {
        private const string ConnectionString = "Server=localhost;Database=campus_love;Uid=campus2023;Pwd=campus2023;";

        // Estado de “sesión” para el modo multicliente ficticio:
        private static int? CurrentUserId = null;
        private static string CurrentUserName = "Guest";

        // Configuración de créditos (likes por día)
        private const int DAILY_LIKE_CREDIT = 10; // ejemplo
        private static CultureInfo Culture = new CultureInfo("es-CO"); // para formateos

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Header();

                Console.WriteLine("1) 📝 Register new user");
                Console.WriteLine("2) 👤 Log in / Switch user");
                Console.WriteLine("3) 👀 Browse profiles and Like/Dislike");
                Console.WriteLine("4) 💘 View my matches");
                Console.WriteLine("5) 📊 System statistics");
                Console.WriteLine("6) 🧾 Users (list/search)");   // útil en desarrollo
                Console.WriteLine("7) 🚪 Exit");
                Console.Write("Select an option: ");

                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarUsuario();
                        break;
                    case "2":
                        LoginUsuario();
                        break;
                    case "3":
                        NavegarPerfilesYReaccionar();
                        break;
                    case "4":
                        VerMisMatches();
                        break;
                    case "5":
                        VerEstadisticas();
                        break;
                    case "6":
                        SubmenuUsuarios();
                        break;
                    case "7":
                        Console.WriteLine("Thanks for using Campus Love! 💕");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option.");
                        break;
                }
                Pausa();
            }
        }

        // ========== PRESENTACIÓN ==========
        static void Header()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("           CAMPUS LOVE - CONSOLE");
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine($"Active user: {(CurrentUserId.HasValue ? $"{CurrentUserName} (ID {CurrentUserId})" : "—")}");

            // Mostrar créditos de like disponibles hoy
            var credits = GetTodayRemainingCredits(CurrentUserId);
            Console.WriteLine($"Daily like credits: {credits.ToString("N0", Culture)} / {DAILY_LIKE_CREDIT.ToString("N0", Culture)}");
            Console.WriteLine("══════════════════════════════════════");
        }

        static void Pausa()
        {
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // ========== MENÚ USUARIOS (LISTAR/BUSCAR) ==========
        static void SubmenuUsuarios()
        {
            Console.WriteLine("\n── Users submenu ──");
            Console.WriteLine("1) List all users (with profile phrase)");
            Console.WriteLine("2) Find profile by name");
            Console.Write("Select: ");
            var op = Console.ReadLine();
            if (op == "1") VerUsuarios();
            else if (op == "2") VerPerfilPorNombre();
            else Console.WriteLine("❌ Invalid option.");
        }

        // ========== REQUISITO 1: REGISTRARSE ==========
        static void RegistrarUsuario()
        {
            Console.Write("\nName: ");
            var nombre = ToTitle(Console.ReadLine());

            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int edad) || edad < 18 || edad > 100)
            {
                Console.WriteLine("❌ Invalid age. Must be between 18 and 100.");
                return;
            }

            Console.Write("Gender (Male/Female/Other): ");
            var genero = ToTitle(Console.ReadLine());

            Console.Write("Major/Career: ");
            var carrera = ToTitle(Console.ReadLine());

            Console.Write("Interests (comma-separated): ");
            var intereses = Console.ReadLine() ?? "";

            Console.Write("Profile phrase: ");
            var frasePerfil = Console.ReadLine();

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = @"INSERT INTO usuarios 
                              (nombre, edad, genero, carrera, intereses, frase_perfil) 
                              VALUES (@nombre, @edad, @genero, @carrera, @intereses, @frasePerfil)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@edad", edad);
                command.Parameters.AddWithValue("@genero", genero);
                command.Parameters.AddWithValue("@carrera", carrera);
                command.Parameters.AddWithValue("@intereses", intereses);
                command.Parameters.AddWithValue("@frasePerfil", frasePerfil);
                command.ExecuteNonQuery();

                Console.WriteLine("✅ User registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error registering user: {ex.Message}");
            }
        }

        // ========== LOGIN / CAMBIO DE USUARIO (MODO MULTICLIENTE) ==========
        static void LoginUsuario()
        {
            Console.Write("\nEnter your user ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Invalid ID.");
                return;
            }

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var q = "SELECT id, nombre FROM usuarios WHERE id = @id";
                using var cmd = new MySqlCommand(q, connection);
                cmd.Parameters.AddWithValue("@id", id);
                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    Console.WriteLine("❌ User not found.");
                    return;
                }
                CurrentUserId = Convert.ToInt32(r["id"]);
                CurrentUserName = r["nombre"].ToString()!;
                Console.WriteLine($"✅ Logged in as {CurrentUserName} (ID {CurrentUserId}).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error logging in: {ex.Message}");
            }
        }

        // ========== REQUISITO 2: VER PERFILES Y DAR LIKE/DISLIKE ==========
        static void NavegarPerfilesYReaccionar()
        {
            if (!EnsureLogged()) return;

            // Ejemplo simple: iterar usuarios distintos al activo y ofrecer like/dislike uno por uno
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var q = @"SELECT id, nombre, edad, genero, carrera, frase_perfil 
                          FROM usuarios
                          WHERE id <> @me
                          ORDER BY id";
                using var cmd = new MySqlCommand(q, connection);
                cmd.Parameters.AddWithValue("@me", CurrentUserId);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int targetId = Convert.ToInt32(reader["id"]);
                    Console.WriteLine("\n──────────────────────────────────────");
                    Console.WriteLine($"👤 {reader["nombre"]} (ID {targetId})");
                    Console.WriteLine($"Age: {reader["edad"]} | Gender: {reader["genero"]} | Major: {reader["carrera"]}");
                    Console.WriteLine($"✨ \"{reader["frase_perfil"]}\"");

                    // Mostrar créditos disponibles antes de permitir like
                    var credits = GetTodayRemainingCredits(CurrentUserId);
                    Console.WriteLine($"Credits available today: {credits}/{DAILY_LIKE_CREDIT}");

                    Console.Write("Choose: [1] Like  [2] Dislike  [S] Skip  [Q] Quit: ");
                    var op = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

                    if (op == "Q") break;
                    if (op == "S") continue;

                    if (op == "1" || op == "2")
                    {
                        var type = (op == "1") ? "like" : "dislike";
                        if (type == "like" && credits <= 0)
                        {
                            Console.WriteLine("⚠️ No credits left for today.");
                            continue;
                        }

                        reader.Close(); // cerrar para reusar la misma conexión con otro comando
                        RegistrarInteraccion((int)CurrentUserId!, targetId, type);
                        if (type == "like") Console.WriteLine("👍 Like sent!");
                        else Console.WriteLine("👎 Dislike sent!");

                        // ¿Se formó un match?
                        if (type == "like" && HayMatch((int)CurrentUserId!, targetId))
                        {
                            RegistrarMatch((int)CurrentUserId!, targetId);
                            Console.WriteLine("💘 It's a MATCH!");
                        }

                        // reabrir cursor para seguir iterando (volver a ejecutar el SELECT desde el siguiente)
                        cmd.Parameters["@me"].Value = CurrentUserId;
                        using var cmd2 = new MySqlCommand(q, connection);
                        cmd2.Parameters.AddWithValue("@me", CurrentUserId);
                        using var reader2 = cmd2.ExecuteReader();
                        // mover el cursor hasta el usuario siguiente al que ya procesamos
                        while (reader2.Read())
                        {
                            if (Convert.ToInt32(reader2["id"]) == targetId) break;
                        }
                        // continuar ciclo externo con reader2 (para simplificar, salimos y relanzamos)
                        Console.WriteLine("↩️ Returning to profiles list...");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error browsing profiles: {ex.Message}");
            }
        }

        // ========== REQUISITO 3: VER MIS MATCHES ==========
        static void VerMisMatches()
        {
            if (!EnsureLogged()) return;

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var q = @"
                    SELECT 
                        u.id, u.nombre, u.edad, u.genero, u.carrera, u.frase_perfil
                    FROM matches m
                    JOIN usuarios u ON (u.id = CASE WHEN m.usuario1_id = @me THEN m.usuario2_id ELSE m.usuario1_id END)
                    WHERE m.usuario1_id = @me OR m.usuario2_id = @me
                    ORDER BY u.nombre";
                using var cmd = new MySqlCommand(q, connection);
                cmd.Parameters.AddWithValue("@me", CurrentUserId);
                using var r = cmd.ExecuteReader();

                Console.WriteLine("\n💘 Your matches:");
                Console.WriteLine("══════════════════════════════════════");
                bool any = false;
                while (r.Read())
                {
                    any = true;
                    Console.WriteLine($"• {r["nombre"]} (ID {r["id"]}) - {r["carrera"]} | \"{r["frase_perfil"]}\"");
                }
                if (!any) Console.WriteLine("No matches yet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading matches: {ex.Message}");
            }
        }

        // ========== REQUISITO 4: ESTADÍSTICAS (LINQ PUEDE IR EN C# O SQL) ==========
        static void VerEstadisticas()
        {
            Console.WriteLine("\n📊 System statistics");
            Console.WriteLine("1) User with most received likes");
            Console.WriteLine("2) Users with most matches");
            Console.WriteLine("3) Daily likes used by user (today)");
            Console.Write("Select: ");
            var op = Console.ReadLine();

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                switch (op)
                {
                    case "1":
                        {
                            var q = @"
                                SELECT u.id, u.nombre, COUNT(*) AS likes
                                FROM interacciones i
                                JOIN usuarios u ON u.id = i.usuario_objetivo_id
                                WHERE i.tipo_interaccion = 'like'
                                GROUP BY u.id, u.nombre
                                ORDER BY likes DESC
                                LIMIT 5";
                            using var cmd = new MySqlCommand(q, connection);
                            using var r = cmd.ExecuteReader();
                            Console.WriteLine("\n🏆 Top users by likes received:");
                            while (r.Read())
                                Console.WriteLine($"• {r["nombre"]} (ID {r["id"]}): {r["likes"]} likes");
                            break;
                        }
                    case "2":
                        {
                            var q = @"
                                SELECT u.id, u.nombre, COUNT(*) AS matches
                                FROM (
                                    SELECT usuario1_id AS uid FROM matches
                                    UNION ALL
                                    SELECT usuario2_id AS uid FROM matches
                                ) m
                                JOIN usuarios u ON u.id = m.uid
                                GROUP BY u.id, u.nombre
                                ORDER BY matches DESC
                                LIMIT 5";
                            using var cmd = new MySqlCommand(q, connection);
                            using var r = cmd.ExecuteReader();
                            Console.WriteLine("\n💞 Top users by matches:");
                            while (r.Read())
                                Console.WriteLine($"• {r["nombre"]} (ID {r["id"]}): {r["matches"]} matches");
                            break;
                        }
                    case "3":
                        {
                            Console.Write("Enter user ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("❌ Invalid ID."); return; }
                            Console.WriteLine($"Likes used today: {GetTodayUsedLikes(id)} / {DAILY_LIKE_CREDIT}");
                            break;
                        }
                    default:
                        Console.WriteLine("❌ Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading statistics: {ex.Message}");
            }
        }

        // ========== UTILIDADES EXISTENTES: LISTAR/BUSCAR ==========
        static void VerUsuarios()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var query = "SELECT id, nombre, edad, genero, carrera, frase_perfil FROM usuarios";
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                Console.WriteLine("\nRegistered users:");
                Console.WriteLine("══════════════════════════════════════");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]} | {reader["nombre"]} | {reader["edad"]} | {reader["genero"]} | {reader["carrera"]} | \"{reader["frase_perfil"]}\"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading users: {ex.Message}");
            }
        }

        static void VerPerfilPorNombre()
        {
            Console.Write("\nEnter the user name: ");
            var nombreBuscado = Console.ReadLine();

            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();

                var queryUsuario = "SELECT id, nombre, edad, genero, carrera, frase_perfil FROM usuarios WHERE nombre = @nombre";
                using var commandUsuario = new MySqlCommand(queryUsuario, connection);
                commandUsuario.Parameters.AddWithValue("@nombre", nombreBuscado);
                using var reader = commandUsuario.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("❌ User not found.");
                    return;
                }

                var userId = Convert.ToInt32(reader["id"]);
                Console.WriteLine($"\nProfile of {reader["nombre"]}:");
                Console.WriteLine($"Age: {reader["edad"]}, Gender: {reader["genero"]}, Major: {reader["carrera"]}");
                Console.WriteLine($"✨ {reader["frase_perfil"]}");
                reader.Close();

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
                Console.WriteLine($"❌ Error loading profile: {ex.Message}");
            }
        }

        // ========== INFRA: INTERACCIONES, MATCHES, CRÉDITOS ==========
        static void RegistrarInteraccion(int fromUserId, int toUserId, string tipo)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            // Validar crédito cuando es like
            if (tipo == "like")
            {
                var remaining = GetTodayRemainingCredits(fromUserId);
                if (remaining <= 0)
                    throw new InvalidOperationException("No like credits left for today.");
            }

            var q = @"INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion, fecha)
                      VALUES (@u, @t, @tipo, CURRENT_DATE())";
            using var cmd = new MySqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@u", fromUserId);
            cmd.Parameters.AddWithValue("@t", toUserId);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.ExecuteNonQuery();
        }

        static bool HayMatch(int a, int b)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            // ¿El otro ya me dio like?
            var q = @"SELECT COUNT(*) FROM interacciones 
                      WHERE usuario_id = @b AND usuario_objetivo_id = @a AND tipo_interaccion = 'like'";
            using var cmd = new MySqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@a", a);
            cmd.Parameters.AddWithValue("@b", b);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        static void RegistrarMatch(int a, int b)
        {
            // Evitar duplicados (a,b) / (b,a)
            if (ExisteMatch(a, b)) return;

            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var q = @"INSERT INTO matches (usuario1_id, usuario2_id, fecha) 
                      VALUES (@a, @b, CURRENT_DATE())";
            using var cmd = new MySqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@a", Math.Min(a, b));
            cmd.Parameters.AddWithValue("@b", Math.Max(a, b));
            cmd.ExecuteNonQuery();
        }

        static bool ExisteMatch(int a, int b)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var q = @"SELECT COUNT(*) FROM matches 
                      WHERE (usuario1_id = @x AND usuario2_id = @y) OR (usuario1_id = @y AND usuario2_id = @x)";
            using var cmd = new MySqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@x", Math.Min(a, b));
            cmd.Parameters.AddWithValue("@y", Math.Max(a, b));
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        static int GetTodayRemainingCredits(int? userId)
        {
            if (!userId.HasValue) return DAILY_LIKE_CREDIT; // sin sesión, solo informativo
            var used = GetTodayUsedLikes(userId.Value);
            var remaining = DAILY_LIKE_CREDIT - used;
            return Math.Max(0, remaining);
        }

        static int GetTodayUsedLikes(int userId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var q = @"SELECT COUNT(*) FROM interacciones 
                      WHERE usuario_id = @u AND tipo_interaccion = 'like' AND fecha = CURRENT_DATE()";
            using var cmd = new MySqlCommand(q, connection);
            cmd.Parameters.AddWithValue("@u", userId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ========== HELPER ==========
        static bool EnsureLogged()
        {
            if (!CurrentUserId.HasValue)
            {
                Console.WriteLine("⚠️ You must log in first (option 2).");
                return false;
            }
            return true;
        }

        static string ToTitle(string? s)
        {
            s ??= "";
            s = s.Trim().ToLowerInvariant();
            return Culture.TextInfo.ToTitleCase(s);
        }
    }
}
