using System;
using MySql.Data.MySqlClient;
using CampusLove_hadassa_dylan.src.Shared.UI;

class Program
{
    //  Conexión a MySQL
    static string connectionString = "Server=localhost;Database=campus_love;Uid=root;Pwd=campus2023;";

    static void Main(string[] args)
    {
        Console.Title = "CampusLove - Login";
        MostrarLogin();
    }

    static void MostrarLogin()
    {
        bool accesoConcedido = false;

        while (!accesoConcedido)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("      Bienvenido a CampusLove     ");
            Console.WriteLine("=================================\n");

            Console.Write("Número de documento: ");
            string numeroDocumento = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Contraseña: ");
            string contrasena = LeerContrasena();

            if (ValidarCredenciales(numeroDocumento, contrasena))
            {
                Console.WriteLine("\n✅ Acceso concedido. Bienvenido!");
                accesoConcedido = true;
                Console.ReadKey();
                UIMain.Show(); // Menú principal
            }
            else
            {
                Console.WriteLine("\n❌ Documento o contraseña incorrectos.");
                Console.WriteLine("Presiona cualquier tecla para intentarlo de nuevo...");
                Console.ReadKey();
            }
        }
    }

    // Validar contra la BD
    static bool ValidarCredenciales(string numeroDocumento, string contrasena)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"
                SELECT uc.contrasena 
                FROM usuarios u
                JOIN usuario_contrasenas uc ON u.id = uc.usuario_id
                WHERE u.numero_documento = @doc
            ";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@doc", numeroDocumento);

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string? storedPassword = result.ToString();
                    return storedPassword == contrasena;
                }
            }
        }
        return false;
    }

    //  Para ocultar la contraseña con ****
    static string LeerContrasena()
    {
        string pass = "";
        ConsoleKeyInfo tecla;

        do
        {
            tecla = Console.ReadKey(intercept: true);
            if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
            {
                pass += tecla.KeyChar;
                Console.Write("*");
            }
            else if (tecla.Key == ConsoleKey.Backspace && pass.Length > 0)
            {
                pass = pass.Substring(0, pass.Length - 1);
                Console.Write("\b \b");
            }
        } while (tecla.Key != ConsoleKey.Enter);

        return pass;
    }
}