using System;
using MySql.Data.MySqlClient;
using CampusLove_hadassa_dylan.src.Shared.UI;

class Program
{
    //  Conexión a MySQL
    static string connectionString = "Server=localhost;Database=campus_love;Uid=root;Pwd=campus2023;";

    static void Main(string[] args)
    {
        UIMain.Show();
    }
}